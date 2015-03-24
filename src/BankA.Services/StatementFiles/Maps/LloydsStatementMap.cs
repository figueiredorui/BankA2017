using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.StatementFiles.Maps;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.StatementFiles
{
    [BankNameAttribute(BankEnum.LLOYDS)]
    public class LloydsStatementMap : IStatementMap
    {
        public LloydsStatementMap()
        {
            Map(m => m.TransactionDate).Index(0);
            Map(m => m.Type).Index(1);
            Map(m => m.Description).Index(4);
            Map(m => m.DebitAmount).Index(5).Default(0);
            Map(m => m.CreditAmount).Index(6).Default(0);
        }
    }
}
