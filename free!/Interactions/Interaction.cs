using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public enum InteractionType { Bounce, BounceLeft, BounceRight, ChangeLevel, Crush, Fall, Hurt, Kill, PlayAnimation, Remove, Teleport, ShowText, SpeedUp }
    public class Interaction
    {
        public int OBJ1ID { get; set; } // the ID of the object causing the interaction.
        public int OBJ2ID { get; set; } // the ID of the object being affected by the interaction. (change to list?)
        public InteractionType OBJINTERACTIONTYPE { get; set; } // the interaction type. 
    }
}
