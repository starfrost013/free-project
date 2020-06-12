using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    // temporary?
    public partial class MainWindow
    {
        // This is what the physicsworker does.
        public void PhysicsDoWork(object sender, DoWorkEventArgs e)
        {
            List<IGameObject> ObjectList = (List<IGameObject>)e.Argument;

            foreach (IGameObject Obj in ObjectList)
            {
                HandlePhys(Obj); 
            }
        }

        public void PhysicsDone(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                // An error occurred on the physics thread.
                return;
            }

            return; 
        }
        
    }
}
