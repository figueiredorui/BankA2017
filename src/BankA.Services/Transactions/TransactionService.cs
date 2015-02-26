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

namespace BankA.Services.Transactions
{
    public class TransactionService
    {
        TransactionRepository transactionRepository = null;
        StatementFileRepository statementFileRepository = null;
        
        public TransactionService()
        {
            transactionRepository = new TransactionRepository();
            statementFileRepository = new StatementFileRepository();
        }

        public Transaction Find(int transactionId)
        {
            var transaction = transactionRepository.Find(transactionId);

            return transaction.ToModel();
        }

        public List<Transaction> GetTransactions(int? accountID, DateTime startDate, DateTime endDate, string tag)
        {
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

        public void Upload(StatementFile statementFile)
        {
            var statement = StatementFileMapper.Map(statementFile);
            statementFileRepository.Add(statement);

            Stream stream = new MemoryStream(statement.FileContent);
            TextReader  r = new StreamReader(stream);


            new StatementFileService().Import(statementFile.AccountID, r );
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
