using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    public partial class FreeSDL : Window
    {
        /// <summary>
        /// Load settings using the Emerald Settings API.
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                LogDebug_C("BootNow!", "Now loading settings...");

                Settings.DebugMode = GameSettings.GetBool("DebugMode");

                Settings.DemoMode = GameSettings.GetBool("DemoMode");

                if (Settings.DemoMode) Settings.DemoModeMaxLevel = GameSettings.GetInt("DemoModeMaxLevel");

                Settings.Resolution = GameSettings.GetPoint_V2("Resolution");

                Settings.TitleScreenPath = GameSettings.GetString("TitleScreenPath");

                Settings.WindowType = (WindowType)Enum.Parse(typeof(WindowType), GameSettings.GetString("WindowType"));

                // Load FeatureControl

                Settings.FeatureControl_DumpConsoleMessagesToW32Console_WindowsOnly = GameSettings.GetBool("FC_Dump2Console");
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Fatal error loading settings - Conversion to enum failed.", "Init error", 87);
            }
        }
    }
}
