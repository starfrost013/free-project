using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class FreeSDL // move to own class?
    {
        public void RunPhysicsV2(IGameObject GameObject)
        {
            if (GameObject.CollisionsTop > 0)
            {
                // temp
                GameObject.GameObjectSPEEDY = 0;
            }
        }
    }
}
