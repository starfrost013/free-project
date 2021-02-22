using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    public static class SettingLoader
    {
        /// <summary>
        /// Load settings using the Emerald Settings API.
        /// </summary>
        /// 
        public static bool IsLoaded = false;

        public static void LoadSettings()
        {
            try
            {
                //SDLDebug.LogDebug_C("BootNow!", "Now loading settings...");

                Settings.DebugMode = GameSettings.GetBool("DebugMode");

                Settings.DemoMode = GameSettings.GetBool("DemoMode");

                if (Settings.DemoMode) Settings.DemoModeMaxLevel = GameSettings.GetInt("DemoModeMaxLevel");

                Settings.Resolution = GameSettings.GetPoint("Resolution");

                Settings.TitleScreenPath = GameSettings.GetString("TitleScreenPath");

                Settings.WindowType = (WindowType)Enum.Parse(typeof(WindowType), GameSettings.GetString("WindowType"));

                // Load FeatureControl

                Settings.FeatureControl_DumpConsoleMessagesToW32Console_WindowsOnly = GameSettings.GetBool("FC_Dump2Console");

                Settings.FeatureControl_DisableWPF = GameSettings.GetBool("FC_DisableWPF");


                IsLoaded = true; 
            }
            catch (ArgumentException err)
            {
                //Error.Throw(err, ErrorSeverity.FatalError, "Fatal error loading settings - Conversion to enum failed.", "Init error", 87);

                // TEMP
#if DEBUG
                MessageBox.Show($"EngineCore: Temp error T87: Fatal error loading settings - conversion to enum failed, likely malformed\n\n{err}", "Error T87 Temp", MessageBoxButton.OK);
#else
                MessageBox.Show($"EngineCore: Temp error T87: Fatal error loading settings - conversion to enum failed, likely malformed", "Error T87 Temp", MessageBoxButton.OK);
#endif

            }
        }
    }
}
