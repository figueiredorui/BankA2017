using AutoMapper;
using BankA.Data.Models;
using BankA.Models;
using BankA.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Accounts
{
    public class AccountMapper
    {
        static AccountMapper()
        {
            Mapper.CreateMap<BankAccount, Account>();

            Mapper.CreateMap<Account, BankAccount>();
        }

        public static Account Map(BankAccount entity)
        {
            return Mapper.Map<Account>(entity);
        }

        public static List<Account> Map(List<BankAccount> entityLst)
        {
            return Mapper.Map<List<Account>>(entityLst);
        }

        public static BankAccount Map(Account entity)
        {
            return Mapper.Map<BankAccount>(entity);
        }
    }
}
