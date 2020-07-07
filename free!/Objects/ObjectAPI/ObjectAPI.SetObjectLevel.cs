using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class Level
    {
        public void SetObjectLevel(int ObjId, int Level)
        {
            foreach (IGameObject GameObject in LevelIGameObjects)
            {
                if (GameObject.GameObjectINTERNALID == ObjId)
                {
                    GameObject.GameObjectLEVEL = Level; 
                }
            }
        }
    }
}
