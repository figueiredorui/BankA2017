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
    public class RulesService
    {
        TransactionRuleRepository ruleRepository = null;
        TransactionRepository transactionRepository = null;

        public RulesService()
        {
            ruleRepository = new TransactionRuleRepository();
            transactionRepository = new TransactionRepository();
        }

        public TransactionRule Find(int id)
        {
            var entity = ruleRepository.Find(id);
            return MapToModel(entity);
        }

        public List<TransactionRule> GetList()
        {
            var entity = ruleRepository.Table
                                                .OrderBy(o=>o.TagGroup)
                                                .ThenBy(o=>o.Tag)
                                                .ThenBy(o=>o.Description)
                                                .ToList();
            return MapToModel(entity);
        }

        public List<string> GetTags()
        {
            var result = ruleRepository.Table.Select(o => o.Tag).Distinct().OrderBy(q => q).ToList();
            return result;
        }
        
        public List<string> GetGroups()
        {
            var result = ruleRepository.Table.Select(o => o.TagGroup).Distinct().OrderBy(q => q).ToList();
            return result;
        }

        public void Add(TransactionRule model)
        {
            var entity = MapToTable(model);

            ValidateIfExists(entity);
            ruleRepository.Add(entity);

            ApplyRuleToExistingTransactions(entity);
        }

        public void Update(TransactionRule model)
        {
            var entity = MapToTable(model);
            ruleRepository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = ruleRepository.Find(id);
            ruleRepository.Delete(entity);
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
            var exists = ruleRepository.Table.Any(q => q.Description == entity.Description);
            if (exists)
                throw new Exception(string.Format("Rule for '{0}' already exists.", entity.Description));
        }

        #region Model mapping
        private TransactionRule MapToModel(BankTransactionRule table)
        {
            return new TransactionRule()
            {
                RuleID = table.RuleID,
                Description = table.Description,
                Tag = table.Tag,
                TagGroup = table.TagGroup,
                IsTransfer = table.IsTransfer
            };
        }

        private List<TransactionRule> MapToModel(List<BankTransactionRule> tableLst)
        {
            var lst = new List<TransactionRule>();
            tableLst.ForEach(i => lst.Add(MapToModel(i)));
            return lst;
        }

        private BankTransactionRule MapToTable(TransactionRule model)
        {
            return new BankTransactionRule()
            {
                RuleID = model.RuleID,
                Description = model.Description,
                Tag = model.Tag,
                TagGroup = model.TagGroup,
                IsTransfer = model.IsTransfer
            };
        } 
        #endregion
    }
}
