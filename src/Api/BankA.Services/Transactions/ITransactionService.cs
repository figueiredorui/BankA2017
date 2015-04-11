using BankA.Models.Transactions;
using System;
using System.Collections.Generic;
namespace BankA.Services.Transactions
{
    public interface ITransactionService
    {
        void Add(Transaction model);
        Transaction Find(int transactionId);
        List<string> GetTags();
        List<Transaction> GetTransactions(int? accountID, DateTime startDate, DateTime endDate, string tag);
        void Update(Transaction model);
    }
}
