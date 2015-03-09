using BankA.Data.Models;
using BankA.Data.Repositories;
using BankA.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Reports
{
    public class ReportService
    {
        TransactionRepository transactionRepository = null;

        public ReportService()
        {
            transactionRepository = new TransactionRepository();
        }

        public List<MonthlyDebitCredit> GetMonthlyDebitCredit(DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table
                                                        .Where(q => q.IsTransfer == false 
                                                        && q.TransactionDate >= startDate 
                                                        && q.TransactionDate <= endDate)
                                                        .ToList();

            var lst = (from item in transactionsLst
                       group item by new
                       {
                           Month = item.TransactionDate.Month,
                           Year = item.TransactionDate.Year
                       } into grp
                       orderby grp.Key.Year, grp.Key.Month
                       select new MonthlyDebitCredit()
                       {
                           Month = (grp.Key.Month + "/" + grp.Key.Year).ToString(),
                           CreditAmount = grp.Sum(o => o.CreditAmount),
                           DebitAmount = grp.Sum(o => o.DebitAmount),
                       }).ToList();

            return lst;
        
        
        }

        public List<RunningBalance> GetRunningBalance(int? accountID, DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table
                                                       .Where(q=>q.AccountID == (accountID ?? q.AccountID))
                                                       .OrderBy(o=>o.TransactionDate)
                                                       .ToList();

            decimal balance = 0;
            var statement = transactionsLst.Select(transaction =>
                                            {
                                                balance += transaction.CreditAmount - transaction.DebitAmount;

                                                return new RunningBalance()
                                                {
                                                    TransactionDate = transaction.TransactionDate,
                                                    CreditAmount = transaction.CreditAmount,
                                                    DebitAmount = transaction.DebitAmount,
                                                    RunningAmount = balance
                                                };
                                            }).Where(q => q.TransactionDate >= startDate && q.TransactionDate <= endDate).ToList();

            var result = new List<RunningBalance>();
            var date = new DateTime(endDate.Year, endDate.Month, 2);

            while (date > startDate)
            {
                result.Add(new RunningBalance()
                    {
                        Month = date.ToShortDateString(),//(date.Month + "/" + date.Year).ToString(),
                        TransactionDate = date,
                        RunningAmount = statement.Where(q => q.TransactionDate <= date).Select(o=>o.RunningAmount).LastOrDefault()
                    });

                date = date.AddMonths(-1);
            }

            return result.OrderBy(o=>o.TransactionDate).ToList();
        }

        public List<ExpensesReport> GetExpenses(int? accountID, DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table
                                                        .Where(q => q.AccountID == (accountID ?? q.AccountID)
                                                            && q.IsTransfer == false
                                                            && q.TransactionDate >= startDate
                                                            && q.TransactionDate <= endDate 
                                                            && q.DebitAmount > 0);

            var lst = (from trans in transactionsLst
                       group trans by new
                       {
                           Tag = trans.Tag,
                           Month = trans.TransactionDate.Month,
                           Year = trans.TransactionDate.Year
                       } into grp
                       select new ExpensesReport()
                       {
                           Tag = grp.Key.Tag,
                           Year = grp.Key.Year,
                           Month = grp.Key.Month,
                           Amount = grp.Sum(o => o.DebitAmount),
                       }).ToList();

            return lst;
        }

        public List<ExpensesByTag> GetExpensesByTag(int? accountID, DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table
                                                        .Where(q => q.AccountID == (accountID ?? q.AccountID)
                                                            && q.IsTransfer == false
                                                            && q.TransactionDate >= startDate
                                                            && q.TransactionDate <= endDate
                                                            && q.DebitAmount > 0);

            var lst = (from trans in transactionsLst
                       group trans by new
                       {
                           Tag = trans.Tag,
                       } into grp
                       select new ExpensesByTag()
                       {
                           Tag = grp.Key.Tag,
                           Amount = grp.Sum(o => o.DebitAmount),
                       }).OrderByDescending(o => o.Amount).Take(10).ToList();

            return lst;
        }

        public List<IncomeReport> GetIncome(int? accountID, DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table
                                                        .Where(q => q.AccountID == (accountID ?? q.AccountID)
                                                            && q.IsTransfer == false
                                                            && q.TransactionDate >= startDate
                                                            && q.TransactionDate <= endDate
                                                            && q.CreditAmount > 0);

            var lst = (from trans in transactionsLst
                       group trans by new
                       {
                           Tag = trans.Tag,
                           Month = trans.TransactionDate.Month,
                           Year = trans.TransactionDate.Year
                       } into grp
                       select new IncomeReport()
                       {
                           Tag = grp.Key.Tag,
                           Year = grp.Key.Year,
                           Month = grp.Key.Month,
                           Amount = grp.Sum(o => o.CreditAmount),
                       }).ToList();

            return lst;
        }
    }
}
