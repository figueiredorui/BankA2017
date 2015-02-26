using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankA.Models.Transactions
{
    public class TransactionSearch
    {
        public int? AccountID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tag { get; set; }
    }
}
