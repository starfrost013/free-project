using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Free
{
    // temporary?
    public partial class FreeSDL
    {
        // This is what the physicsworker does.
        public void PhysicsDoWork(object sender, DoWorkEventArgs e)
        {
            List<IGameObject> IGameObjectList = (List<IGameObject>)e.Argument;

            foreach (IGameObject GameObject in IGameObjectList)
            {
                HandlePhys(GameObject); 
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
