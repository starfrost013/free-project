using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    public class COMAttribute2
    {
        public byte LocalId { get; set; } // The local ID. This is part of the node that it is assigned to - this structure is held in the node itself.
        public string Name { get; set; } // The name of the attribute. 
        public string Content { get; set; } // The value of the XML attribute.
    }
}
