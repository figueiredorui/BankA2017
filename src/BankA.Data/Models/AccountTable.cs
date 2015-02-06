using System;
using System.Collections.Generic;

namespace BankA.Data.Models
{
    public partial class AccountTable
    {
        public int AccountID { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
    }
}
