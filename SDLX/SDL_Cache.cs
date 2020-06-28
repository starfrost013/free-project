using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public partial class GameScene
    {
        /// <summary>
        /// Doesn't work yet
        /// </summary>
        /// <param name="SpriteSDL"></param>
        public void CacheImage(SDLSprite SpriteSDL)
        {
            foreach (SDLSprite SDLSpr in SDLTextureCache)
            {
                if (SDLSpr.Sprite == SpriteSDL.Sprite)
                {
                    CachedTextures.Add(SDLSpr.Sprite); 
                }
            }
        }
    }
}
