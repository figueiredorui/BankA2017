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
using BankA.Models.Enums;
using BankA.Models.Transactions;
using BankA.Services.Statements;

namespace BankA.Controllers.Controllers
{
    public class StatementsController : ApiController
    {
        private readonly IStatementService svc;

        public StatementsController()
        {
            this.svc = new StatementService();
        }

        public StatementsController(IStatementService svc)
        {
            this.svc = svc;
        }

        // GET: api/Statement
        public IHttpActionResult Get()
        {

            var lst = svc.GetList();
            return Ok(lst);
        }

        //GET: api/Statement/5
        //public IHttpActionResult Get(int id)
        //{
        //    var result = svc.Find(id);
        //    return Ok(result);
        //}

        // PUT: api/Statement/5
        //public IHttpActionResult Put(int id, TransactionRule model)
        //{
        //    svc.Update(model);
        //    return Ok();
        //}

        // POST: api/Statement
        //public IHttpActionResult Post(TransactionRule model)
        //{
        //    svc.Add(model);
        //    return Ok();
        //}

        // DELETE: api/Statement/5
        public IHttpActionResult Delete(int id)
        {
            svc.Delete(id);
            return Ok();
        }



    }
}