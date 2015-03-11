using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Import;
using BankA.Services.StatementFiles;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.StatementFiles
{
    public class StatementFileService
    {
        private readonly TransactionRepository transactionRepository;
        private readonly AccountRepository accountRepository;

        public StatementFileService()
        {
            transactionRepository = new TransactionRepository();
            accountRepository = new AccountRepository();
        }

        public void Import(StatementFile statement)
        {
            ReadStatementFile(statement);
            if (statement.Rows.Any())
                ImportFile(statement);
        }

        private void ReadStatementFile(StatementFile statement)
        {
            //var account = accountRepository.Find(statement.AccountID);
            //statement.Rows = new List<StatementRow>();
            Stream stream = new MemoryStream(statement.FileContent);
            using (var reader = new CsvReader(new StreamReader(stream)))
            {
                reader.Configuration.RegisterClassMap<HsbcStatementMap>();
                statement.Rows = reader.GetRecords<StatementRow>().ToList();
            }
        }

        private void ImportFile(StatementFile statementFile)
        {
            var transactionLst = new List<BankTransactionTable>();
            var historyLst = GetTagHistory();

            foreach (var row in statementFile.Rows)
            {
                transactionLst.Add(MapColumns(statementFile, row, historyLst));
            }

            transactionRepository.AddBatch(transactionLst);
        }

        private BankTransactionTable MapColumns(StatementFile file, StatementRow row, List<BankTransactionTable> historyLst)
        {
            var trans = new BankTransactionTable();

            trans.AccountID = file.AccountID;
            trans.FileID = file.FileID;

            trans.TransactionDate = row.TransactionDate;
            trans.Description = row.Description;
            trans.DebitAmount = row.DebitAmount;
            trans.CreditAmount = row.CreditAmount;

            trans.Tag = GetBestTag(trans.Description, historyLst);

            return trans;
        }

        private string GetBestTag(string description, List<BankTransactionTable> historyLst)
        {
            string tag = "NA";

            Dictionary<BankTransactionTable, int> matchLst = new Dictionary<BankTransactionTable, int>();
            foreach (var item in historyLst)
            {
                int compareDistance = Convert.ToInt32((decimal)description.Length / 2);
                var distance = LevenshteinDistance.Compute(description, item.Description);
                if (distance <= compareDistance)
                    matchLst.Add(item, distance);
            }

            var bestMatch = matchLst.Where(q => q.Value == matchLst.Values.Min()).FirstOrDefault();
            var key = ((BankTransactionTable)bestMatch.Key);

            if (key != null)
                tag = key.Tag;

            return tag;
        }

        private List<BankTransactionTable> GetTagHistory()
        {
            return transactionRepository.Table.ToList();
        }

    }

   

    

    

}
