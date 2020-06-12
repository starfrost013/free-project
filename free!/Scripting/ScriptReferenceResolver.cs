using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{

    /// <summary>
    /// Interface to implement script reference resolving for classes so we can verify for event classes. Potentially make an interface later.
    /// 
    /// Call this which checks this for playersatcertainpositio when the player first reaches this certain position and then mark the script as run. 
    /// </summary>
    public static class ScriptReferenceResolver
    {
        public static bool Resolve(EventClass EvtClass, List<ScriptReferenceRunOnParameter> Params)
        {
            switch (EvtClass)
            {
                case EventClass.EveryFrame:
                    return true; // EveryFrame has no parameters.
            }

            return false; 
        } 

        
    }
}
