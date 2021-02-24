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
        /// <summary>
        /// soon this will allow the user to select a game so we aren't getting rid of this weird looking code
        /// </summary>
        public void BootNow_S0()
        {

            Debug.WriteLine("BootNow! prestage0 now launching...");
            GlobalSettings.Load();
            
            
            BootNow_S0_LaunchS0();

        }

        private void BootNow_S0_LaunchS0()
        {
            BootNow_S0_LaunchSDL();
        }


        private void BootNow_S0_LaunchSDL()
        {
            Debug.WriteLine("Now launching SDL Emerald...");
            // manually call the entry point
            
            SDL_Stage0_Init.Main(new string[] { } ); // fake arguments
            //FreeSDL FSDL = new FreeSDL();
        }
    }
}
