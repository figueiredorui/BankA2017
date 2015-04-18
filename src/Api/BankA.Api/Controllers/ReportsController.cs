using System;
using System.Collections.Generic;
using System.Data;
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
    [RoutePrefix("api")]
    public class ReportsController : ApiController
    {
        private readonly IReportService svc;

        public ReportsController()
        {
            this.svc = new ReportService();
        }


        public ReportsController(IReportService svc)
        {
            this.svc = svc;
        }

        // GET: api/Reports/Chart
        [Route("Reports/MonthlyCashFlow/{accountID:int?}")]
        public IHttpActionResult GetMonthlyCashFlow(int? accountID = null)
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetMonthlyCashFlow(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        // GET: api/Reports/RunningBalance
        [Route("Reports/RunningBalance/{accountID:int?}")]
        public IHttpActionResult GetRunningBalance(int? accountID = null)
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetRunningBalance(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        [Route("Reports/Expenses/{accountID:int?}")]
        public IHttpActionResult GetExpenses(int? accountID = null)
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetExpenses(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        [Route("Reports/ExpensesByTag/{accountID:int?}")]
        public IHttpActionResult GetExpensesByTag(int? accountID = null)
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetExpensesByTag(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }

        [Route("Reports/Income/{accountID:int?}")]
        public IHttpActionResult GetIncome(int? accountID = null)
        {
            var dates = DateFilterHelper.Calc(12);
            var lst = svc.GetIncome(accountID, dates.StartDate, dates.EndDate);
            return Ok(lst);
        }
    }

    public class DateFilterHelper
    {
        public static BetweenDates Calc(int months)
        {
            int currentDay = DateTime.Now.Date.Day;
            return new BetweenDates()
            {
                EndDate = DateTime.Now.Date,
                StartDate = new DateTime(DateTime.Now.Date.AddMonths(-months).Year, DateTime.Now.Date.AddMonths(-months).Month, currentDay),
                
            };
        }

        public class BetweenDates
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

        }

    }

}