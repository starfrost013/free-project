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
        public int GameObject1ID { get; set; } // the ID of the IGameObject causing the interaction.
        public int GameObject2ID { get; set; } // the ID of the IGameObject being affected by the interaction. (change to list?)
        public InteractionType GameObjectINTERACTIONTYPE { get; set; } // the interaction type. 
    }
}
