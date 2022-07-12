using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.BAL
{
  public  class CashTransactionModel
    {
        public int CashId { get; set; }
        public int TransactionId { get; set; }
        public decimal Cash { get; set; }
        public int Count { get; set; }
        public int C100 { get; set; }
        public int C200 { get; set; }
        public int C500 { get; set; }
        public int C2000 { get; set; }
        public AccountModel Account { get; set; }
        public List<AccountModel> AccountList { get; set; }
        public TransactionModel Transactions { get; set; }
        public List<TransactionModel> TransactionsList { get; set; }
       

    }
}
