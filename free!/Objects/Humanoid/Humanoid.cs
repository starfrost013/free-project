using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class Humanoid : PhysicalObject
    {
        public HumanoidState State { get; set; }

        public Humanoid()
        {

            State = new HumanoidState();
        }
    }
}
