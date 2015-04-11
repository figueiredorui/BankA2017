using BankA.Models.Transactions;
using System;
namespace BankA.Services.Statements
{
    public interface IStatementService
    {
        void ImportFile(StatementFile statement);
    }
}
