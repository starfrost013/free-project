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
        public int CachedSpriteRenderId { get; set; }
        public int LocalID { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public SDLPoint Position { get; set; }
        public SDL.SDL_Rect RenderRect { get; set; }

        public SDLPoint Size { get; set; }
        public IntPtr Sprite { get; set; }

        public void SetPosition(SDLPoint Pos)
        {
            Position = Pos;
        }

        public void SetSize(SDLPoint Siz)
        {
            Size = Siz;
        }

    }

}
