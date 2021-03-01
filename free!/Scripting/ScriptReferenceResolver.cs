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
        public static bool Resolve(ScriptReferenceRunOn EvtClass)
        {
            // WPF only
            FreeSDL MnWindow = SDL_Stage0_Init.SDLEngine;

            switch (EvtClass.EventClass)
            {
                case EventClass.EveryFrame:
                    return true; // EveryFrame has no parameters.
                case EventClass.OnPlayerReachCertainPosition:
                    List<IGameObject> Players = MnWindow.currentlevel.GetPlayers();

                    if (EvtClass.ReferenceRunOn.Count > 1)
                    {
                        ScriptError.Throw("Error: Only one RunOn parameter is allowed for OnPlayerReachCertainPosition event-handler scripts", 9, 0, "ScriptXML Bug"); 
                    }
                    else
                    {

                        // TEMP (multiplayer later)
                        if (Players[0].Position.X > (double)EvtClass.ReferenceRunOn[0].Value[0] && Players[0].Position.Y > (double)EvtClass.ReferenceRunOn[0].Value[1])
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    return false;

                case EventClass.OnCollide:

                    ScriptReferenceRunOnParameter SRROPID1 = GetRunOnParamWithName(EvtClass, "Obj1ID");
                    ScriptReferenceRunOnParameter SRROPID2 = GetRunOnParamWithName(EvtClass, "Obj2ID");

                    if (SRROPID1 != null && SRROPID2 != null)
                    {
                        // Get the object to check
                        IGameObject SRROPIDC1 = MnWindow.currentlevel.GetObjectWithLevelObjid((int)SRROPID1.Value[0]);
                        IGameObject SRROPIDC2 = MnWindow.currentlevel.GetObjectWithLevelObjid((int)SRROPID1.Value[1]);

                        // If colliding return true

                        if (SRROPIDC1.IsCollidingWith(SRROPIDC2)) return true;
                    }
                    else
                    {
                        ScriptError.Throw("Obj1ID or OBJ2ID condition not defined!", 14, 0, "ReflectionMetadata runtime parse error");
                    }

                    return false;
            }

            return false; 
        } 

        private static ScriptReferenceRunOnParameter GetRunOnParamWithName(ScriptReferenceRunOn EvtClass, string Name)
        {
            foreach (ScriptReferenceRunOnParameter SRRO in EvtClass.ReferenceRunOn)
            {
                if (SRRO.Name == Name) return SRRO;
            }

            return null;
        }
    }
}
