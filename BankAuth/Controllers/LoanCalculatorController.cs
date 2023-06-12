using BankAuth.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace BankAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCalculatorController : ControllerBase
    {

        private readonly AppDbContext _authContext;

        public class LoanCalculator
        {
            
            public string? LoanAmount { get; set; }
            public string? Interest { get; set; }
            public int? Tenure { get; set; }

        }
        public class Result
        {
            public string LoanEMI { get; set; }
            public string TotalAmountPayable { get; set; }
            public string TotalInterestPayable { get; set; }
        }

        public LoanCalculatorController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;

        }

        [HttpPost("calculateEMI")]

        public async Task<IActionResult> CalculateEMI([FromBody] LoanCalculator LoanObj)
        {

            float interest = float.Parse(LoanObj.Interest);
            float loanAmount = float.Parse(LoanObj.LoanAmount);
            var tenure = LoanObj.Tenure;
           
        
            interest = interest / 12 / 100;




            var loan_Emi =Math.Round( (loanAmount * interest * Math.Pow(1 + interest, (double)tenure))  / (Math.Pow(1 + interest, (double)tenure) - 1));
           
            var totalAmountPayable = Math.Round((double)(loan_Emi * tenure));
            var totalInterestPayable = Math.Round((double)(totalAmountPayable-loanAmount));
            if (loanAmount <= 0 || interest <= 0 || tenure <= 0)
            {
                return BadRequest("Invalid loan details.");
            }
            else
            {
                var result = new Result
                {

                    LoanEMI = $"{loan_Emi}",
                    TotalAmountPayable = $"{totalAmountPayable}",
                    TotalInterestPayable = $"{totalInterestPayable}"


                };
                return Ok(result);
            }
                
            
            

        }
    }
}
