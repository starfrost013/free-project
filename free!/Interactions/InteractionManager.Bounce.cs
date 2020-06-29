using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL
    {
        public void Interaction_Bounce(IGameObject GameObject)
        {
            // Wow this is an ugly hack and we need to get rid of it.
            GameObject.GameObjectY -= 10;
            GameObject.GameObjectACCELERATIONY = -8.5;
            
            return;
        }
    }
}
