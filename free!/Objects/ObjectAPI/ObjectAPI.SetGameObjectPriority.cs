using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free.Objects.ObjectAPI
{
    // TEMP in freesdl.
    public partial class FreeSDL
    {
        public GameObject SetGameObjectPriority(GameObject GameObjectToModify, Priority priority)
        {
            foreach (GameObject GameObject in currentlevel.LevelIGameObjects)
            {
                if (GameObject == GameObjectToModify)
                {
                    GameObject.GameObjectPRIORITY = priority;
                    return GameObjectToModify; //should return null.
                }
            }

            Error.Throw(new Exception("Failed to set IGameObject priority. You probably tried to change the priority of an IGameObject that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 23);
            return null;
        }
    }
}
