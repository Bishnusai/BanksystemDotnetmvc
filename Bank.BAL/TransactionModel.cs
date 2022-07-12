using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.BAL
{
  public  class TransactionModel
    {

        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int? AccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdrawl { get; set; }
        public string transactionType { get; set; }
        //[DataType(DataType.DateTime)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[BindProperty]
        public DateTime TransactionDate { get; set; }
        public AccountModel Account { get; set; }
        public UserModel User { get; set; }
        public List<UserModel> UserList { get; set; }

        public List<AccountModel> AccountList { get; set; }
        public List<CashTransactionModel> CashTransactionsList { get; set; }
        public CashTransactionModel CashTransaction { get; set; }

    }
}
