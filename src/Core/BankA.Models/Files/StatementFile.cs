using BankA.Models.Enums;
using System;
using System.Collections.Generic;

namespace BankA.Models.Files
{
    public partial class StatementImport
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }
        public int AccountID { get; set; }

    }

    public partial class StatementFile
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Account { get; set; }

    }
}
