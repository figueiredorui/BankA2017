using System;
using System.Collections.Generic;

namespace BankA.Data.Models
{
    public partial class BankAccountTable
    {
        public int AccountID { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string ChangedBy { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
