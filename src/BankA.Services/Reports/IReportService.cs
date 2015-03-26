using BankA.Models.Reports;
using System;
using System.Collections.Generic;
namespace BankA.Services.Reports
{
    public interface IReportService
    {
        List<ExpensesReport> GetExpenses(int? accountID, DateTime startDate, DateTime endDate);
        List<ExpensesByTag> GetExpensesByTag(int? accountID, DateTime startDate, DateTime endDate);
        List<IncomeReport> GetIncome(int? accountID, DateTime startDate, DateTime endDate);
        List<MonthlyDebitCredit> GetMonthlyDebitCredit(DateTime startDate, DateTime endDate);
        List<RunningBalance> GetRunningBalance(int? accountID, DateTime startDate, DateTime endDate);
    }
}
