using BankA.Data.Entities;
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
            return MapToModel(bank);
        }

        public List<Account> GetList()
        {
            var bank = accountRepository.Table.Where(q => q.Closed == false).ToList();
            return MapToModel(bank);
        }

        public List<AccountSummary> GetAccountSummary()
        {
            var accountLst = (from account in accountRepository.Table.Where(q=>q.Closed == false)
                              select new AccountSummary()
                              {
                                  AccountID = account.AccountID,
                                  Description = account.Description,
                                  Balance = (decimal?)account.BankTransactions.Sum(o => o.CreditAmount - o.DebitAmount) ?? 0,
                                  LastTransactionDate = (DateTime?)account.BankTransactions.Max(o => o.TransactionDate) ?? null
                              }).ToList();

            var total = new AccountSummary() { AccountID = null, Description = "All Accounts", Balance = accountLst.Sum(q => q.Balance), LastTransactionDate = null };

            accountLst.Add(total);

            return accountLst.OrderBy(o => o.AccountID).ToList();
        }

        public void Add(Account model)
        {
            var bank = MapToTable(model);
            accountRepository.Add(bank);
        }

        public void Update(Account model)
        {
            var bank = MapToTable(model);
            accountRepository.Update(bank);
        }

        public void Delete(int id)
        {
            accountRepository.DeleteAccountAndTransactions(id);
        }

        private Account MapToModel(BankAccount table)
        {
            return new Account()
            {
                AccountID = table.AccountID,
                Description = table.Description,
                BankName = table.BankName,
                IsSavingsAccount = table.IsSavingsAccount
            };
        }

        private List<Account> MapToModel(List<BankAccount> tableLst)
        {
            var lst = new List<Account>();
            tableLst.ForEach(i => lst.Add(MapToModel(i)));
            return lst;
        }

        private BankAccount MapToTable(Account entity)
        {
            return new BankAccount()
            {
                AccountID = entity.AccountID,
                Description = entity.Description,
                BankName = entity.BankName,
                IsSavingsAccount = entity.IsSavingsAccount
            };
        }
    }
}
