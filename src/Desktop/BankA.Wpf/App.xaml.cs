using BankA.Wpf.Config;
using MahApps.Metro;
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
using System.Reflection;
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
            var localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(localPath, "BankA.db");

            Directory.CreateDirectory(dbPath);
            AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);

            string baseAddress = "http://localhost:9000/";
            // Start OWIN host 
            WebApp.Start<HostConfig>(url: baseAddress);

            //var svc = new AccountService().GetAccountSummary();


        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // add custom accent and theme resource dictionaries
            ThemeManager.AddAccent("BankAStyle", new Uri("pack://application:,,,/BankA.Wpf;component/Resources/BankAStyle.xaml"));
            ThemeManager.AddAppTheme("BankATheme", new Uri("pack://application:,,,/BankA.Wpf;component/Resources/BankATheme.xaml"));

            // get the theme from the current application
            var theme = ThemeManager.DetectAppStyle(Application.Current);

            // now use the custom accent
            ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent("BankAStyle"), ThemeManager.GetAppTheme("BankATheme"));
//                                    theme.Item1);

            base.OnStartup(e);

              base.OnStartup(e);
        }
    }

}
