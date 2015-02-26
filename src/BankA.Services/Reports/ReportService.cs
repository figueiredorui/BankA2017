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

        public List<MonthlyBalance> GetMonthlyBalance(DateTime startDate, DateTime endDate)
        {
            var transactionsLst = transactionRepository.Table.Where(q => q.TransactionDate >= startDate && q.TransactionDate <= endDate).ToList();

            var curDate = DateTime.Now.Date;
            var firstOfNextMonth = new DateTime(curDate.Year, curDate.Month, 1).AddMonths(1);

            var startdate = firstOfNextMonth.AddYears(-1);
            for (int i = 0; i < 12; i++)
            {
                var date = startdate.AddMonths(i);

                if (!transactionsLst.Any(q => q.TransactionDate.Month == date.Month && q.TransactionDate.Year == date.Year))
                    transactionsLst.Add(new BankTransactionTable() { TransactionDate = date, CreditAmount = 0, DebitAmount = 0 });

            }

            var lst = (from item in transactionsLst
                       group item by new
                       {
                           Month = item.TransactionDate.Month,
                           Year = item.TransactionDate.Year
                       } into grp
                       orderby grp.Key.Year, grp.Key.Month
                       select new MonthlyBalance()
                       {
                           // YearMonth = DateTimeHelper.GetMonthName(grp.Key.Month) + "/" + grp.Key.Year.ToString().Substring(2, 2),
                           Month = (grp.Key.Month + "/" + grp.Key.Year).ToString(),
                           CreditAmount = grp.Sum(o => o.CreditAmount),
                           DebitAmount = grp.Sum(o => o.DebitAmount),
                       }).ToList();

            return lst;
        }
    }
}
