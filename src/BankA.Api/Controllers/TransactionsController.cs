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
using BankA.Data.Models;
using BankA.Services.Transactions;
using BankA.Api.Models;

namespace BankA.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        TransactionService svc = new TransactionService();


        // GET: api/Transactions/Search
        [Route("Transactions/Search")]
        public IHttpActionResult GetTrans([FromUri] TransactionSearch search)
        {
            //var startDate = new DateTime();// JsonConvert.DeserializeObject<DateTime>(search.StartDate);
            //var endDate = JsonConvert.DeserializeObject<DateTime>(search.EndDate);
            if (search == null)
                return BadRequest();

            var lst = svc.GetTransactions(search.AccountID, search.StartDate, search.EndDate);
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
            svc.Add(transaction);
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
            var lst = svc.GetTags();
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
                string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.FileData[0];
                var accountID = provider.FormData["AccountID"];
                var fileContent = System.IO.File.ReadAllBytes(file.LocalFileName);

                svc.Upload(new StatementFile()
                            {
                                FileName = file.Headers.ContentDisposition.FileName.Trim('"'),
                                FileContent = fileContent,
                                ContentType = file.Headers.ContentType.MediaType
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