using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL
    {
        public IGameObject Interaction_Kill(IGameObject obj)
        {
            obj.OBJPLAYERHEALTH = 0;
            return obj;
        }
    }
}
