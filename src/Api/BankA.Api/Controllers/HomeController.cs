using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace BankA.Api.Controllers
{
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Name = "BankA API",
                Version = GetVersion(),
                GitHub = "https://github.com/figueiredorui/BankA.Api"
            });
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
