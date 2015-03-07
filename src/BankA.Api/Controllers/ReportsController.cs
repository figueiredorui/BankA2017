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
        ReportService svc = new ReportService();

        // GET: api/Reports/Chart
        [Route("Reports/MonthlyDebitCredit")]
        public IHttpActionResult GetMonthlyDebitCredit()
        {
            var lst = svc.GetMonthlyDebitCredit(DateTime.Now.Date.AddMonths(-12), DateTime.Now.Date);
            return Ok(lst);
        }

        // GET: api/Reports/RunningBalance
        [Route("Reports/RunningBalance")]
        public IHttpActionResult GetRunningBalance()
        {
            var lst = svc.GetRunningBalance(null, DateTime.Now.Date.AddMonths(-24), DateTime.Now.Date);
            return Ok(lst);
        }

        [Route("Reports/DebitReport")]
        public IHttpActionResult GetDebitReport()
        {
            var lst = svc.GetDebitReport(null, DateTime.Now.Date.AddMonths(-12), DateTime.Now.Date);
            return Ok(lst);
        }

        [Route("Reports/ExpensesByTag")]
        public IHttpActionResult GetExpensesByTag()
        {
            var lst = svc.GetExpensesByTag(null, DateTime.Now.Date.AddMonths(-12), DateTime.Now.Date);
            return Ok(lst);
        }

        private DateTime LastDayOfMonth(DateTime date)
        {
            return date.AddDays(1 - (date.Day)).AddMonths(1).AddDays(-1);
        }
    }


}