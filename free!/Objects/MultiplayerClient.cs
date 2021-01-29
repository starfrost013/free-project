using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class MultiplayerClientInfo
    {
        /// <summary>
        /// MP: Player name
        /// </summary>
        public string UName { get; set; }

        /// <summary>
        /// MP: User Guid
        /// </summary>
        public Guid UGuid { get; set; }

        /// <summary>
        /// MP: User ping
        /// </summary>
        public int UPing { get; set; }
    }
}
