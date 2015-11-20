using BankA.Data.Entities;
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

        public List<MonthlyCashFlow> GetMonthlyCashFlow(int? accountID, DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table
                                                        .Where(q => q.IsTransfer == false
                                                            && q.AccountID == (accountID ?? q.AccountID)
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
                       select new MonthlyCashFlow()
                       {
                           Month = grp.Key.Month,
                           Year = grp.Key.Year,
                           CreditAmount = grp.Sum(o => o.CreditAmount),
                           DebitAmount = grp.Sum(o => o.DebitAmount),
                       }).ToList();



            while (startDate < endDate)
            {
                if (!lst.Any(q => q.Month == startDate.Month && q.Year == startDate.Year))
                    lst.Add(new MonthlyCashFlow() { Month = startDate.Month, Year = startDate.Year });

                startDate = startDate.AddMonths(1);
            };

            return lst = lst.OrderBy(o => o.Year).ThenBy(o => o.Month).ToList();
        }

        public List<RunningBalance> GetRunningBalance(int? accountID, DateTime startDate, DateTime endDate)
        {

            var transactionsLst = (from trans in transactionRepository.Table.Where(q => q.AccountID == (accountID ?? q.AccountID))
                                   group trans by new
                                    {
                                        TransactionDate = trans.TransactionDate,
                                    } into grp
                                   select new
                                   {
                                       TransactionDate = grp.Key.TransactionDate,
                                       CreditAmount = grp.Sum(o => o.CreditAmount),
                                       DebitAmount = grp.Sum(o => o.DebitAmount),
                                   }).ToList();


            decimal balance = 0;
            var statement = transactionsLst.Select(transaction =>
            {
                balance += transaction.CreditAmount - transaction.DebitAmount;

                return new RunningBalance()
                {
                    TransactionDate = transaction.TransactionDate,
                    RunningAmount = balance
                };
            }).Where(q => q.TransactionDate >= startDate && q.TransactionDate <= endDate).ToList();
            return statement.OrderBy(o => o.TransactionDate).ToList();

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
