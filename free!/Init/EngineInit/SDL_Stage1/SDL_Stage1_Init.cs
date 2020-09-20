using Emerald.Core; 
using SDLX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Second stage initialisation for SDL-based free! (version 3.1.1488.20204+)
/// </summary>

namespace Free
{
    public partial class Game_SDL
    {
        public Game SDLGame { get; set; }

        public void SDL_Init_Stage1()
        {

            // Check that we're loaded. TODO: LOAD GAMEINFO

            if (!SettingLoader.IsLoaded)
            {
                Error.Throw(null, ErrorSeverity.FatalError, "Error - settings have not been loaded. You likely didn't use the Launcher.", "Please use the Launcher", 101);
            }

            SDLGame.Game_Init();

        }
    }
}
