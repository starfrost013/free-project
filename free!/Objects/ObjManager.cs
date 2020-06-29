using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// File name: GameObjectManager.cs
/// 
/// Created: 2019-11-18
/// 
/// Modified: 2019-12-21
/// 
/// Version: 1.00
/// 
/// Purpose: Provides methods for managing and deleting IGameObjects. Required for the Interaction Manager.
/// 
/// </summary>
namespace Free
{
    partial class FreeSDL //move to its own class maybe?
    {
        public void DeleteGameObject(IGameObject GameObjectToDel, int int2id) // Deletes an IGameObject from the level layout.
        {
            foreach (IGameObject GameObject in currentlevel.LevelIGameObjects)
            {
                if (GameObject == GameObjectToDel)
                {
                    if (GameObject.GameObjectID == int2id)
                    {
                        currentlevel.LevelIGameObjects.Remove(GameObject);
                        return;
                    }
                }
            }
            Error.Throw(new Exception("Failed to delete IGameObject. You probably tried to delete an IGameObject that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 3);
            return;
        }

        //do we need this?
        public GameObject SetGameObjectPos(GameObject GameObjectToMove, double x, double y) // Sets an IGameObject's position to a certain value.
        {
            foreach (GameObject GameObject in currentlevel.LevelIGameObjects)
            {
                if (GameObject == GameObjectToMove)
                {
                    GameObject.GameObjectX = x;
                    GameObject.GameObjectY = y;
                    return GameObjectToMove;
                }
            }
            Error.Throw(new Exception("Failed to move IGameObject. You probably tried to move an IGameObject that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 22);
            return null;
        }

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
