using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Free
{

    public class PhysicsDefinition
    {
        public bool AirState { get; set; }

        [XmlElement("CanCollide")]
        public bool CanCollide { get; set; }

        [XmlElement("CanSnap")]
        public bool CanSnap { get; set; }
        public bool IsJumping { get; set; }

        [XmlElement("Weightless")]
        public bool Weightless { get; set; }

        [XmlElement("ImmuneToNewtonsFirstLaw")]
        public bool ImmuneToNewtonsFirstLaw { get; set; }

        [XmlElement("ImmuneToNewtonsSecondLaw")]
        public bool ImmuneToNewtonsSecondLaw { get; set; }

        [XmlElement("ImmuneToNewtonsThirdLaw")]
        public bool ImmuneToNewtonsThirdLaw { get; set; }

        [XmlElement("SolidOnTop")]
        public bool SolidOnTop { get; set; }

        [XmlElement("SolidOnLeft")]
        public bool SolidOnLeft { get; set; }

        [XmlElement("SolidOnRight")]
        public bool SolidOnRight { get; set; }

        [XmlElement("SolidOnBottom")]
        public bool SolidOnBottom { get; set; }

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
