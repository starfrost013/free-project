using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class PhysicsFlags
    {
        public bool AirState { get; set; }
        public bool IsJumping { get; set; }
        public bool Mass { get; set; }
        public bool Weightless { get; set; }
        public bool NotAffectedByGravity { get; set; }
        public bool ImmuneToNewtons1stLaw { get; set; }
        public bool ImmuneToNewtons2ndLaw { get; set; }
        public bool ImmuneToNewtons3rdLaw { get; set; }
        public bool SolidOnTop { get; set; }
        public bool SolidOnLeft { get; set; }
        public bool SolidOnRight { get; set; }
        public bool SolidOnBottom { get; set; }
    }
}
