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
        public GameScene CurrentScene { get; set; }
        public SDL.SDL_Event EventHandler { get; set; } // naive?
        public bool RunningNow { get; set; }


        /// <summary>
        /// Sorta shit but works for now. Reimplement later.
        /// </summary>
        public void SDL_Main()
        {
            SDL.SDL_Event _ = EventHandler;

            // Initial colour
            SDL_SetBgColour(new SDLColor { R = 170, G = 170, B = 255, A = 255 });

            while (RunningNow)
            {

                // Clear the SDL window
                SDL.SDL_RenderClear(SDL_RenderPtr);

                while (SDL.SDL_PollEvent(out _) != 0)
                {
                    // Check for the events we want to handle.

                    // The user closed the window so we shutdown the game.
                    if (_.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        Game_Shutdown();
                    }
                    // The user pressed a key.
                    else if (_.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        HandleKeys(_.key);
                    }
                }

                // Draw the background. (TEMP)

                SDL_DrawBackground();


                // Render each SDLTexture in the TextureCache. 

                foreach (SDLSprite SDLSprite in CurrentScene.LevelSprites)
                {
                    if (SDLSprite.Position.X >= (CurrentScene.GameCamera.CameraPosition.X - SDLSprite.Size.X)
                        && SDLSprite.Position.X <= (CurrentScene.GameCamera.CameraPosition.X + (CurrentScene.Resolution.X + SDLSprite.Size.X))
                        && SDLSprite.Position.Y >= (CurrentScene.GameCamera.CameraPosition.Y - SDLSprite.Size.Y)
                        && SDLSprite.Position.Y <= (CurrentScene.GameCamera.CameraPosition.Y + (CurrentScene.Resolution.Y + SDLSprite.Size.Y)))
                    {
                        SDL.SDL_Rect SpriteDestRect = SDL_GetRect(new SDLPoint(SDLSprite.Position.X - CurrentScene.GameCamera.CameraPosition.X, SDLSprite.Position.Y - CurrentScene.GameCamera.CameraPosition.Y), new SDLPoint(SDLSprite.RenderRect.w, SDLSprite.RenderRect.h));

                        SDL.SDL_Rect SpriteRenderRect = SDLSprite.RenderRect;

                        SpriteDestRect.x = (int)SDLSprite.Position.X - (int)CurrentScene.GameCamera.CameraPosition.X;
                        SpriteDestRect.y = (int)SDLSprite.Position.Y - (int)CurrentScene.GameCamera.CameraPosition.Y;

                        SpriteDestRect.w = SpriteRenderRect.w;
                        SpriteDestRect.h = SpriteRenderRect.h;


                        SDL.SDL_RenderCopy(SDL_RenderPtr, SDLSprite.Sprite, ref SpriteRenderRect, ref SpriteDestRect);
                    }

                    // TEMPORARY CODE START DO NOT USE AFTER FREEUI


                    SDL_DrawText();

                    // TEMPORARY CODE END DO NOT USE AFTER FREEUI

                }
                

                SDL.SDL_RenderPresent(SDL_RenderPtr);
            }

            Game_Shutdown();

        }

        public void SDL_DrawBackground()
        {
            SDL.SDL_Rect BgRenderRect = SDL_GetRect(new SDLPoint(0, 0), CurrentScene.Resolution);
            SDL.SDL_Rect _2 = CurrentScene.Background.RenderRect;

            SDL.SDL_RenderCopy(SDL_RenderPtr, CurrentScene.Background.Sprite, ref _2, ref BgRenderRect);
        }

        public void SDL_DrawText()
        {
            // Render the text to a surface.
            IntPtr Surface = SDL_ttf.TTF_RenderText_Solid(CurrentScene.TEMP_SHITTY_DONTUSE_FONTTTFCONSOLAS, $"Camera Position: {CurrentScene.GameCamera.CameraPosition.X}, {CurrentScene.GameCamera.CameraPosition.Y}", new SDL.SDL_Color { a = 255, r = 0, g = 0, b = 0 });

            // Render the text surface to a texture.
            IntPtr Texture = SDL.SDL_CreateTextureFromSurface(SDL_RenderPtr, Surface);

            SDL.SDL_Rect SrcRect = SDL_GetRect(new SDLPoint(0, 0), new SDLPoint(300, 20));

            SDL.SDL_Rect DestRect = SrcRect;

            SDL.SDL_RenderCopy(SDL_RenderPtr, Texture, ref SrcRect, ref DestRect);

            SDL.SDL_DestroyTexture(Texture);
            SDL.SDL_FreeSurface(Surface);
        }

        /// <summary>
        /// I wonder what this does.
        /// </summary>
        public void SDL_LoadAndCacheTexture(string Path)
        {
            TextureCache.LoadCachedItem(Path); 
        }
    }
}
