using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class MainWindow
    {
        public IObject Interaction_Kill(IObject obj)
        {
            obj.OBJPLAYERHEALTH = 0;
            return obj;
        }
    }
}
