using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BankA.Models;
using BankA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net.Http.Headers;
using BankA.Services.Transactions;
using BankA.Services.Reports;
using BankA.Models.Reports;

namespace BankA.Api.Controllers
{
    public class ReportsController : ApiController
    {
        private readonly IReportService svc;

        public ReportsController(IReportService svc)
        {
            this.svc = svc;
        }

        // GET: api/Reports/Chart
        [Route("Reports/MonthlyCashFlow/{accountID}")]
        public IHttpActionResult GetMonthlyCashFlow(int accountID)
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetMonthlyDebitCredit(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        // GET: api/Reports/RunningBalance
        [Route("Reports/RunningBalance/{accountID}")]
        public IHttpActionResult GetRunningBalance(int accountID)
        {
            var dates = DateFilterHelper.Calc(24);
            var lst = svc.GetRunningBalance(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        [Route("Reports/Expenses")]
        public IHttpActionResult GetExpenses()
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetExpenses(null, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        [Route("Reports/ExpensesByTag")]
        public IHttpActionResult GetExpensesByTag()
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetExpensesByTag(null, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        [Route("Reports/Income")]
        public IHttpActionResult GetIncome()
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetIncome(null, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }
    }

    public class DateFilterHelper
    {
        public static BetweenDates Calc(int months)
        {
            return new BetweenDates()
            {
                 StartDate = new DateTime(DateTime.Now.Date.AddMonths(-months).Year, DateTime.Now.Date.AddMonths(-months).Month, 1),
                 EndDate = DateTime.Now.Date
            };
        }

        public class BetweenDates
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

        }

    }

}