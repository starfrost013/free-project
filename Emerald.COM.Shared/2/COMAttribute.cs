using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    public class COMAttribute2
    {
        public short LocalId { get; set; } // The local ID. This is part of the node that it is assigned to - this structure is held in the node itself.
        public short Name { get; set; } // The name of the attribute. 
        public short Content { get; set; } // The value of the XML attribute.
    }
}
