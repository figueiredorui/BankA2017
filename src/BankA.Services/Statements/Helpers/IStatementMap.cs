using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankA.Models.Enums;
using CsvHelper.Configuration;
using BankA.Models.Transactions;

namespace BankA.Services.Statements.Maps
{
    public class IStatementMap : CsvClassMap<StatementRow>
    {
    }
}
