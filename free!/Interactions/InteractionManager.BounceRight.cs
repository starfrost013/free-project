using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL // move to GameObject in the GameObject/player split?
    {
        public void Interaction_BounceRight(IGameObject GameObject)
        {
            GameObject.GameObjectACCELERATION = 28.5;
            GameObject.GameObjectSPEED = Physics.MaxSpeed;
            return;
        }
    }
}
