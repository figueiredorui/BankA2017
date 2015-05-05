using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Models.Reports
{
    public partial class RunningBalance
    {
        public string MonthYear { get { return String.Format("{0}/{1}", TransactionDate.Month, TransactionDate.Year); } }
        public DateTime TransactionDate { get; set; }
        public decimal RunningAmount { get; set; }
    }
}
