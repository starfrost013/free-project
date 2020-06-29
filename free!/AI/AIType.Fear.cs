using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL
    {
        public void AI_Fear(IGameObject GameObject, IGameObject ObjId2)
        {
            if (GameObject.GameObjectX - ObjId2.GameObjectX < this.Width / 1.5 & GameObject.GameObjectX - GameObject.GameObjectX > -this.Width / 1.5 & GameObject.GameObjectY - ObjId2.GameObjectY < this.Height / 2 & GameObject.GameObjectY - ObjId2.GameObjectY > -this.Height / 2)
            {
                if (ObjId2.GameObjectSPEED > 0.1 & ObjId2.GameObjectMOVERIGHT | ObjId2.GameObjectSPEED < -0.1 & ObjId2.GameObjectMOVELEFT)
                {
                    //TEMP
                    GameObject.MoveRight(); //TEMPCODE. 
                }
            }
        }
    }
}
