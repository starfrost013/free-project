using Emerald.Utilities;
using SDL2;
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

        public SDL_CachedItem LoadCachedItem(IntPtr UnmanagedRenderer, string Path)
        {
            SDL_CachedItem SCI = new SDL_CachedItem();
            
            if (!SCI.Load(UnmanagedRenderer, Path))
            {
                return null;
            }
            else
            {
                SCI.ID = CachedItems.Count - 1;
                CachedItems.Add(SCI); 
                return SCI;
            }
        }
    }

    public partial class SDL_CachedItem
    {
        public int ID { get; set; }
        public IntPtr Spr { get; set; }

        /// <summary>
        /// Loads a cached item
        /// </summary>
        /// <param name="Path">The path to the cached item to load</param>
        /// <returns></returns>
        public bool Load(IntPtr UnmanagedRenderer, string Path)
        {

            // figure out what the hell is going on here
            IntPtr _ = SDL_image.IMG_LoadTexture(Game.SDL_RenderPtr, Path);

            if (_ == IntPtr.Zero)
            {
                System.Diagnostics.Debug.WriteLine(SDL.SDL_GetError()); // TEMP
                return false;
            }
            else
            {
                Spr = _;
                return true; 
            }
        }
    }

    public partial class SDL_AnimatedCachedItem
    {
        public List<IntPtr> SprFrm { get; set; }
        public int ID { get; set; }
        public int CurrentFrame { get; set; }

        public SDL_AnimatedCachedItem()
        {
            SprFrm = new List<IntPtr>();
        }

        public bool LoadSingleFrame(IntPtr UnmanagedRenderer, string Path)
        {
            IntPtr NewSprFrm = SDL_image.IMG_LoadTexture(UnmanagedRenderer, Path);

            if (NewSprFrm == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                SprFrm.Add(NewSprFrm);
                return true;
            }
        }
    }
}
