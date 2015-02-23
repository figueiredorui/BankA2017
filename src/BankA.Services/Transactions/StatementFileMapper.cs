using AutoMapper;
using BankA.Data.Models;
using BankA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Transactions
{
    public class StatementFileMapper
    {
        static StatementFileMapper()
        {
            Mapper.CreateMap<StatementFileTable, StatementFile>();
            Mapper.CreateMap<StatementFile, StatementFileTable>();
        }

        public static StatementFile Map(StatementFileTable entity)
        {
            return Mapper.Map<StatementFile>(entity);
        }

        public static StatementFileTable Map(StatementFile entity)
        {
            return Mapper.Map<StatementFileTable>(entity);
        }
    }
}
