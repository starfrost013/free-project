using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// File name: ObjManager.cs
/// 
/// Created: 2019-11-18
/// 
/// Modified: 2019-12-21
/// 
/// Version: 1.00
/// 
/// Purpose: Provides methods for managing and deleting objects. Required for the Interaction Manager.
/// 
/// </summary>
namespace Free
{
    partial class MainWindow //move to its own class maybe?
    {
        public void DeleteObj(IGameObject objToDel, int int2id) // Deletes an object from the level layout.
        {
            foreach (IGameObject Obj in currentlevel.OBJLAYOUT)
            {
                if (Obj == objToDel)
                {
                    if (Obj.OBJID == int2id)
                    {
                        currentlevel.OBJLAYOUT.Remove(Obj);
                        return;
                    }
                }
            }
            Error.Throw(new Exception("Failed to delete object. You probably tried to delete an object that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 3);
            return;
        }

        //do we need this?
        public Obj SetObjPos(Obj objToMove, double x, double y) // Sets an object's position to a certain value.
        {
            foreach (Obj Obj in currentlevel.OBJLAYOUT)
            {
                if (Obj == objToMove)
                {
                    Obj.OBJX = x;
                    Obj.OBJY = y;
                    return objToMove;
                }
            }
            Error.Throw(new Exception("Failed to move object. You probably tried to move an object that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 22);
            return null;
        }

        public Obj SetObjPriority(Obj objToModify, Priority priority)
        {
            foreach (Obj Obj in currentlevel.OBJLAYOUT)
            {
                if (Obj == objToModify)
                {
                    Obj.OBJPRIORITY = priority;
                    return objToModify; //should return null.
                }
            }

            Error.Throw(new Exception("Failed to set object priority. You probably tried to change the priority of an object that doesn't exist."), ErrorSeverity.FatalError, "", "avant-gardé engine ver 2.7.0/01", 23);
            return null;
        }
    }
}
