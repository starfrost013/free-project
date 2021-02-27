using Emerald.Core;
using Emerald.Utilities.Wpf2Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
#if DEBUG
                Error.ThrowV2($"EngineCore:Fatal error loading settings - conversion to enum failed, likely malformed!\n\n{err}", "Error 87", ErrorSeverity.Error, 87);
#else
                Error.ThrowV2($"EngineCore:Fatal error loading settings - conversion to enum failed, likely malformed!", "Error 87", ErrorSeverity.Error, 87);
#endif



            }
        }
    }
}
