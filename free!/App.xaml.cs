using Emerald.Core;
using SDLX;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Game SDLGame { get; set; }
        private void Application_Activated(object sender, EventArgs e)
        {

            // temporary

            NativeMethods.AllocConsole();

#if DEBUG
            IntPtr _ = NativeMethods.GetConsoleWindow();

            if (_ == IntPtr.Zero)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"Win32 error occurred while initialising debug console {Marshal.GetLastWin32Error()}");
            }
            else
            {
                NativeMethods.ShowWindow(_, (int)NativeMethods.Win32__ShowWindow_Mode.SW_SHOWNORMAL);
            }

            Console.OpenStandardOutput();
#endif

            SDLGame = new Game();

            if (!Settings.FeatureControl_DisableWPF)
            {
                FreeSDL MnWindow = new FreeSDL();
                MnWindow.Show();
            }
        }
    }
}
