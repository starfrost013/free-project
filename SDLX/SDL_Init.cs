using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SDLX
{
    public class Game
    {
        public bool Game_Init()
        {
            if (!Game_InitSDL()) return false;
            return true;
        }

        public bool Game_InitSDL()
        {
            // Check that we haven't done any stupid crap like running with the wrong SDL version.
            SDL.SDL_version SDL_Ver = new SDL.SDL_version();
            SDL.SDL_GetVersion(out SDL_Ver);
            
            if (SDL_Ver.major < 2)
            {
                Debug.WriteLine("Running on SDL1.x!");
                return false;
            }

            // Initialise SDL.
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) < 0)
            {
                Debug.WriteLine($"SDL init fail: {SDL.SDL_GetError()}"); 
            }

#if DEBUG
            SDL.SDL_LogSetAllPriority(SDL.SDL_LogPriority.SDL_LOG_PRIORITY_WARN);
#endif
            return true;
            
        }
    }
}
