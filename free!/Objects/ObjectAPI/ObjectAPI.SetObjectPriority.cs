using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    // TEMP in freesdl.
    public partial class FreeSDL
    {
        public GameObject SetGameObjectPriority(GameObject GameObjectToModify, Priority Priority)
        {
            foreach (GameObject GameObject in currentlevel.LevelIGameObjects)
            {
                if (GameObject == GameObjectToModify)
                {
                    GameObject.GameObjectPRIORITY = Priority;
                    return GameObjectToModify; //should return null.
                }
            }

            Error.Throw(new Exception("Failed to set IGameObject priority. You probably tried to change the priority of an IGameObject that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 23);
            return null;
        }
    }
}
