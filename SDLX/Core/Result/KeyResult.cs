using SDL2;
using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public class KeyResult : ValidationClass
    {
        public SDL_Key Key { get; set; }
        public bool Success { get; set; }
    }
}
