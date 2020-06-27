using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// SDLX/SDL_Internal_KeyHandler.cs
/// 
/// Created: 2020-06-24
/// 
/// Modified: 2020-06-24
/// 
/// Version: 1.00
/// 
/// Purpose: SDL key handling. (Temporary)
/// </summary>

namespace SDLX
{
    public partial class Game
    {
        private void HandleKeys(SDL.SDL_KeyboardEvent KeyX)
        {
            // Highly temporary (everything is temporary essentially) (EngineCore?)
            switch (KeyX.keysym.sym)
            {
                case SDL.SDL_Keycode.SDLK_w:
                    CurrentScene.GameCamera.CameraPosition.Y -= 5; 
                    return;
                case SDL.SDL_Keycode.SDLK_a:
                    CurrentScene.GameCamera.CameraPosition.X -= 5;
                    return;
                case SDL.SDL_Keycode.SDLK_s:
                    CurrentScene.GameCamera.CameraPosition.Y += 5;
                    return;
                case SDL.SDL_Keycode.SDLK_d:
                    CurrentScene.GameCamera.CameraPosition.X += 5;
                    return; 
            }
        }
    }
}
