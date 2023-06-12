using BankAuth.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BankAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanEligibleController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public class LoanEligible
        {
            public string? LoanType { get; set; }
            public string? LoanAmount { get; set; }
            public int? Tenure { get; set; }
            public string? MonthlyIncome { get; set; }
            public string? AnnualIncome { get; set; }
            public string? OtherEmi { get; set; }

        }
        public class Result
        {
            public string AvailableEMI { get; set; }
            public string LoanEMI { get; set; }
            public string Results { get; set; }
            public string Color { get; set; }
        }
        public LoanEligibleController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;

        }

        [HttpGet("getInterest")]
        public async Task<float?> GetInterestByLoanType(string loanType)
        {
            var type = await _authContext.LoanInterest.FirstOrDefaultAsync(x => x.LoanType == loanType);

            return type.LoanInterest;

        }
        [HttpPost("checkEligible")]

        public async Task<IActionResult> CheckEligible([FromBody] LoanEligible LoanObj)
        {
            var Interest = await GetInterestByLoanType(LoanObj.LoanType);

            float loanAmount = float.Parse(LoanObj.LoanAmount);
            float monthlyIncome = float.Parse(LoanObj.MonthlyIncome);
            float annualIncome = float.Parse(LoanObj.AnnualIncome);
            float otheremi = float.Parse(LoanObj.OtherEmi);

            var tenure = LoanObj.Tenure;

            var monthemi = Math.Round(monthlyIncome / 2);
            var annualemi = Math.Round(annualIncome / 24);

            var avgemi = Math.Round((monthemi + annualemi) / 2);

            var availableamount = avgemi - otheremi;

            var loan_Emi = Math.Round((double)(loanAmount * (Interest / 1200) * Math.Pow((double)(1 + (Interest / 1200)), (double)(tenure))) / (Math.Pow((double)(1 + (Interest / 1200)), (double)(tenure)) - 1));

            if (availableamount < loan_Emi)
            {

                var result = new Result
                {
                    AvailableEMI = $"Your Available EMI is {availableamount}",
                    LoanEMI = $"Your Loan EMI for the following input is {loan_Emi}",
                    Results = "Sorry your Loan cannot be approved, Please Contact our customer services for more info",
                    Color = "danger"
                };
                return Ok(result);
            }
            else
            {
                var result = new Result
                {
                    AvailableEMI = $"Your Available EMI is {availableamount}",
                    LoanEMI = $"Your Loan EMI for the following input is {loan_Emi}",
                    Results = "Congratulations!! Your Loan is Eligible and waiting to be approved",
                    Color = "success"
                };
                return Ok(result);
            }

        }
    
}
}
