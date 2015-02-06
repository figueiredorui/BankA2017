using System;
using System.Collections.Generic;

namespace BankA.Data.Models
{
    public partial class TransactionTable
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public System.DateTime TransationDate { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Tag { get; set; }
    }
}
