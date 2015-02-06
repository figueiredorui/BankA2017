using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Services.Mappers;

namespace BankA.Services
{
    public class TransactionService
    {
        TransactionRepository transactionRepository = null;
        
        public TransactionService()
        {
            transactionRepository = new TransactionRepository();
        }

        public Transaction Find(int transactionId)
        {
            var transaction = transactionRepository.Find(transactionId);

            return TransactionMapper.Map(transaction);
        }

        public List<Transaction> GetTransactionByAccount(Int64 accountID, DateTime startDate, DateTime endDate)
        {
            var transactionLst = transactionRepository.GetList(q => q.AccountID == accountID && q.TransationDate >= startDate && q.TransationDate <= endDate );

            return TransactionMapper.Map(transactionLst);
        }

        public List<Transaction> GetTransactions(DateTime startDate, DateTime endDate)
        {
            var transactionLst = transactionRepository.GetList(q => q.TransationDate >= startDate && q.TransationDate <= endDate);

            return TransactionMapper.Map(transactionLst);
        }

        public List<string> GetTagsByAccount(Int64 accountID)
        {
            var tagLst = transactionRepository.GetList(q => q.AccountID == accountID).Select(q => q.Tag).Distinct().ToList();

            return tagLst.OrderBy(q => q).ToList();
        }

        public void Update(Transaction model)
        {
            var transaction = TransactionMapper.Map(model);
            if (model.Tag == null)
                model.Tag = string.Empty;

            transactionRepository.Update(transaction);
        }

    }
}
