using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// SDLX/API/SDL_API_SetObjectPosition.cs
/// 
/// Created: 2020-07-20
/// 
/// Modified: 2020-07-20
/// 
/// Version: 1.00
/// 
/// Purpose: Glue API between other components and SDLX to set the position of an SDL object. External use only.
/// 
/// </summary>

namespace SDLX
{
    public partial class GameScene
    {
        public void SetObJPosition(int ID, SDLPoint Position)
        {
            foreach (SDLSprite SDLSpr in LevelSprites)
            {
               if (SDLSpr.LocalID == id)
                {
                    SDLSpr.SetPosition(Position); 
                }
            }
        }
    }
}
