using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Files.Maps;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Files
{
    [BankNameAttribute(BankEnum.NATWEST)]
    public class NatwestStatementMap : IStatementMap
    {
        public NatwestStatementMap()
        {
            Map(m => m.TransactionDate).Index(0);
            Map(m => m.Type).Index(1);
            Map(m => m.Description).Index(2);
            Map(m => m.DebitAmount).ConvertUsing(row => row.GetField<decimal>(3) < 0 ? Math.Abs(row.GetField<decimal>(3)) : 0);
            Map(m => m.CreditAmount).ConvertUsing(row => row.GetField<decimal>(3) > 0 ? Math.Abs(row.GetField<decimal>(3)) : 0);

        }
    }
}
