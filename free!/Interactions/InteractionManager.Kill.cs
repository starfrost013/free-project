using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL
    {
        public IGameObject Interaction_Kill(IGameObject GameObject)
        {
            GameObject.GameObjectPLAYERHEALTH = 0;
            return GameObject;
        }
    }
}
