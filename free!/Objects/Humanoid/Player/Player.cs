using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class Player : Humanoid
    {

        /// <summary>
        /// Is this player locally controlled?
        /// </summary>
        public bool IsLocalPlayer { get; set; }
        public MultiplayerClientInfo MPClientInfo { get; set; }

        public static new ObjectTypes OType = ObjectTypes.Player;
        public Player()
        {
            MPClientInfo = new MultiplayerClientInfo();
            State = new HumanoidState();
            PhysicsDefinition = new PhysicsDefinition();

        }

    }
}
