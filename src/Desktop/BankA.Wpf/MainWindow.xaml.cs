using FluentScheduler;
using MahApps.Metro.Controls;
using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankA.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "BankA v." + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            btnNewVersion.Visibility = Visibility.Hidden;
            ScheduleAppUpdates();
            wb.Visibility = System.Windows.Visibility.Hidden;
            wb.FrameLoadEnd += wb_FrameLoadEnd;
        }

        void wb_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                wb.Visibility = System.Windows.Visibility.Visible;
                prgRing.Visibility = System.Windows.Visibility.Hidden;
            });
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
                wb.ShowDevTools();
        }

        private void ScheduleAppUpdates()
        {
            //TaskManager.AddTask(() => ScheduleApplicationUpdates(), x => x.ToRunNow().AndEvery(60).Seconds());
            ScheduleApplicationUpdates();
        }

        private async void ScheduleApplicationUpdates()
        {

            var location = @"https://raw.githubusercontent.com/figueiredorui/BankA/master/src/Desktop/Releases";
            var appName = "BankA";
            using (var mgr = new UpdateManager(location, appName, FrameworkVersion.Net45))
            {
                try
                {

                    UpdateInfo updateInfo = await mgr.CheckForUpdate();
                    TraceWrite("Check For Update - " + location);
                    if (updateInfo.FutureReleaseEntry != null)
                    {
                        TraceWrite("checking Version");
                        if (updateInfo.CurrentlyInstalledVersion.Version == updateInfo.FutureReleaseEntry.Version)
                        {
                            TraceWrite("Same Version - " + updateInfo.CurrentlyInstalledVersion.Version.ToString());
                            return;
                        }
                        TraceWrite("Start UpdateApp");
                        await mgr.UpdateApp();
                        TraceWrite("End UpdateApp");
                        // This will show a button that will let the user restart the app
                        Dispatcher.Invoke(ShowUpdateIsAvailable);
                        TraceWrite("ShowUpdateIsAvailable");
                        // This will restart the app automatically
                        //Dispatcher.InvokeAsync<Task>(ShutdownApp);
                    }

                }
                catch (Exception ex)
                {
                    var a = ex;
                }
            }
        }

        private async Task ShutdownApp()
        {
            //UpdateManager.RestartApp();
            await Task.Delay(1000);
            Application.Current.Shutdown(0);
        }

        private void ShowUpdateIsAvailable()
        {
            btnNewVersion.Visibility = Visibility.Visible;
        }

        private async void RestartButtonClicked(object sender, MouseButtonEventArgs e)
        {
            await ShutdownApp();
        }

        private void TraceWrite(string message)
        {
            Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, message));
        }
    }

}

