using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class MainWindow
    {
        public void AI_Fear(IObject obj, IObject obj2)
        {
            if (obj.OBJX - obj2.OBJX < this.Width / 1.5 & obj.OBJX - obj.OBJX > -this.Width / 1.5 & obj.OBJY - obj2.OBJY < this.Height / 2 & obj.OBJY - obj2.OBJY > -this.Height / 2)
            {
                if (obj2.OBJSPEED > 0.1 & obj2.OBJMOVERIGHT | obj2.OBJSPEED < -0.1 & obj2.OBJMOVELEFT)
                {
                    //TEMP
                    obj.MoveRight(); //TEMPCODE. 
                }
            }
        }
    }
}
