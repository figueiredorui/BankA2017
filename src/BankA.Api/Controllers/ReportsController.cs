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

namespace BankA.Api.Controllers
{
    public class ReportsController : ApiController
    {
        ReportService svc = new ReportService();



        // GET: api/Reports/Chart
        [Route("Reports/MonthlyBalance")]
        public IHttpActionResult GetChart()
        {
            var lst = svc.GetMonthlyBalance(DateTime.Now.Date.AddMonths(-12), DateTime.Now.Date);
            return Ok(lst);
        }
    }


}