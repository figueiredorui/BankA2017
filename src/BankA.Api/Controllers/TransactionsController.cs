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

namespace BankA.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        TransactionService svc = new TransactionService();

        // GET: api/Transactions
        public IHttpActionResult Get()
        {

            var lst = svc.GetTransactions(DateTime.MinValue, DateTime.MaxValue);
            return Ok(lst);
        }

        // GET: api/Transactions/5
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        // PUT: api/Transactions/5
        public IHttpActionResult Put(int id, Transaction transaction)
        {
            return Ok();
        }

        // POST: api/Transactions
        public IHttpActionResult Post(Transaction transaction)
        {
            return Ok();
        }

        // DELETE: api/Transactions/5
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }


    }
}