using BankAuth.Context;
using BankAuth.Models;
using BankAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using BankAuth.Helpers;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace BankAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        private readonly IEmailService _emailService;

        public UserRegController(AppDbContext appDbContext, IEmailService emailService
           )
        {
            _authContext = appDbContext;
            _emailService = emailService;

        }

        public class UserObj
        {
            public UserReg? userReg { get; set; }
            public CustomerAccountInfo? custObj { get; set; }
            
        }


        [HttpPost("authenticate")]

        public async Task<IActionResult> Authenticate([FromBody] UserReg userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            var user = await _authContext.UserRegs.FirstOrDefaultAsync(x => x.CustomerId == userObj.CustomerId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });

            }

            string generated_token = GeneratedTokenForTwoF();



            user.AuthToken = PasswordHasher.HashPassword(generated_token);
            //user.AuthToken = generated_token;
            await _authContext.SaveChangesAsync();


            var text = new Message(
                new string[] { "archanaabillava0154@gmail.com" },
                "OTP for Authentication",
              $"Hello {user.CustomerId}, {Environment.NewLine}  Your OTP for verification is  {generated_token.ToString()}");




            _emailService.SendEmail(text);


            return Ok(new
            {
                //Token = user.Token,
                Message = "Check your registered Email for the OTP"
            });



        }


        [HttpPost("authenticate/2F")]
        public async Task<IActionResult> ProAuthenticate([FromBody] UserReg userObj, string tokenentry)
        {


            var user = await _authContext.UserRegs.FirstOrDefaultAsync(x => x.CustomerId == userObj.CustomerId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }


            if (PasswordHasher.VerifyPassword(tokenentry, user.AuthToken))

            //if (tokenentry == user.AuthToken)
            {
                user.Token = CreateJwtToken(user);

                return Ok(new
                {
                    Token = user.Token,
                    Message = $"Login Sucess"
                });
            }

            return BadRequest(new
            {

                Message = $"Please Check the OTP and try again"
            });


        }

        [HttpPost("registers")]


        public async Task<IActionResult> RegisterNewUser([FromBody] UserObj userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            if (await CheckPhoneExistAsync(userObj.custObj.ContactNum))
            {
                return BadRequest(new { Message = $"ContactNum doesn't exists" });
            }


            if (await CheckAccountNumberExistAsync(userObj.custObj.AccountNum))
            {
                return BadRequest(new { Message = "AccountNum doesn't exists" });
            }

            if (await CheckEmailExistAsync(userObj.custObj.Email))
            {
                return BadRequest(new { Message = "Email doesnt  exists" });

            }



            var pass = CheckPasswordStrength(userObj.userReg.Password);

            if (!string.IsNullOrEmpty(pass))
            {
                return BadRequest(new { Message = pass.ToString() });
            }
            var customerId = CheckCustomerIdStrength(userObj.userReg.CustomerId);

            if (!string.IsNullOrEmpty(customerId))
            {
                return BadRequest(new { Message = customerId.ToString() });
            }
            var accountnumber = CheckAccountNumberStrength(userObj.custObj.AccountNum);
            if (!string.IsNullOrEmpty(accountnumber))
            {
                return BadRequest(new { Message = accountnumber.ToString() });
            }
            var contactnumber = CheckContactStrength(userObj.custObj.ContactNum);
            if (!string.IsNullOrEmpty(contactnumber))
            {
                return BadRequest(new { Message = contactnumber.ToString() });
            }




            bool isAssociated = await ArePhoneAndEmailAccountAssociated(userObj.custObj.ContactNum, userObj.custObj.Email, userObj.custObj.AccountNum, userObj.custObj.UserName);

            if (isAssociated)
            {
                if (await CheckCustomerIdExistAsync(userObj.userReg.CustomerId))
                {
                    return BadRequest(new { Message = "CustomerId already exists, Please Login" });
                }

                if (await CheckAccountNumberInUserReg(userObj.custObj.AccountNum))
                {
                    return BadRequest(new { Message = "Account already exists Please Login" });
                }


                //if (await CheckCustomerIdExistAsync(userObj.userReg.CustomerId))
                //    return BadRequest(new { Message = "CustomerId already exists" });



                var newuser = new UserReg
                {
                    Password = PasswordHasher.HashPassword(userObj.userReg.Password),
                    //Password = userObj.userReg.Password,
                    Role = "user",
                    Token = "",
                    AccountNum = userObj.custObj.AccountNum,
                    CustomerId = userObj.userReg.CustomerId,

                };


                await _authContext.UserRegs.AddAsync(newuser);
                await _authContext.SaveChangesAsync();

                return Ok(new { Message = "Register Success" });
            }
            else
            {
                return BadRequest(new { Message = "Invalid Credentials, Please Check and try again" });
            }



        }

        private async Task<bool> CheckPhoneExistAsync(string phone)
        {
            return !(await _authContext.AccInfo.AnyAsync(x => x.ContactNum == phone));
        }

        private async Task<bool> CheckAccountNumberInUserReg(string regaccount)
        {
            return (await _authContext.UserRegs.AnyAsync(x => x.AccountNum == regaccount));
        }




        private async Task<bool> CheckCustomerIdExistAsync(string customer_id)
        {
            return await _authContext.UserRegs.AnyAsync(x => x.CustomerId == customer_id);
        }




        private async Task<bool> CheckEmailExistAsync(string email)

        {
            return !(await _authContext.AccInfo.AnyAsync(x => x.Email == email));
        }



        private async Task<bool> CheckAccountNumberExistAsync(string accountnum)
        {
            return !(await _authContext.AccInfo.AnyAsync(x => x.AccountNum == accountnum));
        }

        private async Task<bool> ArePhoneAndEmailAccountAssociated(string phone, string email, string AccountNum, string username)
        {
            var phoneUser = await _authContext.AccInfo.SingleOrDefaultAsync(x => x.ContactNum == phone);
            var emailUser = await _authContext.AccInfo.SingleOrDefaultAsync(x => x.Email == email);
            var accountUser = await _authContext.AccInfo.SingleOrDefaultAsync(x => x.AccountNum == AccountNum);
            var usernameUser = await _authContext.AccInfo.SingleOrDefaultAsync(x => x.UserName == username);



            if (phoneUser != null && emailUser != null && accountUser != null && usernameUser != null)
            {
                // Compare the user IDs
                if (phoneUser.CustomerAccountId == emailUser.CustomerAccountId && phoneUser.CustomerAccountId == accountUser.CustomerAccountId && phoneUser.CustomerAccountId == usernameUser.CustomerAccountId)
                {

                    return true;
                }
            }
            return false;
        }


        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();

            if (password.Length < 8)
            {
                sb.Append("Password should have minimum of 8 characters" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,-,=]"))
            {
                sb.Append("Password should contain special characters" + Environment.NewLine);
            }

            return sb.ToString();

        }

        private string CheckAccountNumberStrength(string accountnum)
        {
            StringBuilder sb = new StringBuilder();

            if (accountnum.Length < 12 || accountnum.Length > 12)
            {
                sb.Append("AccountNumber must have 12 digits" + Environment.NewLine);
            }

            return sb.ToString();

        }

        private string CheckContactStrength(string contactNum)
        {
            StringBuilder sb = new StringBuilder();

            if (contactNum.Length < 10 || contactNum.Length > 10)
            {
                sb.Append("Phonenumber must have 10 digits" + Environment.NewLine);
            }

            return sb.ToString();

        }

        private string CheckCustomerIdStrength(string customerid)
        {
            StringBuilder sb = new StringBuilder();

            if (customerid.Length < 8)
            {
                sb.Append("CustomerId should have minimum of 8 characters" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(customerid, "[a-z]") && Regex.IsMatch(customerid, "[A-Z]") && Regex.IsMatch(customerid, "[0-9]")))
            {
                sb.Append("CustomerId should be Alphanumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(customerid, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,-,=]"))
            {
                sb.Append("CustomerId should contain special characters" + Environment.NewLine);
            }

            return sb.ToString();

        }

        private string GeneratedTokenForTwoF()
        {
            Random rnd = new Random();

            var token_generated = rnd.Next(100000, 999999);


            return token_generated.ToString();
        }



        [HttpGet("getByAccountNum")]
        public async Task<IActionResult> getUserByAccountNumber(string accountnum)
        {
            var user = await _authContext.AccInfo.FirstOrDefaultAsync(x => x.AccountNum == accountnum);

            if (user == null)
            {
                return BadRequest(new
                {

                    Message = "User doesnt exist"
                });
            }
            return Ok(user);
        }


        private  string CreateJwtToken(UserReg user)
        {
           
              var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("veryveryverysecret...");
            if (user.Role == "user")
            {
                var identity = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Actor,user.AccountNum),
                new Claim(ClaimTypes.NameIdentifier,user.CustomerId)
                });
                var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credentials
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                return jwtTokenHandler.WriteToken(token);
            }
            else
            {
                var identity = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier,user.CustomerId)
                });
                var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credentials
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                return jwtTokenHandler.WriteToken(token);
            }
                

        }
       

        


    }
}
