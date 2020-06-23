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
                Settings.DebugMode = GameSettings.GetBool("DebugMode");

                Settings.DemoMode = GameSettings.GetBool("DemoMode");

                if (Settings.DemoMode) Settings.DemoModeMaxLevel = GameSettings.GetInt("DemoModeMaxLevel");

                Settings.Resolution = GameSettings.GetPoint("Resolution");

                Settings.TitleScreenPath = GameSettings.GetString("TitleScreenPath");

                Settings.UseSDLX = GameSettings.GetBool("UseSDLX");

                Settings.WindowType = (WindowType)Enum.Parse(typeof(WindowType), GameSettings.GetString("WindowType"));
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Fatal error loading settings - Conversion to enum failed.", "Init error", 87);
            }
        }
    }
}
