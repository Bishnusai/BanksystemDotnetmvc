using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.BAL
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Plz Enter Your Correct Name")]
        [Display(Name ="First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last Name: ")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Give Valid Email")]
        [Display(Name = "EMail: ")]
        [EmailAddress]
        public string UEmail { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Contact Number: ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public long UMobileNumber { get; set; }

        [Display(Name ="Your Address")]
        public string UAddress { get; set; }

        public string Password { get; set; }
        public object Account { get; internal set; }
        
        //[NotMapped]
        //public List<NameDropDown> NameDropDownProperty { get; set; }
    }

    //public class NameDropDown
    //{
    //    public int UserId { get; set; }
    //    public string FirstName { get; set; }
    //}
}
