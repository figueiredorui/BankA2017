using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace BankA.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //container.RegisterType<IAccountService, AccountService>();
            //container.RegisterType<IReportService, ReportService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}