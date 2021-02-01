using Emerald.Utilities.Wpf2Sdl; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Free
{
    public class PhysicsState
    { 
        /// <summary>
        /// Acceleration of this object.
        /// </summary>
        /// 
        
        public SDLPoint Acceleration { get; set; }

        [XmlElement("Inertia")]
        public double Inertia { get; set; }

        [XmlElement("Mass")]
        public double Mass { get; set; }

        [XmlElement("MaxSpeed")]
        public double MaxSpeed { get; set; }

        public double GetInertia() => Mass;

        public double GetMass() => Mass;

        public void SetMass(double NewMass) => Mass = NewMass;


        
    }
}
