using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public partial class Game
    {
        public SDL.SDL_Event EventHandler { get; set; } // naive?
        public void SDL_Main()
        {
            SDL.SDL_Event _ = EventHandler;

            while (SDL.SDL_PollEvent(out _) == 1)
            {

                if (_.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Game_Shutdown();
                }

                // Destroy
                SDL.SDL_DestroyRenderer(SDL_RenderPtr);
                SDL.SDL_DestroyWindow(SDL_WindowPtr);
            }
        }
    }
}
