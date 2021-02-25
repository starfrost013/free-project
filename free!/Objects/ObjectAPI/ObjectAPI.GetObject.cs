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
        public IGameObject GetObjectWithLevelObjid(int ObjId)
        {
            if (ObjId > LevelIGameObjects.Count)
            {
                Error.Throw(null, ErrorSeverity.FatalError, "Attempted to get invalid object global ID. This would've crashed.", "We're crashing here", 95);
            }

            return LevelIGameObjects[ObjId];
        }
    }
}
