using AutoMapper;
using BankA.Data.Models;
using BankA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Mappers
{
    public class AccountMapper
    {
        static AccountMapper()
        {
            Mapper.CreateMap<AccountTable, Account>();

            Mapper.CreateMap<Account, AccountTable>();
        }

        public static Account Map(AccountTable entity)
        {
            return Mapper.Map<Account>(entity);
        }

        public static List<Account> Map(List<AccountTable> entityLst)
        {
            return Mapper.Map<List<Account>>(entityLst);
        }

        public static AccountTable Map(Account entity)
        {
            return Mapper.Map<AccountTable>(entity);
        }
    }
}
