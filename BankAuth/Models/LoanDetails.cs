using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BankAuth.Models
{
    public class LoanDetails
    {
       

        [Key]
        public int LoanId { get; set; }

        public string? AccountNum { get; set; }

        public string? LoanType { get; set; }
        public string? LoanAmount { get; set; }
        public float? Interest { get; set; }
        public int? Tenure { get; set; }
        public string? LoanEmi { get; set; }

        public string? LoanTotalAmount { get; set; }
        public string? MonthlyIncome { get; set; }
        public string? AnnualIncome { get; set; }
        public string? OtherEmi { get; set; }
        public string? LoanStartDate { get; set; }
        public string? LoanEndDate { get; set; }
        public string? LoanStatus { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Modified_At { get; set; } = DateTime.Now;
        public string? LoanPurpose { get; set; }
        public string? PropertyLoc { get; set; }
        public string? PropertyValue { get; set; }
        public string? PropertyArea { get; set; }

    }
}
