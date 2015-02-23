using System;
using System.Collections.Generic;

namespace BankA.Data.Models
{
    public partial class StatementFile
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string ContentType { get; set; }
    }
}
