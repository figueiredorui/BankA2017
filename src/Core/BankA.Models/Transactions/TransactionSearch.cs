using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankA.Models.Transactions
{
    public class TransactionSearch
    {
        public TransactionFilter Filter { get; set; }
        public TransactionPagination Pagination { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class TransactionFilter
    {
        public int AccountID { get; set; }
        public string Description { get; set; }
    }

    public class TransactionPagination
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
    }
}
