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
            MainWindow MnWindow = (MainWindow)Application.Current.MainWindow;

            switch (EvtClass.EventClass)
            {
                case EventClass.EveryFrame:
                    return true; // EveryFrame has no parameters.
                case EventClass.OnPlayerReachCertainPosition:
                    
                    return true; 
            }

            return false; 
        } 

        
    }
}
