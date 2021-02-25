using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class Level
    {
        public void SetObjPhysicsState(int ObjID, PhysicsState PhysState)
        {
            if (ObjID > LevelIGameObjects.Count - 1)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"Fatal: Attempted to set invalid PhysicsState for invalid object with invalid global OBJID {ObjID} (max OBJID {LevelIGameObjects.Count - 1})", "Fatal E94", 94);
                return;
            }
            else
            {
                LevelIGameObjects[ObjID].PhysState = PhysState;
            }
        }
    }
}
