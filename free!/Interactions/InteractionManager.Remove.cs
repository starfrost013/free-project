using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class FreeSDL
    {
        public void Interaction_Remove(IGameObject GameObject, Interaction interaction)
        {
            GameObject.GameObjectCOLLISIONS--;
            GameObject.CollidedLevelObjects.Remove(GameObject);
            DeleteGameObject(GameObject, interaction.ObjId2ID);
            return;
        }
    }
}
