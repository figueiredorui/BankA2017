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
        public int ID { get; set; }
        public int AccountID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Amount { get; set; }
        public string Tag { get; set; }
    }

}
