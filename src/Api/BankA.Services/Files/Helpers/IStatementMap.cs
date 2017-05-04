using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankA.Models.Enums;
using CsvHelper.Configuration;
using BankA.Models.Files;

namespace BankA.Services.Files.Maps
{
    public class IStatementMap : CsvClassMap<StatementRow>
    {
    }
}
