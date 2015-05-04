using BankA.Services.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace BankA.Api.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            var apiVersion = GetApiVersion();
            var dbVersion = GetDbVersion();

            return Ok(new
            {
                Name = "BankA API",
                Version = apiVersion,
                GitHub = "https://github.com/figueiredorui/BankA",
                Help= this.Request.RequestUri.AbsoluteUri + "swagger/ui/index",
                DbVersion = dbVersion
            });
        }

        private string GetDbVersion()
        {
            var svc = new AdminService();
            return svc.GetDbVersion();
        }

        private string GetApiVersion()
        {
            Assembly web = Assembly.GetExecutingAssembly();
            AssemblyName webName = web.GetName();

            string myVersion = webName.Version.ToString();

            return myVersion;
        }
    }

}
