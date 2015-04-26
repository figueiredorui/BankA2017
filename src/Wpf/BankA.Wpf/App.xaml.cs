using BankA.Services.Accounts;
using BankA.Wpf.Config;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Windows;

namespace BankA.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string baseAddress = "http://localhost:9000/";
            // Start OWIN host 
            WebApp.Start<HostConfig>(url: baseAddress);

            //var svc = new AccountService().GetAccountSummary();


        }
    }

}
