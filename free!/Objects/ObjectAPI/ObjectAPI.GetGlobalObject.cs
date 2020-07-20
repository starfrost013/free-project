using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Essentially just temporary code anyway
/// </summary>

namespace Free
{
    public partial class FreeSDL
    {
        public IGameObject GetGlobalObject(int GlobalObjectID)
        {
            if (GlobalObjectID > IGameObjectList.Count || GlobalObjectID < 0)
            {
                Error.Throw(null, ErrorSeverity.FatalError, "Attempted to acquire invalid global object!", "Fatal Error E100", 0);
                return null; 
            }

            return IGameObjectList[GlobalObjectID];

        }
    }
}
