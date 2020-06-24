using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// Filename: InteractionManager.Hurt.cs
/// 
/// Created: 2019-11-20
/// 
/// Modified: 2019-12-30
/// 
/// Purpose: Manages the Hurt interaction in V0.07+
/// 
/// </summary>

namespace Free
{
    public partial class FreeSDL
    {
        public void Interaction_Hurt(IGameObject Obj, int amount)
        {
            Obj.OBJPLAYERHEALTH -= amount;
            return;
        }
    }
}
