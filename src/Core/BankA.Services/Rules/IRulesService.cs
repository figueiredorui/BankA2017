using BankA.Models.Accounts;
using BankA.Models.Transactions;
using System;
using System.Collections.Generic;
namespace BankA.Services.Rules
{
    public interface IRulesService
    {
        void Add(TransactionRule model);
        void Delete(int id);
        TransactionRule Find(int id);
        List<TransactionRule> GetList();
        void Update(TransactionRule model);
    }
}
