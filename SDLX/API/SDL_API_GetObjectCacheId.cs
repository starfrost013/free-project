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
            if (CacheId > TextureCache.CachedItems.Count - 1)
            {
                return IntPtr.Zero; 
            }
            else
            {
                return TextureCache.CachedItems[CacheId].Spr;
            }
        }
    }
}
