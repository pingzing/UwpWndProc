using Windows.ApplicationModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace WndProcTest
{
    sealed partial class App : Application
    {
        private IntPtr _oldWndProc;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {                
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;

                // We could probably do this a little earlier, but we need to wait
                // for the CoreWindow to be ready so can get its HWND, and this is
                // Good Enough(tm).
                _oldWndProc = Native.WndProc.SetWndProc(WindowProcess);
            }

            if (e.UWPLaunchActivatedEventArgs.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        private IntPtr WindowProcess(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            // Standard WndProc handling code here

            // Call the "base" WndProc
            return Native.Interop.CallWindowProc(_oldWndProc, hwnd, message, wParam, lParam);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
