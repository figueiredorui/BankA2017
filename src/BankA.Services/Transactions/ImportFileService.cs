using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Enums;
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
    public class StatementFileService
    {
        private readonly TransactionRepository transactionRepository;
        private readonly AccountRepository accountRepository;

        public StatementFileService()
        {
            transactionRepository = new TransactionRepository();
            accountRepository = new AccountRepository();
        }

        public void Import(BankEnum bank, int accountID, string path)
        {
            if (bank == BankEnum.HSBC)
            {
                var engine = new FileHelperEngine(typeof(FileHSBC));
                var lst = engine.ReadFile(path) as FileHSBC[];
                ImportFile(accountID, lst);
            }

            if (bank == BankEnum.LLOYDS)
            {
                var engine = new FileHelperEngine(typeof(FileLLOYDS));
                var lst = engine.ReadFile(path) as FileLLOYDS[];
                ImportFile(accountID, lst);
            }
        }

        public void Import(int accountID, TextReader path)
        {
            var account = accountRepository.Find(accountID);

            if (account.BankName == BankEnum.HSBC.ToString())
            {
                var engine = new FileHelperEngine(typeof(FileHSBC));
                var lst = engine.ReadStream(path) as FileHSBC[];
                ImportFile(accountID, lst);
            }

            if (account.BankName == BankEnum.LLOYDS.ToString())
            {
                var engine = new FileHelperEngine(typeof(FileLLOYDS));
                var lst = engine.ReadStream(path) as FileLLOYDS[];
                ImportFile(accountID, lst);
            }
        }

        private void ImportFile(int accountID, FileBank[] lst)
        {
            var transactionLst = new List<BankTransactionTable>();
            var historyLst = transactionRepository.Table.Where(q => q.AccountID == accountID).ToList();

            foreach (var row in lst)
            {
                transactionLst.Add(MapColumns(accountID, row, historyLst));
            }

            transactionRepository.AddBatch(transactionLst);
        }

        private BankTransactionTable MapColumns(int accountID, FileBank row, List<BankTransactionTable> accountTransLst)
        {
            BankTransactionTable trans = null;

            if (row is FileHSBC)
                trans = MapColumnsHSBC(accountID, (FileHSBC)row);
            else if (row is FileLLOYDS)
                trans = MapColumnsLLOYDS(accountID, (FileLLOYDS)row);

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

        private BankTransactionTable MapColumnsHSBC(int accountID, FileHSBC row)
        {
            var trans = new BankTransactionTable();

            trans.AccountID = accountID;
            trans.TransactionDate = row.TransactionDate;
            trans.Description = row.Description;

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

        private BankTransactionTable MapColumnsLLOYDS(int accountID, FileLLOYDS row)
        {
            var trans = new BankTransactionTable();

            trans.AccountID = accountID;
            trans.TransactionDate = row.TransactionDate;
            trans.Description = row.Description;

            trans.DebitAmount = row.DebitAmount;
            trans.CreditAmount = row.CreditAmount;

            if (row.Description.Length > 4)
                trans.Tag = row.Description.Substring(0, 4);
            else
                trans.Tag = row.Description;

            return trans;
        }
    }

    static class LevenshteinDistance
    {
        public static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }
    }

    [DelimitedRecord(",")]
    class FileBank
    {
    }

    [DelimitedRecord(",")]
    class FileHSBC : FileBank
    {
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime TransactionDate;

        public string Description;

        public decimal Amount;
    }

    [DelimitedRecord(",")]
    class FileLLOYDS : FileBank
    {
        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime TransactionDate;

        public string Type;
        public string SortCode;
        public string AccountNo;

        public string Description;

        [FieldNullValue(typeof(decimal), "0")]
        public decimal DebitAmount;

        [FieldNullValue(typeof(decimal), "0")]
        public decimal CreditAmount;

        [FieldNullValue(typeof(decimal), "0")]
        public decimal Balance;

    }

}
