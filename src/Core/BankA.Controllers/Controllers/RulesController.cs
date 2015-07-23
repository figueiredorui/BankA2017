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
using BankA.Services.Rules;
using BankA.Models.Transactions;

namespace BankA.Controllers.Controllers
{
    public class RulesController : ApiController
    {
        private readonly IRulesService svc;

        public RulesController()
        {
            this.svc = new RulesService();
        }

        public RulesController(IRulesService svc)
        {
            this.svc = svc;
        }

        // GET: api/Rules
        public IHttpActionResult Get()
        {

            var lst = svc.GetList();
            return Ok(lst);
        }

        // GET: api/Rules/5
        public IHttpActionResult Get(int id)
        {
            var result = svc.Find(id);
            return Ok(result);
        }

        // PUT: api/Rules/5
        public IHttpActionResult Put(int id, TransactionRule model)
        {
            svc.Update(model);
            return Ok();
        }

        // POST: api/Rules
        public IHttpActionResult Post(TransactionRule model)
        {
            svc.Add(model);
            return Ok();
        }

        // DELETE: api/Rules/5
        public IHttpActionResult Delete(int id)
        {
            svc.Delete(id);
            return Ok();
        }



    }
}