using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bank.BAL
{
   public class LoginModel
    {
        [Required(ErrorMessage = "Give Valid Email")]
        [Display(Name = "EMail: ")]
        [EmailAddress]
        public string UEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
