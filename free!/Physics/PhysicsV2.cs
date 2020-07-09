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

                if (!GameObject.GameObjectISJUMPING)
                {
                    GameObject.GameObjectSPEEDY = 0;
                    GameObject.GameObjectACCELERATIONY = 0; 
                }

            }
            else
            {
                if (GameObject.CollisionsRight > 0 || GameObject.CollisionsLeft > 0)
                {
                    GameObject.GameObjectSPEED = 0;
                }

                GameObject.GameObjectACCELERATIONY += Physics.Gravity;

            }


            if (!GameObject.GameObjectMOVELEFT && GameObject.LastControl == LastCtrl.MoveLeft)
            {
                if (GameObject.GameObjectSPEED < 0)
                {
                    GameObject.GameObjectDECELERATION += Physics.Friction;
                }
                else
                {
                    GameObject.GameObjectDECELERATION = 0; 
                }
            }

            if (!GameObject.GameObjectMOVERIGHT && GameObject.LastControl == LastCtrl.MoveRight)
            {
                if (GameObject.GameObjectSPEED > 0)
                {
                    GameObject.GameObjectDECELERATION += Physics.Friction;
                }
                else
                {
                    GameObject.GameObjectDECELERATION = 0;
                }

            }
            GameObject.GameObjectSPEED += GameObject.GameObjectACCELERATION;
            GameObject.GameObjectSPEEDY += GameObject.GameObjectACCELERATIONY;
            GameObject.GameObjectSPEED -= GameObject.GameObjectDECELERATION;
            GameObject.GameObjectSPEEDY -= GameObject.GameObjectDECELERATIONY;

            GameObject.Position.X += GameObject.GameObjectSPEED;
            GameObject.Position.Y += GameObject.GameObjectSPEEDY;


        }
    }
}
