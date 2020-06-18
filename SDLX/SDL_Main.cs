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
                
                switch (_.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        // VERY TEMPORARY CODE
                        Game_Shutdown();
                        continue;
                }

                // Destroy
                SDL.SDL_DestroyRenderer(SDL_RenderPtr);
                SDL.SDL_DestroyWindow(SDL_WindowPtr);
            }
        }
    }
}
