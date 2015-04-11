using System;
using System.Collections.Generic;

namespace BankA.Data.Models
{
    public partial class StatementFileTable
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string ChangedBy { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
