using System.ComponentModel.DataAnnotations;

namespace BankAuth.Models
{
    public class UserReg
    {
        [Key]
        public int UserId { get; set; }
        public string? Password { get; set; }

        public string? Token { get; set; }

        public string? Role { get; set; }

        public string? AccountNum { get; set; }
        public string? CustomerId { get; set; }
        public string? AuthToken { get; set; }
    }
}
