using System.ComponentModel.DataAnnotations;

namespace BankAuth.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        public string? AccountNum { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }

        public string? LoanType { get; set; }
    }
}
