using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class MainWindow // move to obj in the obj/player split?
    {
        public void Interaction_BounceLeft(IObject obj)
        {
            obj.OBJACCELERATION = -28.5;
            obj.OBJSPEED = -Physics.MaxSpeed;
            return;
        }
    }
}
