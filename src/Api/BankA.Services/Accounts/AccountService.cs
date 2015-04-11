using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models;
using BankA.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Accounts
{
    public class AccountService : IAccountService
    {
        AccountRepository accountRepository = null;
        TransactionRepository transactionRepository = null;

        public AccountService()
        {
            accountRepository = new AccountRepository();
            transactionRepository = new TransactionRepository();
        }

        public Account Find(int id)
        {
            var bank = accountRepository.Find(id);
            return AccountMapper.Map(bank);
        }

        public List<Account> GetList()
        {
            var bank = accountRepository.Table.ToList();
            return AccountMapper.Map(bank);
        }

        public void Add(Account model)
        {
            var bank = AccountMapper.Map(model);
            accountRepository.Add(bank);
        }

        public void Update(Account model)
        {
            var bank = AccountMapper.Map(model);
            accountRepository.Update(bank);
        }

        public void Delete(int id)
        {
            var bank = accountRepository.Find(id);
            accountRepository.Delete(bank);
        }

        public List<AccountSummary> GetAccountSummary()
        {
            var accountLst = (from account in accountRepository.Table
                          select new AccountSummary()
                          {
                              AccountID = account.AccountID,
                              Description = account.Description,
                              Balance = (decimal?)account.Transactions.Sum(o => o.CreditAmount - o.DebitAmount) ?? 0,
                              LastTransactionDate = (DateTime?)account.Transactions.Max(o => o.TransactionDate)?? null
                          }).ToList();

            var total = new AccountSummary() { AccountID = 0, Description = "All Accounts", Balance = accountLst.Sum(q => q.Balance), LastTransactionDate = null };

            accountLst.Add(total);

            return accountLst.OrderBy(o => o.AccountID).ToList();
        }
    }
}
