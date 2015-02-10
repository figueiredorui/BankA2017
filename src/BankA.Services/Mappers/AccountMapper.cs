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
            Mapper.CreateMap<BankAccountTable, Account>();

            Mapper.CreateMap<Account, BankAccountTable>();
        }

        public static Account Map(BankAccountTable entity)
        {
            return Mapper.Map<Account>(entity);
        }

        public static List<Account> Map(List<BankAccountTable> entityLst)
        {
            return Mapper.Map<List<Account>>(entityLst);
        }

        public static BankAccountTable Map(Account entity)
        {
            return Mapper.Map<BankAccountTable>(entity);
        }
    }
}
