using BankA.Models.Accounts;
using System;
using System.Collections.Generic;
namespace BankA.Services.Accounts
{
    public interface IAccountService
    {
        void Add(Account model);
        void Delete(int id);
        Account Find(int id);
        List<AccountSummary> GetAccountSummary();
        List<Account> GetList();
        void Update(Account model);
    }
}
