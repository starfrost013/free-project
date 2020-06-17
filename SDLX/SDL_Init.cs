using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// SDLX/SDLX/SDL_Init.cs
/// 
/// Created: 2020-06-14
/// 
/// Modified: 2020-06-15
/// 
/// Version: 1.10 (bringup-2.21.1375.55 v1.00 → v1.10): Added SDL_WindowPtr and SDL_RenderPtr
/// 
/// Purpose: Provides rendering initalisation services utilising C# bindings for the Simple DirectMedia Layer, version 2.0.x where SDLX is enabled..
/// 
/// </summary>
namespace SDLX
{
    public class Game
    {
        /// <summary>
        /// Unmanaged code pointer to the SDL game window.
        /// </summary>
        public IntPtr SDL_WindowPtr { get; set; }
        /// <summary>
        /// Unmanaged code pointer to the SDL game renderer.
        /// </summary>
        public IntPtr SDL_RenderPtr { get; set; }

        public bool Game_Init()
        {
            if (!Game_InitSDL()) return false;

            // Add ressolution 
            if (!Game_InitSDL_Window()) return false;

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

        public bool Game_InitSDL_Window()
        {
            // Work around C# shit

            IntPtr _ = SDL_WindowPtr;
            IntPtr _2 = SDL_RenderPtr;

            if (SDL.SDL_CreateWindowAndRenderer(800, 450, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE, out _, out _2) < 0)
            {
                Debug.WriteLine($"SDL createwindow failure: {SDL.SDL_GetError()}");
                return false;
            }
            else
            {
                SDL_WindowPtr = _;
                SDL_RenderPtr = _2;
                return true;
            }

            
        }


    }
}
