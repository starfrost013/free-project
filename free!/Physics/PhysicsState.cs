using Emerald.Utilities.Wpf2Sdl; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class PhysicsState
    { 
        /// <summary>
        /// Acceleration of this object.
        /// </summary>
        public SDLPoint Acceleration { get; set; }
        public double Inertia { get; set; }
        
    }
}
