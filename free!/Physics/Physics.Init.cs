using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Free
{
    public partial class MainWindow
    {
        public void InitPhysics()
        {
            PhysicsTimer = new Timer();
            PhysicsTimer.Interval = 0.001; // Run AFAP.
            PhysicsTimer.AutoReset = false;
            PhysicsTimer.Elapsed += PhysicsTimerElapsed;
            //PhysicsTimer.Start();
        }
    }
}
