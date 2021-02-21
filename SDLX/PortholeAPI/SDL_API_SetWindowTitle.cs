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
        public void SDL_API_SetWindowTitle(string WindowTitle)
        {
            SDL.SDL_SetWindowTitle(SDL_WindowPtr, WindowTitle);
        }
    }
}
