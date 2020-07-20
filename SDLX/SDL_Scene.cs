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
    public partial class GameScene
    {
        public SDLSprite Background { get; set; } // We might need to fix this before it gets out of control...Shared classes?
        public List<SDLSprite> LevelSprites { get; set; }
        public GameCamera GameCamera { get; set; }
        public SDLPoint Resolution { get; set; }
        private IntPtr TEMP_SHITTY_DONTUSE_FONTTTFCONSOLAS { get; set; } // Pre-FreeUI font
        private SDL_Cache TextureCache { get; set; }

        public GameScene()
        {
            GameCamera = new GameCamera();
            Background = new SDLSprite();
            Resolution = new SDLPoint(850, 400);
            TextureCache = new SDL_Cache();
            TextureCache.CachedItems = new List<SDL_CachedItem>();
            LevelSprites = new List<SDLSprite>();
        }

        public void SDL_LoadLevel(string BackgroundPath)
        {
            LoadScene(BackgroundPath);
        }

        public void PreLoadScene()
        {
            TEMP_SHITTY_DONTUSE_FONTTTFCONSOLAS = SDL_ttf.TTF_OpenFont("consolaz.ttf", 18);

            if (TEMP_SHITTY_DONTUSE_FONTTTFCONSOLAS == IntPtr.Zero)
            {
                Debug.WriteLine($"Error loading font: {SDL.SDL_GetError()}");
            }
        }

        private void LoadScene(string BackgroundPath)
        {
            LoadBackground(BackgroundPath);
        }

        /// <summary>
        /// Loads the background. Will be replaced by a single call to LoadScene. 
        /// </summary>
        /// <param name="BackgroundPath"></param>
        private void LoadBackground(string BackgroundPath)
        {

            Background.Sprite = SDL_image.IMG_LoadTexture(Game.SDL_RenderPtr, BackgroundPath);
           
            Background.RenderRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                // TEMPORARY we will have configurable background sizes
                w = (int)Resolution.X,
                h = (int)Resolution.Y
            };

            // More temp stuff

            Background.Size = Resolution;
        }

        // 2020-07-16 (v1462.106): now use sdl caching
        public bool LoadImage(int ObjectId, SDLPoint Position, int SizeX, int SizeY)
        {
            // Load the image using SDLImage
            IntPtr ImagePtr = GetObjSpriteWithCacheId(ObjectId);
            
            if (ImagePtr == new IntPtr(0))
            {
                Debug.WriteLine("An error occurred loading an image - ImagePtr returned null.");
                return true; // return false once it works. THIS IS SHIT!!!
            }

            // TEMPORARY - these will be parameters
            uint Tex = SDL.SDL_PIXELFORMAT_UNKNOWN;
            int TexAccess = (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC;

            if (SDL.SDL_QueryTexture(ImagePtr, out Tex, out TexAccess, out SizeX, out SizeY) < 0)
            {
                Debug.WriteLine($"An error occurred while caching this image... {SDL.SDL_GetError()} ");

                // Don't return false for now. Not sure what's causing the "Invalid texture." errors.
                //return false;
                return true;
            }
            else
            {
                // Create a new SDLX sprite. 
                SDLSprite SDLSprite = new SDLSprite();
                SDLSprite.Sprite = ImagePtr;

                SDLSprite.LocalID = LevelSprites.Count - 1; 
                // Create a new SDL rect.
                SDL.SDL_Rect RenderRect = new SDL.SDL_Rect();
                RenderRect.x = 0;
                RenderRect.y = 0;
                RenderRect.w = SizeX;
                RenderRect.h = SizeY;

                SDLSprite.RenderRect = RenderRect;

                SDLSprite.Position = Position;

                SDLSprite.Size = new SDLPoint(SizeX, SizeY);
                
                LevelSprites.Add(SDLSprite);
                return true;
            }
        }
    }
}
