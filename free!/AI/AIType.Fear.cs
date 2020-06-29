using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL
    {
        public void AI_Fear(IGameObject GameObject, IGameObject GameObject2)
        {
            if (GameObject.GameObjectX - GameObject2.GameObjectX < this.Width / 1.5 & GameObject.GameObjectX - GameObject.GameObjectX > -this.Width / 1.5 & GameObject.GameObjectY - GameObject2.GameObjectY < this.Height / 2 & GameObject.GameObjectY - GameObject2.GameObjectY > -this.Height / 2)
            {
                if (GameObject2.GameObjectSPEED > 0.1 & GameObject2.GameObjectMOVERIGHT | GameObject2.GameObjectSPEED < -0.1 & GameObject2.GameObjectMOVELEFT)
                {
                    //TEMP
                    GameObject.MoveRight(); //TEMPCODE. 
                }
            }
        }
    }
}
