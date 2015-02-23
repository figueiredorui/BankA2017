using BankA.Data.Repositories;
using BankA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Accounts
{
    public class AccountService
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
            var result = (from transaction in transactionRepository.Table
                          group transaction by new { transaction.AccountID, transaction.Account.Description } into grp
                          select new AccountSummary()
                          {
                              AccountID = grp.Key.AccountID,
                              Description = grp.Key.Description,
                              Balance = grp.Sum(o => o.CreditAmount - o.DebitAmount),
                              LastTransactionDate = grp.Max(o => o.TransactionDate),
                          }).ToList();

            return result;
        }
    }
}
