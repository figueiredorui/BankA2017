using BankA.Services.Accounts;
using BankA.Services.Reports;
using BankA.Services.Statements;
using BankA.Services.Transactions;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace BankA.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<ITransactionService, TransactionService>();
            container.RegisterType<IStatementService, StatementService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}