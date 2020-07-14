using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public partial class SDL_Cache
    {
        public List<SDL_CachedItem> CachedItems { get; set; }

        public SDL_CachedItem GetCachedItemWithId(int ID)
        {
            if (ID > CachedItems.Count - 1)
            {
                return null;
            }
            else
            {
                return CachedItems[ID];
            }
        }
    }

    public partial class SDL_CachedItem
    {
        public int ID { get; set; }
        public IntPtr Spr { get; set; }

        public void Load(string Path)
        {

        }
    }

    public partial class SDL_AnimatedCachedItem
    {
        public List<IntPtr> SprFrm { get; set; }
        public int ID { get; set; }
        public int CurrentFrame { get; set; }
    }
}
