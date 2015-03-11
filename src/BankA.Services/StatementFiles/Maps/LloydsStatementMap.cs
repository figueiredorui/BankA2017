using BankA.Models.Transactions;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.StatementFiles
{
    public sealed class LloydsStatementMap : CsvClassMap<StatementRow>
    {
        public LloydsStatementMap()
        {
            Map(m => m.TransactionDate).Index(0);
            Map(m => m.Type).Index(1);
            Map(m => m.Description).Index(4);
            Map(m => m.DebitAmount).Index(5);
            Map(m => m.CreditAmount).Index(6);
        }
    }
}
