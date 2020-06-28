using Emerald.Utilities.Wpf2Sdl;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    /// <summary>
    /// SDL2 2D accelerated rendering API.
    /// </summary>
    public class SDLSprite
    {
        public IntPtr Sprite { get; set; }
        public SDL.SDL_Rect RenderRect { get; set; }
        public SDLPoint Position { get; set; }
    }
}
