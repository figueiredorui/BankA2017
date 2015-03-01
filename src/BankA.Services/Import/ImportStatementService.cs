using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Import;
using Excel;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Transactions
{
    public class ImportStatementService
    {
        private readonly TransactionRepository transactionRepository;
        private readonly AccountRepository accountRepository;

        public ImportStatementService()
        {
            transactionRepository = new TransactionRepository();
            accountRepository = new AccountRepository();
        }

        public void Import(StatementFile statement)
        {

            Stream stream = new MemoryStream(statement.FileContent);
            TextReader text = new StreamReader(stream);

            var account = accountRepository.Find(statement.AccountID);

            if (account.BankName == BankEnum.HSBC.ToString())
            {
                var engine = new FileHelperEngine(typeof(FileHSBC));
                var lst = engine.ReadStream(text) as FileHSBC[];
                ImportFile(statement, lst);
            }

            if (account.BankName == BankEnum.LLOYDS.ToString())
            {
                var engine = new FileHelperEngine(typeof(FileLLOYDS));
                var lst = engine.ReadStream(text) as FileLLOYDS[];
                ImportFile(statement, lst);
            }
        }

        private void ImportFile(StatementFile statementFile, BankFile[] lst)
        {
            var transactionLst = new List<BankTransactionTable>();
            var historyLst = transactionRepository.Table.Where(q => q.AccountID == statementFile.AccountID).ToList();

            foreach (var row in lst)
            {
                transactionLst.Add(MapColumns(statementFile, row, historyLst));
            }

            transactionRepository.AddBatch(transactionLst);
        }

        private BankTransactionTable MapColumns(StatementFile statementFile, BankFile row, List<BankTransactionTable> accountTransLst)
        {
            BankTransactionTable trans = null;

            if (row is FileHSBC)
                trans = MapColumnsHSBC(statementFile, (FileHSBC)row);
            else if (row is FileLLOYDS)
                trans = MapColumnsLLOYDS(statementFile, (FileLLOYDS)row);

            trans.Tag = GetTags(trans, accountTransLst);

            return trans;
        }

        private string GetTags(BankTransactionTable trans, List<BankTransactionTable> historyLst)
        {
            string tag = "NA";

            Dictionary<BankTransactionTable, int> matchLst = new Dictionary<BankTransactionTable, int>();
            foreach (var item in historyLst)
            {
                int compareDistance = Convert.ToInt32((decimal)trans.Description.Length / 2);
                var distance = LevenshteinDistance.Compute(trans.Description, item.Description);
                if (distance <= compareDistance)
                    matchLst.Add(item, distance);
            }

            var bestMatch = matchLst.Where(q => q.Value == matchLst.Values.Min()).FirstOrDefault();
            var key = ((BankTransactionTable)bestMatch.Key);

            if (key != null)
                tag = key.Tag;

            return tag;
        }

        private BankTransactionTable MapColumnsHSBC(StatementFile statementFile, FileHSBC row)
        {
            var trans = new BankTransactionTable();

            trans.AccountID = statementFile.AccountID;
            trans.TransactionDate = row.TransactionDate;
            trans.Description = row.Description;
            trans.FileID = statementFile.FileID;

            if (row.Amount < 0)
                trans.DebitAmount = Math.Abs(row.Amount);
            else
                trans.CreditAmount = row.Amount;

            if (row.Description.Length > 4)
                trans.Tag = row.Description.Substring(0, 4);
            else
                trans.Tag = row.Description;

            return trans;
        }

        private BankTransactionTable MapColumnsLLOYDS(StatementFile statementFile, FileLLOYDS row)
        {
            var trans = new BankTransactionTable();

            trans.AccountID = statementFile.AccountID;
            trans.TransactionDate = row.TransactionDate;
            trans.Description = row.Description.Trim('"');
            trans.FileID = statementFile.FileID;

            trans.DebitAmount = row.DebitAmount;
            trans.CreditAmount = row.CreditAmount;

            if (row.Description.Length > 4)
                trans.Tag = row.Description.Substring(0, 4);
            else
                trans.Tag = row.Description;

            return trans;
        }
    }

   

    

    

}
