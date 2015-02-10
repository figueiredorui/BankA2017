using BankA.Data.Repositories;
using BankA.Models;
using BankA.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services
{
    public class AccountService
    {
        AccountRepository accountRepository = null;

        public AccountService()
        {
            accountRepository = new AccountRepository();
        }

        public Account Find(int id)
        {
            var bank = accountRepository.Find(id);
            return AccountMapper.Map(bank);
        }

        public List<Account> GetList()
        {
            var bank = accountRepository.GetList();
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
    }
}
