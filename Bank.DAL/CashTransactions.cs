//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bank.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashTransactions
    {
        public int CashId { get; set; }
        public int TransactionId { get; set; }
        public decimal Cash { get; set; }
        public int Count { get; set; }
        public int C100 { get; set; }
        public int C200 { get; set; }
        public int C500 { get; set; }
        public int C2000 { get; set; }
    
        public virtual Transaction Transaction { get; set; }
    }
}
