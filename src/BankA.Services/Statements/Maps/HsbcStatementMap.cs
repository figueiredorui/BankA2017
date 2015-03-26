using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Statements.Maps;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Statements
{
    [BankNameAttribute(BankEnum.HSBC)]
    public class HsbcStatementMap : IStatementMap
    {
        public HsbcStatementMap()
        {
            Map(m => m.TransactionDate).Index(0);
            //Map(m => m.Type);
            Map(m => m.Description).Index(1);
            Map(m => m.DebitAmount).ConvertUsing(row => row.GetField<decimal>(2) < 0 ? Math.Abs(row.GetField<decimal>(2)) : 0);
            Map(m => m.CreditAmount).ConvertUsing(row => row.GetField<decimal>(2) > 0 ? Math.Abs(row.GetField<decimal>(2)) : 0);
        }
    }
}
