using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    public partial class FreeSDL
    {
        /// <summary>
        /// Add an object. WILL BE MOVED TO LEVEL!!!!!!!!!!!! MAKE FreeSDL.GAMEOBJECT STATIC
        /// </summary>
        /// <param name="ID">ID of the object to insert</param>
        /// <param name="Position">Point (soon to be SDLPoint) for position. </param>
        public void AddObject(int ID, Point Position)
        {
            // Insert an object. Some may consider this bad. To that I say - too bad!
            // This will be improved once SDLX works

            foreach (IGameObject GameObject in ObjectList)
            {
                if (GameObject.OBJID == ID) 
                {
                    GameObject.OBJX = Position.X;
                    GameObject.OBJY = Position.Y;
                    currentlevel.LevelObjects.Add(GameObject); 
                }
            }
        }
    }
}
