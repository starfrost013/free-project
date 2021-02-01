using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class Humanoid : PhysicsObject
    {
        /// <summary>
        /// ObjectType of this object
        /// </summary>
        public static new ObjectTypes OType = ObjectTypes.Humanoid;

        public HumanoidState State { get; set; }

        public Humanoid()
        {

            State = new HumanoidState();
            PhysicsDefinition = new PhysicsDefinition();

        }
    }
}
