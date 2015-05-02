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

        //private void OnWebViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    switch (e.PropertyName)
        //    {
        //        case "IsBrowserInitialized":
        //            if (WebView.IsBrowserInitialized)
        //            {
        //                WebView.Load("http://10.211.55.2:42000");
        //            }

        //            break;
        //    }
        //}

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
            
            var location = @"D:\GitHub\BankA\src\Wpf\Releases";
            var appName = "BankA";
            using (var mgr = new UpdateManager(location, appName, FrameworkVersion.Net45))
            {
                try
                {
                    
                    UpdateInfo updateInfo = await mgr.CheckForUpdate();
                    Trace.WriteLine("CheckForUpdate");
                    if (updateInfo.FutureReleaseEntry != null)
                    {
                        Trace.WriteLine("check Version");
                        if (updateInfo.CurrentlyInstalledVersion.Version == updateInfo.FutureReleaseEntry.Version)
                        {
                            Trace.WriteLine("Same Version");
                            return;
                        }
                        await mgr.UpdateApp();
                        Trace.WriteLine("UpdateApp");
                        // This will show a button that will let the user restart the app
                        Dispatcher.Invoke(ShowUpdateIsAvailable);
                        Trace.WriteLine("ShowUpdateIsAvailable");
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

        // This listens for Windows messages so we can pop up this window if the
        // user tries to launch a second instance of the application. You can 
        // find more information in NativeMethods.cs and StartupManager.cs.
        //protected override void OnSourceInitialized(EventArgs eventArgs)
        //{
        //    base.OnSourceInitialized(eventArgs);
        //    var source = PresentationSource.FromVisual(this) as HwndSource;
        //    if (source != null) source.AddHook(WndProc);
        //}

        //private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    if (msg == NativeMethods.WM_SHOWME)
        //    {
        //        Show();
        //        WindowState = WindowState.Normal;
        //    }
        //    return IntPtr.Zero;
        //}

        private async void RestartButtonClicked(object sender, MouseButtonEventArgs e)
        {
            await ShutdownApp();
        }
    }


    
    class NativeMethods
    {
        // The following code is used to setup Win32 messaging so we can handle
        // messages from subsequent instances of this app that try to start. They
        // won't be able to start because of the mutex registered in StartupManager.cs
        // but they will send a message before they exit. The current instance will
        // respond to that message and show the Main window as a result.

        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
    }
}

