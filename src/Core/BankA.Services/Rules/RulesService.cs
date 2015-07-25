using BankA.Data.Entities;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Accounts;
using BankA.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Rules
{
    public class RulesService : IRulesService
    {
        TransactionRuleRepository transactionRuleRepository = null;
        TransactionRepository transactionRepository = null;

        public RulesService()
        {
            transactionRuleRepository = new TransactionRuleRepository();
            transactionRepository = new TransactionRepository();
        }

        public TransactionRule Find(int id)
        {
            var entity = transactionRuleRepository.Find(id);
            return entity.ToModel();
        }

        public List<TransactionRule> GetList()
        {
            var entity = transactionRuleRepository.Table
                                                .OrderBy(o=>o.TagGroup)
                                                .ThenBy(o=>o.Tag)
                                                .ThenBy(o=>o.Description)
                                                .ToList();
            return entity.ToModel();
        }

        public void Add(TransactionRule model)
        {
            var entity = model.ToTable();

            ValidateIfExists(entity);
            transactionRuleRepository.Add(entity);

            ApplyRuleToExistingTransactions(entity);
        }

        private void ApplyRuleToExistingTransactions(BankTransactionRule entity)
        {
            var transactionList = transactionRepository.Table.Where(q => string.IsNullOrEmpty(q.Tag) 
                                                                    && string.IsNullOrEmpty(q.TagGroup)
                                                                    && q.Description.ToUpper().Contains(entity.Description.ToUpper())
                                                                    ).ToList();
            foreach (var transaction in transactionList)
            {
                transaction.Tag = entity.Tag;
                transaction.TagGroup = entity.TagGroup;
                transaction.IsTransfer = entity.IsTransfer;

                transactionRepository.Update(transaction);
            }

        }

        private void ValidateIfExists(BankTransactionRule entity)
        {
            var exists = transactionRuleRepository.Table.Any(q => q.Description == entity.Description);
            if (exists)
                throw new Exception(string.Format("Rule for '{0}' already exists.", entity.Description));
        }

        public void Update(TransactionRule model)
        {
            var entity = model.ToTable();
            transactionRuleRepository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = transactionRuleRepository.Find(id);
            transactionRuleRepository.Delete(entity);
        }
    }
}
