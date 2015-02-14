using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
        public bool IsSavingsAccount { get; set; }
    }
}
