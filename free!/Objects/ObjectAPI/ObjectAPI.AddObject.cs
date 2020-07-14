using Emerald.Utilities.Wpf2Sdl;
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
        /// Add an IGameObject. WILL BE MOVED TO LEVEL!!!!!!!!!!!! MAKE FreeSDL.GAMEIGameObject STATIC
        /// </summary>
        /// <param name="ID">ID of the IGameObject to insert</param>
        /// <param name="Position">Point (soon to be SDLPoint) for position. </param>
        public void AddIGameObject(int ID, SDLPoint Position)
        {
            // Insert an IGameObject. Some may consider this bad. To that I say - too bad!
            // This will be improved once SDLX works

            foreach (IGameObject GameIGameObject in IGameObjectList)
            {
                if (GameIGameObject.GameObjectID == ID) 
                {
                    GameIGameObject.Position.X = Position.X;
                    GameIGameObject.Position.Y = Position.Y;
                    currentlevel.LevelIGameObjects.Add(GameIGameObject); 
                }
            }
        }
    }
}
