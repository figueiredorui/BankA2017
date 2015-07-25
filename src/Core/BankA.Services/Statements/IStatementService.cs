using BankA.Models.Transactions;
using System;
using System.Collections.Generic;

namespace BankA.Services.Statements
{
    public interface IStatementService
    {
        void Delete(int id);
        List<StatementFile> GetList();
        void ImportFile(StatementImport statement);
    }
}
