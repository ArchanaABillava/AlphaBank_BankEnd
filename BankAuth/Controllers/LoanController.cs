using BankAuth.Context;
using BankAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace BankAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly AppDbContext _authContext;


        public LoanController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;


        }

        //public class LoanObj
        //{
            //public LoanDetails? loanDetailsobj { get; set; }
            //public Document? DocObj { get; set; }

        //}

        


       [HttpGet("getAllLoans")]
        
       public async Task<IEnumerable<LoanDetails>>GetLoanDetails()
       {
            return await _authContext.LoanDetails.ToListAsync();
        }

        
        


        [HttpGet("getInterest")]
        public async Task<float?> GetInterestByLoanType(string loanType)
        {
            var type = await _authContext.LoanInterest.FirstOrDefaultAsync(x => x.LoanType == loanType);

            return type.LoanInterest;

        }

        [HttpPost("applyLoan")]

        public async Task<IActionResult> ApplyLoan([FromBody] LoanDetails LoanObj)
        {
            LoanObj.Interest = await GetInterestByLoanType(LoanObj.LoanType);

            float loanAmount = float.Parse(LoanObj.LoanAmount);
            var tenure = LoanObj.Tenure;


            DateTime currentDate = DateTime.Now;
            DateTime futureDate = DateTime.Now;
            string loanStartDate = currentDate.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("en-GB"));
            futureDate = futureDate.AddYears((int)tenure);
            string loanEndDate = futureDate.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("en-GB"));



            var total_loan_Amount = (Math.Round((double)(loanAmount + (loanAmount * (LoanObj.Interest / 100) * tenure)))).ToString();
            var loan_Emi = (Math.Round((double)(loanAmount * (LoanObj.Interest / 1200) * Math.Pow((double)(1 + (LoanObj.Interest / 1200)), (double)(tenure * 12))) / (Math.Pow((double)(1 + (LoanObj.Interest / 1200)), (double)(tenure * 12)) - 1))).ToString();


            var updatedLoanObj = new LoanDetails
            {
                AccountNum = LoanObj.AccountNum,
                LoanType = LoanObj.LoanType,
                LoanAmount = LoanObj.LoanAmount,
                Interest = LoanObj.Interest,
                Tenure = LoanObj.Tenure,
                LoanEmi = loan_Emi,
                LoanTotalAmount = total_loan_Amount,
                MonthlyIncome = LoanObj.MonthlyIncome,
                AnnualIncome = LoanObj.AnnualIncome,
                OtherEmi = LoanObj.OtherEmi,
                LoanStartDate = loanStartDate,
                LoanEndDate = loanEndDate,
                LoanPurpose = LoanObj.LoanPurpose,
                PropertyArea = LoanObj.PropertyArea,
                PropertyLoc = LoanObj.PropertyLoc,
                PropertyValue = LoanObj.PropertyValue,
            };

            _authContext.LoanDetails.Add(updatedLoanObj);

            await _authContext.SaveChangesAsync();


            return Ok(new { Message = "LoanApplied Successfully" });
        }

        [HttpGet("getLoanByAccountNum")]
        public async Task<IActionResult> getLoanByAccountNumber(string accountnum)
        {
            var loanDetails = await _authContext.LoanDetails.Where(x => x.AccountNum == accountnum).OrderByDescending(x => x.LoanId).FirstOrDefaultAsync();


            if (loanDetails == null)
            {
                return BadRequest(new
                {

                    Message = "No Loans taken by the user"
                });
            }
            return Ok(loanDetails);
        }
    }




}
