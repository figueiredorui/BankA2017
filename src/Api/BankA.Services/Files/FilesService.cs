using BankA.Data.Entities;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Import;
using BankA.Services.Files;
using BankA.Services.Files.Maps;
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
using BankA.Models.Files;

namespace BankA.Services.Files
{
    public class FilesService 
    {
        private readonly TransactionRepository transactionRepository;
        private readonly TransactionRuleRepository transactionRuleRepository;
        private readonly AccountRepository accountRepository;
        private readonly FileRepository statementFileRepository;
        

        public FilesService()
        {
            transactionRepository = new TransactionRepository();
            transactionRuleRepository = new TransactionRuleRepository();
            accountRepository = new AccountRepository();
            statementFileRepository = new FileRepository();
        }

        public void ImportFile(StatementImport statement)
        {
            var statementRows = ReadStatementFile(statement);
            if (statementRows.Any())
                ImportFile(statement, statementRows);
        }

        private List<StatementRow> ReadStatementFile(StatementImport statement)
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

        private void ImportFile(StatementImport statementFile, List<StatementRow> statementRows)
        {
            var bankFile = CreateBankStatmentFile(statementFile);
            var bankTransaction = CreateBankTransaction(statementRows);

            transactionRepository.AddTransactions(bankFile, bankTransaction);
        }

        private BankFile CreateBankStatmentFile(StatementImport statementFile)
        {
            return new BankFile()
            {
                AccountID = statementFile.AccountID,
                FileName = statementFile.FileName,
                FileContent = statementFile.FileContent,
                ContentType = statementFile.ContentType
            };
        }

        private List<BankTransaction> CreateBankTransaction(List<StatementRow> statementRows)
        {
            

            var transactionLst = new List<BankTransaction>();
            foreach (var row in statementRows)
            {
                var transaction = CreateBankTransaction(row);
                transactionLst.Add(transaction);
            }

            ApplyBestRule(transactionLst);
                
            return transactionLst;
        }

       
        private BankTransaction CreateBankTransaction(StatementRow row)
        {
            var trans = new BankTransaction();

            trans.TransactionDate = row.TransactionDate;
            trans.TransactionType = row.Type;
            trans.Description = row.Description;
            trans.DebitAmount = row.DebitAmount;
            trans.CreditAmount = row.CreditAmount;

            return trans;
        }

        private void ApplyBestRule(List<BankTransaction> transactionLst)
        {
            var transactionRules = transactionRuleRepository.Table.ToList();

            foreach (var transaction in transactionLst)
            {
                var rule = transactionRules.Where(q => transaction.Description.ToUpper().Contains(q.Description.ToUpper())).FirstOrDefault();
                if (rule != null)
                {
                    transaction.Tag = rule.Tag;
                    transaction.TagGroup = rule.TagGroup;
                    transaction.IsTransfer = rule.IsTransfer;
                }
            }
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

        public void Delete(int id)
        {
            var entity = statementFileRepository.Find(id);

            var transactionList = transactionRepository.Table.Where(q => q.FileID == id).ToList();
            foreach (var transaction in transactionList)
            {
                transactionRepository.Delete(transaction);
            }

            statementFileRepository.Delete(entity);
        }

        public List<StatementFile> GetList()
        {
            var entity = statementFileRepository.Table.ToList();
            return ToModel(entity);
        }


        public static StatementFile ToModel(BankFile table)
        {
            return new StatementFile()
            {
                FileID = table.FileID,
                FileName = table.FileName,
                CreatedOn = table.CreatedOn,
                Account = table.BankAccount.Description
            };
        }

        public static List<StatementFile> ToModel(List<BankFile> tableLst)
        {
            var lst = new List<StatementFile>();
            tableLst.ForEach(i => lst.Add(ToModel(i)));
            return lst;
        }

        public static BankFile ToTable(StatementFile model)
        {
            return new BankFile()
            {
                FileID = model.FileID,
                FileName = model.FileName,
                CreatedOn = model.CreatedOn,
            };
        }
    }
}
