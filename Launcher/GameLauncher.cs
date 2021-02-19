using Emerald.Core;
using Emerald.Utilities.Wpf2Sdl;
using Free;
using SDLX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// Launcher/GameLauncher.cs
/// 
/// Created: 2020-07-22
/// 
/// Modified: 2020-07-22
/// 
/// Version: 1.00
/// 
/// Purpose: Chooses the version of the game to launch (WPF or SDL) and launches it. Passes the settings to the game
///
/// </summary>

namespace Launcher
{
    // IRenderer not really required
    public enum AvailableRenderers { SDL, WPF };

    public static class GameLauncher
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Launch0 L0 = new Launch0();
            L0.BootNow_S0();
        }
    }

    /// <summary>
    /// BootNow! stage 0
    /// </summary>
    public class Launch0
    {
        public void BootNow_S0()
        {
            AvailableRenderers UsingRenderer = AvailableRenderers.SDL;

            Debug.WriteLine("BootNow! stage0 now launching...");

            SettingLoader.LoadSettings();
            
            BootNow_S0_LaunchS0(UsingRenderer);

        }

        private void BootNow_S0_LaunchS0(AvailableRenderers LaunchRenderer)
        {
            switch (LaunchRenderer)
            {
                case AvailableRenderers.WPF:
                    BootNow_S0_LaunchWPF();
                    return;
                case AvailableRenderers.SDL:
                    BootNow_S0_LaunchSDL();
                    return; 
            }
        }
        
        private void BootNow_S0_LaunchWPF()
        {
            Debug.WriteLine("Now launching SDL Emerald...");
            App FreeWpfApp = new App();
            FreeWpfApp.InitializeComponent();
            FreeWpfApp.Run();
        }

        private void BootNow_S0_LaunchSDL()
        {
            BootNow_S0_LaunchWPF();
        }
    }
}
