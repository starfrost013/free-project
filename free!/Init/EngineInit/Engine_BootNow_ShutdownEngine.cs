using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    public partial class FreeSDL
    {
        public void BootNow_ShutdownEngine()
        {

#if DEBUG
            LogDebug_C("BootNow!", "Shutting down...");

            LogDebug_C("BootNow!", "Clearing current level.");
#endif

            if (currentlevel != null)
            {
                currentlevel.LevelIGameObjects.Clear();
            }
            

#if DEBUG
            LogDebug_C("SDL2 Renderer", "Shutting down...");    
#endif
            SDLGame.Game_Shutdown();

            Application.Current.Shutdown(0);             
        }
    }
}
