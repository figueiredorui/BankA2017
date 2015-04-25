using BankA.Services.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace BankA.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            CreateIfNotExists();
            var dbVersion = GetDbVersion();

            return Ok(new
            {
                Name = "BankA API",
                Version = GetVersion(),
                GitHub = "https://github.com/figueiredorui/BankA.Api",
                Help= this.Request.RequestUri + "swagger/ui/index",
                DbVersion = dbVersion
            });
        }

        private void CreateIfNotExists()
        {
            //var svc = new AdminService();
            //svc.CreateIfNotExists();
        }

        private string GetDbVersion()
        {
            var svc = new AdminService();
            return svc.GetDbVersion();
        }

        private string GetVersion()
        {
            Assembly web = Assembly.GetExecutingAssembly();
            AssemblyName webName = web.GetName();

            string myVersion = webName.Version.ToString();

            return myVersion;
        }
    }

}
