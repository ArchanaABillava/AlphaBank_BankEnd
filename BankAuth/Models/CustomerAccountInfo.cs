using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BankAuth.Models
{
    public class CustomerAccountInfo
    {

        public CustomerAccountInfo()
        {
            //AadharNum = "23452345234590";
            //PanNum = "KWBPS2301H";
            //DOB = "1/07/1994";
            //Gender = "Male";
            //Addresss = "1676 Simons Hollow Road,Hazleton,Pennsylvania,PIN:18201";
        }
        [Key]
        public int CustomerAccountId { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }
        public string? ContactNum { get; set; }

        public string? AccountNum { get; set; }
        
        public string? AadharNum { get; set; }

        public string? PanNum { get; set; }
        
        public string? DOB { get; set; }

        public string? Gender { get; set; }

        public string? Addresss { get; set; }
        public string? EmpType { get; set; }
        public string? WorkExp { get; set; }
        public string? OccupationName { get; set; }
        public string? OccupationAddress { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationAddress { get; set; }
       
    }
}

