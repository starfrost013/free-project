using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// Collision/CollisionV2.cs
/// 
/// Created: 2020-06-06
/// 
/// Modified: 2020-06-06
/// 
/// Version: 1.00
/// 
/// Purpose: 
/// 
/// </summary>

namespace Free
{
    public partial class FreeSDL
    {
        public void HandleCollisionV2(IGameObject GameObject)
        {
            foreach (GameObject GameObject2 in currentlevel.LevelIGameObjects)
            {

                // If we collide at the top...
                if (TestCollideTop(GameObject, GameObject2))
                {
                    GameObject.CollidedLevelObjects.Add(GameObject2);
                    GameObject.CollisionsTop++;
                }
                // Or at the bottom...
                else if (TestCollideBottom(GameObject, GameObject2))
                {
                    GameObject.CollidedLevelObjects.Add(GameObject2);
                    GameObject.CollisionsBottom++;
                }
                // Or to the left...
                else if (TestCollideLeft(GameObject, GameObject2))
                {
                    GameObject.CollidedLevelObjects.Add(GameObject2);
                    GameObject.CollisionsLeft++;
                }
                // Or to the right!
                else if (TestCollideRight(GameObject, GameObject2))
                {
                    GameObject.CollidedLevelObjects.Add(GameObject2);
                    GameObject.CollisionsRight++;
                }
            }

            if (!GameObject.IsColliding())
            {
                return;
            }
        }
    }
}
