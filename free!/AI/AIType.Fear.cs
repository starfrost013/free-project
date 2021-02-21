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
            /*
            if (GameObject.Position.X - ObjId2.Position.X < this.Width / 1.5 & GameObject.Position.X - GameObject.Position.X > -this.Width / 1.5 & GameObject.Position.Y - ObjId2.Position.Y < this.Height / 2 & GameObject.Position.Y - ObjId2.Position.Y > -this.Height / 2)
            {
                if (ObjId2.GameObjectSPEED > 0.1 & ObjId2.GameObjectMOVERIGHT | ObjId2.GameObjectSPEED < -0.1 & ObjId2.GameObjectMOVELEFT)
                {
                    //TEMP
                    GameObject.MoveRight(); //TEMPCODE. 
                }
            }
            */ // this was crap anyway
        }
    }
}
