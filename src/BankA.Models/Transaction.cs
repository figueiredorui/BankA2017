using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Models
{
    public partial class Transaction
    {
        public Int64 ID { get; set; }
        public Int64 AccountID { get; set; }
        public DateTime TransationDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string YearMonth { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Tag { get; set; }
    }

}
