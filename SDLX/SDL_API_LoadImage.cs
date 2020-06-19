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
        public List<SDL.SDL_Surface> SDLSurfaceCache { get; set; }
        public void LoadImage(string ImageLoad)
        {
            var ImagePtr = SDL_image.IMG_LoadTexture(SDL_RenderPtr, ImageLoad); 
            
        }
    }
}
