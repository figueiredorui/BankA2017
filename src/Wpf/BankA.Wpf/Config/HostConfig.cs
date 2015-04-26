using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BankA.Wpf.Config
{
    class HostConfig
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            Type t2 = typeof(BankA.Api.Controllers.AccountsController);
            Type t3 = typeof(BankA.Api.Controllers.ReportsController);
            Type t4 = typeof(BankA.Api.Controllers.TransactionsController);



            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional }
            );

            appBuilder.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(GetRootDirectory()),
                EnableDirectoryBrowsing = true,
                RequestPath = new Microsoft.Owin.PathString("/html")
            });

            appBuilder.UseWebApi(config);
        }

        private static string GetRootDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var rootDirectory = Directory.GetParent(currentDirectory).Parent;
            Contract.Assume(rootDirectory != null);
            return Path.Combine(currentDirectory, "html");
        }
    }
}
