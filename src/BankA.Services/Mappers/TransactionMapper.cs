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
            Mapper.CreateMap<TransactionTable, Transaction>();

            Mapper.CreateMap<Transaction, TransactionTable>();
        }

        public static Transaction Map(TransactionTable entity)
        {
            return Mapper.Map<Transaction>(entity);
        }

        public static List<Transaction> Map(List<TransactionTable> entityLst)
        {
            return Mapper.Map<List<Transaction>>(entityLst);
        }

        public static TransactionTable Map(Transaction entity)
        {
            return Mapper.Map<TransactionTable>(entity);
        }

    }
}
