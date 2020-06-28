using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public partial class GameScene
    {
        /// <summary>
        /// Somewhat better.
        /// </summary>
        /// <param name="SpriteSDL"></param>
        public void CacheImage(SDLSprite SpriteSDL)
        {
            // Temporary variable
            int Hits = 0;

            foreach (SDLSprite SDLSpr in SDLTextureCache)
            {
                if (SpriteSDL.Name == SDLSpr.Name)
                {
                    Hits += 1; 
                }
            }

            if (Hits == 0) CachedTextures.Add(SpriteSDL); 
        }

        public SDLSprite SelectCachedTexture(SDLSprite SpriteSDL)
        {
            foreach (SDLSprite Spr in CachedTextures)
            {
                if (Spr.Name == SpriteSDL.Name)
                {
                    return Spr;
                }
            }
        }
    }
}
