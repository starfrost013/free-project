using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            FreeSDL MnWindow = (FreeSDL)Application.Current.MainWindow;

            switch (EvtClass.EventClass)
            {
                case EventClass.EveryFrame:
                    return true; // EveryFrame has no parameters.
                case EventClass.OnPlayerReachCertainPosition:
                    List<IGameObject> Players = MnWindow.currentlevel.GetPlayers();

                    if (EvtClass.ReferenceRunOn.Count > 1)
                    {
                        ScriptError.Throw("Error: Only one RunOn is allowed for OnPlayerReachCertainPosition event-handler scripts", 9, 0, "ScriptXML Bug"); 
                    }

                   
                    // TEMP (multiplayer later)
                    if (Players[0].OBJX > (double)EvtClass.ReferenceRunOn[0].Value[0] && Players[0].OBJY > (double)EvtClass.ReferenceRunOn[0].Value[1])
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return false; 
        } 

        
    }
}
