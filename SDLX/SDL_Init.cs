using Emerald.Utilities.Wpf2Sdl;
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
/// Modified: 2020-06-21
/// 
/// Version: 1.40 (core_new_source-v2.21.1402.65 v1.30 → v1.40): added title setting
/// 
/// Purpose: Provides rendering initalisation services utilising C# bindings for the Simple DirectMedia Layer, version 2.0.x where SDLX is enabled..
/// 
/// </summary>
namespace SDLX
{
    public partial class Game
    {
        /// <summary>
        /// Unmanaged code pointer to the SDL game window.
        /// </summary>
        public IntPtr SDL_WindowPtr { get; set; }

        /// <summary>
        /// Unmanaged code pointer to the SDL game renderer.
        /// </summary>
        public static IntPtr SDL_RenderPtr { get; set; }
        

        public bool Game_Init()
        {

            if (!Game_InitScene()) return false;

            if (!Game_InitSDL()) return false;

            // Add resolution 
            if (!Game_InitSDL_Window()) return false;

            return true;
        }

        private bool Game_InitSDL()
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
                return false;
            }

            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) < 0)
            {
                Debug.WriteLine($"SDL_image init fail: {SDL.SDL_GetError()}"); 
                return false;
            }

            if (SDL_ttf.TTF_Init() < 0)
            {
                Debug.WriteLine($"SDL_ttf init fail: {SDL.SDL_GetError()}");
                return false;
            }

#if DEBUG
            SDL.SDL_LogSetAllPriority(SDL.SDL_LogPriority.SDL_LOG_PRIORITY_WARN);
#endif

            return true;
            
        }

        private bool Game_InitSDL_Window()
        {
            // Work around C# shit

            IntPtr _ = SDL_WindowPtr;
            IntPtr _2 = SDL_RenderPtr;



            if (SDL.SDL_CreateWindowAndRenderer(800, 450, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE, out _, out _2) < 0)
            {
                Debug.WriteLine($"SDL createwindow failure: {SDL.SDL_GetError()}");

                //Set scene resolution (TEMP)
                CurrentScene.Resolution.X = 800;
                CurrentScene.Resolution.Y = 450;
                return false;
            }
            else
            {
                SDL.SDL_SetWindowTitle(_, "Emerald Simple DirectMedia Layer Renderer");


                SDL_WindowPtr = _;
                SDL_RenderPtr = _2;

                CurrentScene.PreLoadScene();

                RunningNow = true;

                return true;
            }
        }

        // this will be used for more later
        private bool Game_InitScene()
        {
            CurrentScene = new GameScene();


            return true; 
        }

        public void Game_Shutdown()
        {
            // Shutdown various things
            RunningNow = false; 

            SDL.SDL_DestroyRenderer(SDL_RenderPtr);
            SDL.SDL_DestroyWindow(SDL_WindowPtr);

            SDL_image.IMG_Quit(); 

            SDL.SDL_AudioQuit();

            SDL.SDL_VideoQuit();

            SDL.SDL_Quit();

            // bad
            Environment.Exit(0); 
        }

    }
}
