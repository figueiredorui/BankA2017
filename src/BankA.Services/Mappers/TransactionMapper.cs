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
    public class TransactionMapper
    {
        static TransactionMapper()
        {
            Mapper.CreateMap<BankTransactionTable, Transaction>();

            Mapper.CreateMap<Transaction, BankTransactionTable>();
        }

        public static Transaction Map(BankTransactionTable entity)
        {
            return Mapper.Map<Transaction>(entity);
        }

        public static List<Transaction> Map(List<BankTransactionTable> entityLst)
        {
            return Mapper.Map<List<Transaction>>(entityLst);
        }

        public static BankTransactionTable Map(Transaction entity)
        {
            return Mapper.Map<BankTransactionTable>(entity);
        }

    }
}
