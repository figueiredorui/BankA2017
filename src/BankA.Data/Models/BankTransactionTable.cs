using System;
using System.Collections.Generic;

namespace BankA.Data.Models
{
    public partial class BankTransactionTable
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Tag { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string ChangedBy { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual BankAccountTable Account { get; set; }
    }
}
