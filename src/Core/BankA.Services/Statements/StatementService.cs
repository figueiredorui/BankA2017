﻿using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Import;
using BankA.Services.Statements;
using BankA.Services.Statements.Maps;
using CsvHelper;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Statements
{
    public class StatementService : BankA.Services.Statements.IStatementService
    {
        private readonly TransactionRepository transactionRepository;
        private readonly AccountRepository accountRepository;

        public StatementService()
        {
            transactionRepository = new TransactionRepository();
            accountRepository = new AccountRepository();
        }

        public void ImportFile(StatementFile statement)
        {
            var statementRows = ReadStatementFile(statement);
            if (statementRows.Any())
                ImportFile(statement, statementRows);
        }

        private List<StatementRow> ReadStatementFile(StatementFile statement)
        {
            try
            {
                var statementRows = new List<StatementRow>();
                Stream stream = new MemoryStream(statement.FileContent);
                using (var reader = new CsvReader(new StreamReader(stream)))
                {
                    reader.Configuration.HasHeaderRecord = false;
                    Type statementMap = GetStatementMap(statement.AccountID);
                    reader.Configuration.RegisterClassMap(statementMap);
                    statementRows = reader.GetRecords<StatementRow>().ToList();
                }
                return statementRows;
            }
            catch (CsvTypeConverterException ex)
            {
                var msg = ex.Data["CsvHelper"];
                throw new Exception(msg.ToString(), ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImportFile(StatementFile statementFile, List<StatementRow> statementRows)
        {
            var transactionLst = new List<BankTransaction>();
            var historyLst = GetTagHistory();

            foreach (var row in statementRows)
            {
                transactionLst.Add(CreateTransaction(statementFile, row, historyLst));
            }

            transactionRepository.AddBatch(transactionLst);
        }

        private BankTransaction CreateTransaction(StatementFile file, StatementRow row, List<BankTransaction> historyLst)
        {
            var trans = new BankTransaction();

            trans.AccountID = file.AccountID;
            trans.FileID = file.FileID;

            trans.TransactionDate = row.TransactionDate;
            trans.Description = row.Description;
            trans.DebitAmount = row.DebitAmount;
            trans.CreditAmount = row.CreditAmount;

            trans.Tag = GetBestTag(trans.Description, historyLst);

            return trans;
        }

        private string GetBestTag(string description, List<BankTransaction> historyLst)
        {
            string tag = "NA";

            var matchLst = new Dictionary<BankTransaction, int>();
            foreach (var item in historyLst)
            {
                int compareDistance = Convert.ToInt32((decimal)description.Length / 2);
                var distance = LevenshteinDistance.Compute(description, item.Description);
                if (distance <= compareDistance)
                    matchLst.Add(item, distance);
            }

            var bestMatch = matchLst.Where(q => q.Value == matchLst.Values.Min()).FirstOrDefault();
            var key = ((BankTransaction)bestMatch.Key);

            if (key != null)
                tag = key.Tag;

            return tag;
        }

        private List<BankTransaction> GetTagHistory()
        {
            return transactionRepository.Table.ToList();
        }

        private Type GetStatementMap(int accountID)
        {
            try
            {
                var account = accountRepository.Find(accountID);
                var bank = (BankEnum)Enum.Parse(typeof(BankEnum), account.BankName);

                return FindMap(bank);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private Type FindMap(BankEnum bank)
        {
            Type typeFound = null;
            Assembly a = Assembly.GetExecutingAssembly();
            var types = a.GetTypes().Where(t => typeof(IStatementMap).IsAssignableFrom(t));
            foreach (var type in types)
            {
                var att1 = type.GetCustomAttributes<BankNameAttribute>(false).Any(q => q.BankName == bank);
                if (att1)
                {
                    typeFound = type;
                    break;
                }
            }
            return typeFound;
        }
    }
}