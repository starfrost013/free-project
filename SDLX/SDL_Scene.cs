using Emerald.Utilities.Wpf2Sdl;
using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// SDLX/SDL_Scene.cs
/// 
/// Created: 2020-06-23
/// 
/// Modified: 2020-06-26
/// 
/// Version: 1.30
/// 
/// Purpose: Handles SDL-based rendering engine scenes. 
/// 
/// </summary>

namespace SDLX
{
    public class GameScene
    {
        public GameCamera GameCamera { get; set; }
        public List<SDLSprite> SDLTextureCache { get; set; }
        public SDLPoint Resolution { get; set; }

        public GameScene()
        {
            GameCamera = new GameCamera();
            SDLTextureCache = new List<SDLSprite>();
        }

        public void PreLoadScene()
        {
            SDLTextureCache.Clear();
        }

        public bool LoadImage(string ImageLoad, SDLPoint Position, int SizeX, int SizeY)
        {
            // Load the image using SDLImage
            var ImagePtr = SDL_image.IMG_LoadTexture(Game.SDL_RenderPtr, ImageLoad);

            // TEMPORARY - these will be parameters
            uint Tex = SDL.SDL_PIXELFORMAT_UNKNOWN;
            int TexAccess = (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC;


            // TEMPORARY 64X64
            if (SDL.SDL_QueryTexture(ImagePtr, out Tex, out TexAccess, out SizeX, out SizeY) < 0)
            {
                Debug.WriteLine($"SDL failed loading an image. {SDL.SDL_GetError()} ");
                return false;
            }
            else
            {
                // Create a new SDLX sprite. 
                SDLSprite SDLSprite = new SDLSprite();
                SDLSprite.Sprite = ImagePtr;

                // Create a new SDL rect.
                SDL.SDL_Rect RenderRect = new SDL.SDL_Rect();
                RenderRect.x = 0;
                RenderRect.y = 0;
                RenderRect.w = SizeX;
                RenderRect.h = SizeY;

                SDLSprite.RenderRect = RenderRect;

                SDLSprite.Position = Position;

                // Add it to the texture cache
                SDLTextureCache.Add(SDLSprite);
                return true;
            }
        }
    }
}
