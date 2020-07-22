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
        /// Gets the object sprite with cached ID CacheId
        /// </summary>
        /// <param name="CacheId">The cache ID we want to get.</param>
        /// <returns></returns>
        public IntPtr GetObjSpriteWithCacheId(int CacheId)
        {
            // if we have an invalid ID
            if (CacheId > TextureCache.CachedItems.Count - 1 || CacheId < 0)
            {
                // return zero
                return IntPtr.Zero; 
            }
            else
            { 
                // return the sprite (an IntPtr to the sprite data)
                return TextureCache.CachedItems[CacheId].Spr;
            }
        }
    }
}
