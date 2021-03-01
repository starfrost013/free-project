using Emerald.Core;
using Emerald.Utilities;
using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    /// <summary>
    /// SDL2 Renderer
    /// 
    /// Managed Key Class
    /// 
    /// Handles key services for SDL
    /// </summary>
    public class SDL_Key
    {
        public string KeyName { get; set; }

        public static SDL_Key FromString()
        {
            // While scancodes miss a few characters,
            // they are easier to parse
            foreach (SDL.SDL_Scancode Keymod in Enum.GetValues(typeof(SDL.SDL_Scancode)))
            {
                // put stuff here
            }

            return null;
        }
        
        private static SDL_Key Parse(SDL.SDL_Scancode Keymod)
        {
            string KeyString = Keymod.ToString();

            Debug.Assert(KeyString.Contains('_'));

            string[] UnderscoreArray = KeyString.Split('_');

            if (UnderscoreArray.Length < 3)
            {
                Error.ThrowV2("Error!", "Attempted to convert invalid SDL_Key!", ErrorSeverity.FatalError, 503);
                return null;

            }
            else
            {
                SDL_Key SK = new SDL_Key();
                SK.KeyName = UnderscoreArray[2];

                if (KeyString.ContainsCaseInsensitive(KeyString))
                {
                    return SK;
                }
                else
                {
                    return null; 
                }
                
            }
        }
        public static SDL_Key FromKeysym(SDL.SDL_Keysym Keysym)
        {
            SDL.SDL_Scancode CScancode = Keysym.scancode;
            return null; //temp
        }

    }
}
