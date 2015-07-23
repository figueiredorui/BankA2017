using BankA.Data.Models;
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

        public RulesService()
        {
            transactionRuleRepository = new TransactionRuleRepository();
        }

        public TransactionRule Find(int id)
        {
            var entity = transactionRuleRepository.Find(id);
            return entity.ToModel();
        }

        public List<TransactionRule> GetList()
        {
            var entity = transactionRuleRepository.Table.ToList();
            return entity.ToModel();
        }

        public void Add(TransactionRule model)
        {
            var entity = model.ToTable();
            transactionRuleRepository.Add(entity);
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
