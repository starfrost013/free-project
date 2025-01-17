﻿using Emerald.Core;
using Emerald.Utilities;
using Emerald.Utilities.Wpf2Sdl;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace SDLX
{
    public partial class Game
    {
        public GameScene CurrentScene { get; set; }
        public SDL.SDL_Event EventHandler { get; set; } // naive?
        public bool RunningNow { get; set; }

        /// <summary>
        /// TEMP - FPS COUNTER
        /// </summary>
        public int Debug_FPS { get; set; }

        public Timer Debug_Timer { get; set; }

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

                int IsEventAvailable = SDL.SDL_PollEvent(out _);

                switch (IsEventAvailable)
                {
                    case 0:
                        // Draw the background. (TEMP)

                        SDL_DrawBackground();
                        Debug_FPS++;
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


                        }

                        //vtemp
                        SDL_DrawText();

                        // TEMPORARY CODE END DO NOT USE AFTER FREEUI
                        SDL.SDL_RenderPresent(SDL_RenderPtr);
                        continue;
                    case 1:
                        SDLDebug.LogDebug_C("SDL Event Handler", $"SDL is alive - handling event {_.type}");
                        switch (_.type)
                        {
                            case SDL.SDL_EventType.SDL_QUIT:
                                Game_Shutdown();
                                
                                continue;
                            case SDL.SDL_EventType.SDL_KEYDOWN:
                                HandleKeys(_.key);

                                continue;

                        }

                        continue;

                }

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
            // TEMPORARY
            // Render the text to a surface.
            IntPtr Surface = SDL_ttf.TTF_RenderText_Solid(CurrentScene.TEMP_SHITTY_DONTUSE_FONTTTFCONSOLAS, $"Camera Position: {CurrentScene.GameCamera.CameraPosition.X}, {CurrentScene.GameCamera.CameraPosition.Y}", new SDL.SDL_Color { a = 255, r = 255, g = 255, b = 255 });

            EngineVersion EV = new EngineVersion();
            string VString = EV.GetVersionString();

            IntPtr EngineVersion = SDL_ttf.TTF_RenderText_Solid(CurrentScene.TEMP_SHITTY_DONTUSE_FONTTTFCONSOLAS, $"Engine Version: {VString}", new SDL.SDL_Color { a = 255, r = 255, g = 255, b = 255 } );
            
            // Render the text surface to a texture.
            IntPtr Texture = SDL.SDL_CreateTextureFromSurface(SDL_RenderPtr, Surface);
            IntPtr VerHsx = SDL.SDL_CreateTextureFromSurface(SDL_RenderPtr, EngineVersion);

            SDL.SDL_Rect SrcRect = SDL_GetRect(new SDLPoint(0, 0), new SDLPoint(300, 20));
            SDL.SDL_Rect VerRect = SDL_GetRect(new SDLPoint(0, 40), new SDLPoint(300, 20));

            SDL.SDL_Rect DestRect = SrcRect;
            SDL.SDL_Rect VerDestRect = VerRect;

            SDL.SDL_RenderCopy(SDL_RenderPtr, Texture, ref SrcRect, ref DestRect);
            SDL.SDL_RenderCopy(SDL_RenderPtr, VerHsx, ref VerRect, ref VerDestRect);
            SDL.SDL_DestroyTexture(Texture);
            SDL.SDL_DestroyTexture(VerHsx);
            SDL.SDL_FreeSurface(Surface);
            SDL.SDL_FreeSurface(EngineVersion);
        }

        /// <summary>
        /// I wonder what this does.
        /// </summary>
        public void SDL_LoadAndCacheTexture(string RPath)
        {
            CurrentScene.TextureCache.LoadCachedItem($"{RPath}"); 
        }

        public void TEMP__SDL_DrawFPSCount(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"FPS: {Debug_FPS}");
            Debug_FPS = 0; 
        }
    }
}
