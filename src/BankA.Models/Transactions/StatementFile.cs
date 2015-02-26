using BankA.Models.Enums;
using System;
using System.Collections.Generic;

namespace BankA.Models.Transactions
{
    public partial class StatementFile
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }
        public int AccountID { get; set; }
    }
}
