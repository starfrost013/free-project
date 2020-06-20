﻿using SDL2;
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
        public bool RunningNow { get; set; }
        public void SDL_Main()
        {
            SDL.SDL_Event _ = EventHandler;

            while (RunningNow)
            {
                while (SDL.SDL_PollEvent(out _) == 1)
                {

                    if (_.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        Game_Shutdown();
                    }

                    // Clear the SDL window
                    SDL.SDL_RenderClear(SDL_RenderPtr);

                    // Render each SDLTexture in the TextureCache. 
                    // Pending major refactoring we just draw everything at 0,0 for now.
                    foreach (SDL_Sprite SDLSprite in SDLTextureCache)
                    {

                        SDL.SDL_Rect SpriteDestRect = new SDL.SDL_Rect();

                        // Temporary 0,0 
                        SDL.SDL_Rect SpriteRenderRect = SDLSprite.RenderRect;

                        SpriteDestRect.w = SpriteRenderRect.w;
                        SpriteDestRect.h = SpriteRenderRect.h;

                        SDL.SDL_RenderCopy(SDL_RenderPtr, SDLSprite.Sprite, ref SpriteRenderRect, ref SpriteDestRect);
                    }

                    SDL.SDL_RenderPresent(SDL_RenderPtr);
                }
            }

            Game_Shutdown();

        }
    }
}
