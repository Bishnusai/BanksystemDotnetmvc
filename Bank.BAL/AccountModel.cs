using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bank.BAL
{
  public  class AccountModel
    {
        public int AccountId { get; set; }

        public int UserId { get; set; }
        public int BranchId { get; set; }
        //[Required(ErrorMessage ="BranchName required")]
        //[Display(Name ="Branch Name: ")]
        //public string BranchName { get; set; }
        [Required]
        [Display(Name ="Account Number: ")]
        public long AccountNumber { get; set; }
        [Required]
        [Display(Name ="Minimum Balance:")]
        public decimal MinimumBalance { get; set; }
        public decimal WithdrwaLimit { get; set; }
        public decimal AccountBalance { get; set; }
        public List<AccountModel> AccountList { get; set; }

        public UserModel User { get; set; }
        public List<UserModel> UserList { get; set; }

        public BranchModel Branch { get; set; }
        public List<BranchModel> BranchList { get; set; }

        public TransactionModel Transaction { get; set; }
    }
}
