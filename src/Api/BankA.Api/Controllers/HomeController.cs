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
                Help = ToPublicUrl(this.Request.RequestUri) + "swagger/ui/index",
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

        public string ToPublicUrl(Uri relativeUri)
        {
            var httpContext = HttpContext.Current;

            var uriBuilder = new UriBuilder
            {
                Host = httpContext.Request.Url.Host,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            if (httpContext.Request.IsLocal)
            {
                uriBuilder.Port = httpContext.Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }

}
