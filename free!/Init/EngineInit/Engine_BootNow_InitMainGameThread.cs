using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Free
{
    public partial class FreeSDL
    {
        public void BootNow_InitMainGameThread()
        {
            MainLoopTimer = new Timer();
            MainLoopTimer.Enabled = true;
            MainLoopTimer.Interval = 0.001;
            MainLoopTimer.Elapsed += MainLoop;

        }
    }
}
