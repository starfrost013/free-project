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
    enum AvailableRenderers { SDL, WPF };

    public static class GameLauncher
    {
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
            AvailableRenderers UsingRenderer = AvailableRenderers.WPF;

            Debug.WriteLine("BootNow! stage0 now launching...");

            SettingLoader.LoadSettings();
            
            if (Settings.FeatureControl_DisableSDL_PublicDemosOnly && Settings.FeatureControl_DisableWPF)
            {
                UsingRenderer = AvailableRenderers.SDL;
            }
            
            if (Settings.FeatureControl_DisableWPF)
            {
                UsingRenderer = AvailableRenderers.SDL;
            }

            BootNow_S0_LaunchS0(UsingRenderer);

        }

        private void BootNow_S0_LaunchS0(AvailableRenderers LaunchRenderer)
        {
            switch (LaunchRenderer)
            {
                case AvailableRenderers.WPF:
                    Debug.WriteLine("Now launching Windows Presentation Foundation (WPF) free! game [M6 only, deprecated]");
                    App FreeWpfApp = new App();
                    FreeWpfApp.InitializeComponent();
                    FreeWpfApp.Run(); 

                    return;
                case AvailableRenderers.SDL:
                    Debug.WriteLine("Now launching SDL game [M6, M7+ only renderer]");
                    return; 
            }
        }
    }
}
