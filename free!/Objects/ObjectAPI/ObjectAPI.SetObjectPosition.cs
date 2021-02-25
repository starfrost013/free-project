using Emerald.Core;
using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class Level
    {
        // TEMP
        public bool SetGameObjectPos(int GameObjectId, double x, double y) // Sets an IGameObject's position to a certain value.
        {
            foreach (GameObject GameObject in LevelIGameObjects)
            {
                if (GameObject.GameObjectINTERNALID == GameObjectId)
                {
                    GameObject.Position = new SDLPoint(x, y); 
                    return true;
                }
            }

            Error.Throw(new Exception("Failed to move IGameObject. You probably tried to move an IGameObject that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 22);
            return false; 
        }
    }
}
