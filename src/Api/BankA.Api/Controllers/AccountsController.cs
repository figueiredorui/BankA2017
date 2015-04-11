﻿using System;
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
using BankA.Services.Accounts;
using BankA.Models.Accounts;

namespace BankA.Api.Controllers
{
    [RoutePrefix("api")]
    public class AccountsController : ApiController
    {
        private readonly IAccountService svc;

        public AccountsController()
        {
            this.svc = new AccountService();
        }

        public AccountsController(IAccountService svc)
        {
            this.svc = svc;
        }

        // GET: api/Accounts
        public IHttpActionResult Get()
        {

            var lst = svc.GetList();
            return Ok(lst);
        }

        // GET: api/Accounts/5
        public IHttpActionResult Get(int id)
        {
            var result = svc.Find(id);
            return Ok(result);
        }

        // PUT: api/Accounts/5
        public IHttpActionResult Put(int id, Account account)
        {
            svc.Update(account);
            return Ok();
        }

        // POST: api/Accounts
        public IHttpActionResult Post(Account account)
        {
            svc.Add(account);
            return Ok();
        }

        // DELETE: api/Accounts/5
        public IHttpActionResult Delete(int id)
        {
            svc.Delete(id);
            return Ok();
        }

        // GET: api/Accounts/Summary
        [Route("Accounts/Summary")]
        public IHttpActionResult GetAccountSummary()
        {
            var lst = svc.GetAccountSummary();
            return Ok(lst);
        }

        // GET: api/Accounts/Banks
        [Route("Accounts/Banks")]
        public IHttpActionResult GetBanks()
        {
            var lst = new string[] { BankEnum.HSBC.ToString(), BankEnum.LLOYDS.ToString() };
            return Ok(lst);
        }

    }
}