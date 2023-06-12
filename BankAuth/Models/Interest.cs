using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BankAuth.Models
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        public string? LoanType { get; set; }    

        public float? LoanInterest { get; set; }
    }
}
