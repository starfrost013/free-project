using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class Level
    {
        public void SetObjectHealth(int ObjId, int Health)
        {
            foreach (IGameObject GameObject in LevelIGameObjects)
            {
                if (GameObject.GameObjectINTERNALID == ObjId)
                {
                    GameObject.GameObjectHEALTH = Health;
                }
            }
        }
    }
}
