using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using System.IO;
using BankA.Models.Transactions;
using BankA.Services.Statements;

namespace BankA.Services.Transactions
{
    public class TransactionService : BankA.Services.Transactions.ITransactionService
    {
        TransactionRepository transactionRepository = null;
        
        public TransactionService()
        {
            transactionRepository = new TransactionRepository();
        }

        public Transaction Find(int transactionId)
        {
            var transaction = transactionRepository.Find(transactionId);

            return transaction.ToModel();
        }

        public List<Transaction> GetTransactions(int? accountID, DateTime startDate, DateTime endDate, string tag)
        {
            if (accountID.HasValue && accountID.Value == 0)
                accountID = null;

            var transactionLst = transactionRepository.Table.Where(q => 
                                q.AccountID == (accountID ?? q.AccountID) &&
                                q.Tag == (tag ?? q.Tag) && 
                                q.TransactionDate >= startDate && 
                                q.TransactionDate <= endDate).OrderByDescending(o=>o.TransactionDate).ToList();

            return transactionLst.ToModel();
        }

        public List<string> GetTags()
        {
            var tagLst = transactionRepository.GetTags();

            return tagLst.OrderBy(q => q).ToList();
        }

        public void Update(Transaction model)
        {
            var transaction = model.ToTable();
            if (transaction.Tag == null)
                transaction.Tag = string.Empty;

            transactionRepository.Update(transaction);
        }

        public void Add(Transaction model)
        {
            var transaction = model.ToTable();
            if (transaction.Tag == null)
                transaction.Tag = string.Empty;

            transactionRepository.Add(transaction);
        }
    }
}
