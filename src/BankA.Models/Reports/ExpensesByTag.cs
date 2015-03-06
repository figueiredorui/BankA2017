using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Models.Reports
{
    public partial class ExpensesByTag
    {
        public string Tag { get; set; }
        public decimal Amount { get; set; }
    }
}
