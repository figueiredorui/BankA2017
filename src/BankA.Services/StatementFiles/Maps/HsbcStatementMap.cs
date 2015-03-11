using BankA.Models.Transactions;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.StatementFiles
{
    //[BankNameAttribute()]
    public sealed class HsbcStatementMap : CsvClassMap<StatementRow>
    {
        public HsbcStatementMap()
        {
            Map(m => m.TransactionDate).Index(0);
            //Map(m => m.Type);
            Map(m => m.Description).Index(1);
            Map(m => m.DebitAmount).ConvertUsing(row => row.GetField<decimal>(2) < 0 ? row.GetField<decimal>(2) : 0);
            Map(m => m.CreditAmount).ConvertUsing(row => row.GetField<decimal>(2) > 0 ? row.GetField<decimal>(2) : 0);
        }
    }

   
}
