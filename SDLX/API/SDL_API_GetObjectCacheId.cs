using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public partial class Game
    {
        public IntPtr GetObjSpriteWithCacheId(int CacheId)
        {
            if (CacheId > CurrentScene.TextureCache.CachedItems.Count - 1)
            {
                return IntPtr.Zero; 
            }
            else
            {
                return CurrentScene.TextureCache.CachedItems[CacheId].Spr;
            }
        }
    }
}
