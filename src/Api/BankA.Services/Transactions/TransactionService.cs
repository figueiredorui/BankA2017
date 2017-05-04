using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankA.Data.Entities;
using BankA.Data.Repositories;
using BankA.Models;
using System.IO;
using BankA.Models.Transactions;
using BankA.Services.Files;

namespace BankA.Services.Transactions
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

            return MapToModel(transaction);
        }

        public List<Transaction> GetTransactions(int? accountID, string description)
        {
            if (accountID.HasValue && accountID.Value == 0)
                accountID = null;

            var query = transactionRepository.Table.Where(q => q.AccountID == (accountID ?? q.AccountID));

            if (!string.IsNullOrEmpty(description))
                query = query.Where(q => q.Description.Contains(description));

            var transactionLst = query.OrderByDescending(o => o.TransactionDate)
                                        .ThenByDescending(o => o.ID).ToList();

            return MapToModel(transactionLst);
        }

        public void Update(Transaction model)
        {
            var transaction = MapToTable(model);

            transactionRepository.Update(transaction);
        }

        public void Add(Transaction model)
        {
            var transaction = MapToTable(model);
            if (transaction.Tag == null)
                transaction.Tag = string.Empty;

            transactionRepository.Add(transaction);
        }

        #region Model Mapping
        private Transaction MapToModel(BankTransaction table)
        {
            return new Transaction()
            {
                ID = table.ID,
                AccountID = table.AccountID,
                TransactionDate = table.TransactionDate,
                Description = table.Description,
                DebitAmount = table.DebitAmount,
                CreditAmount = table.CreditAmount,
                Amount = table.CreditAmount - table.DebitAmount,
                IsTransfer = table.IsTransfer,
                Tag = table.Tag,
                TagGroup = table.TagGroup,
                FileID = table.FileID,
            };
        }

        private List<Transaction> MapToModel(List<BankTransaction> tableLst)
        {
            var lst = new List<Transaction>();
            tableLst.ForEach(i => lst.Add(MapToModel(i)));
            return lst;
        }

        private BankTransaction MapToTable(Transaction model)
        {
            return new BankTransaction()
            {
                ID = model.ID,
                AccountID = model.AccountID,
                TransactionDate = model.TransactionDate,
                Description = model.Description,
                DebitAmount = model.Amount < 0 ? -model.Amount : 0,
                CreditAmount = model.Amount > 0 ? model.Amount : 0,
                IsTransfer = model.IsTransfer,
                Tag = model.Tag ?? string.Empty,
                TagGroup = model.TagGroup ?? string.Empty,
                FileID = model.FileID,
            };
        } 
        #endregion
    }
}
