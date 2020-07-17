﻿using SDL2;
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

        public SDL_CachedItem LoadCachedItem(string Path)
        {
            SDL_CachedItem SCI = new SDL_CachedItem();
            
            if (!SCI.Load(Path))
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
        public bool Load(string Path)
        {
            Spr = SDL_image.IMG_Load(Path);

            if (Spr == IntPtr.Zero)
            {
                return false;
            }
            else
            {
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

        public bool LoadSingleFrame(string Path)
        {
            IntPtr NewSprFrm = SDL_image.IMG_Load(Path);

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
