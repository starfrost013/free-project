using Emerald.Utilities.Wpf2Sdl;
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
        public void SDL_SetBgColour(SDLColor Colour)
        {
            SDL.SDL_SetRenderDrawColor(SDL_RenderPtr, Colour.R, Colour.G, Colour.B, Colour.A);
        }
    }
}
