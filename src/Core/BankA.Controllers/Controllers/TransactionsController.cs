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
using BankA.Models.Transactions;
using BankA.Services.Statements;

namespace BankA.Controllers.Controllers
{
    [RoutePrefix("api")]
    public class TransactionsController : ApiController
    {
        private readonly ITransactionService transactionSvc;
        private readonly IStatementService statementSvc;

        public TransactionsController()
        {
            this.transactionSvc = new TransactionService();
            this.statementSvc = new StatementService();
        }

        public TransactionsController(ITransactionService transactionSvc, IStatementService statementSvc)
        {
            this.transactionSvc = transactionSvc;
            this.statementSvc = statementSvc;
        }

        // GET: api/Transactions/Search
        [Route("Transactions/Search")]
        public IHttpActionResult GetTrans([FromUri] TransactionSearch search)
        {
            //var startDate = new DateTime();// JsonConvert.DeserializeObject<DateTime>(search.StartDate);
            //var endDate = JsonConvert.DeserializeObject<DateTime>(search.EndDate);
            if (search == null)
                return BadRequest();

            var lst = transactionSvc.GetTransactions(search.AccountID, search.StartDate, search.EndDate, search.Tag);
            return Ok(lst);
        }

        // GET: api/Transactions/5
        public IHttpActionResult Get(int id)
        {
            var result = transactionSvc.Find(id);
            return Ok(result);
        }

        // PUT: api/Transactions/5
        public IHttpActionResult Put(int id, Transaction transaction)
        {
            transactionSvc.Update(transaction);
            return Ok();
        }

        // POST: api/Transactions
        public IHttpActionResult Post(Transaction transaction)
        {
            transactionSvc.Add(transaction);
            return Ok();
        }

        // DELETE: api/Transactions/5
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }

        // GET: api/Transactions/Tags
        [Route("Transactions/Tags")]
        public IHttpActionResult GetTags()
        {
            var lst = transactionSvc.GetTags();
            return Ok(lst);
        }

        [Route("Transactions/Upload")]
        public async Task<IHttpActionResult> Upload()
        {
            try
            {
                HttpRequestMessage request = this.Request;
                if (!request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = string.Empty;
                if (System.Web.HttpContext.Current != null)
                    root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
                else
                    root = System.AppDomain.CurrentDomain.BaseDirectory;
                
                
                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.FileData[0];
                var accountID = Convert.ToInt32(provider.FormData["AccountID"]);
                var fileContent = System.IO.File.ReadAllBytes(file.LocalFileName);


                new StatementService().ImportFile(new StatementFile()
                            {
                                FileName = file.Headers.ContentDisposition.FileName.Trim('"'),
                                FileContent = fileContent,
                                ContentType = file.Headers.ContentType.MediaType,
                                AccountID = accountID
                            });

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}