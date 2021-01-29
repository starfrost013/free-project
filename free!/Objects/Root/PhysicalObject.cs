using Emerald.Utilities.Wpf2Sdl;
using SDLX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class PhysicalObject : RootObject
    {
        public Animation CurrentAnimation { get; set; }
        public bool CanCollide { get; set; }
        public ObjectPhysFlags PhysicsFlags { get; set; }
        public SDLPoint Position { get; set; }

        /// <summary>
        /// Temporary until we have an SDLVector class.
        /// </summary>
        public SDLPoint Size { get; set; }
        public new void SetName(string NewName) => base.SetName(NewName);
    }
}
