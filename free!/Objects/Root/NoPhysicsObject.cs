using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class NoPhysicsObject : RootObject 
    {
        public string ImagePath { get; set; }
        // todo: SDLVector

        /// <summary>
        /// ObjectType of this object
        /// </summary>
        public static new ObjectTypes OType = ObjectTypes.RootObject;
        public SDLPoint Position { get; set; }
        public SDLPoint Size { get; set; }


    }
}
