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
    public class AccountsController : ApiController
    {
        AccountService svc = new AccountService();

        // GET: api/Accounts
        public IHttpActionResult Get()
        {

            var lst = svc.GetList();
            return Ok(lst);
        }

        // GET: api/Accounts/5
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        // PUT: api/Accounts/5
        public IHttpActionResult Put(int id, Account account)
        {
            return Ok();
        }

        // POST: api/Accounts
        public IHttpActionResult Post(Account account)
        {
            return Ok();
        }

        // DELETE: api/Accounts/5
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }


    }
}