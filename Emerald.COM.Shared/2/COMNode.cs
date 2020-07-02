using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Emerald.COM2
{
    public class COMNode2
    {
        public List<COMAttribute2> Attributes { get; set; } // The attributes for this node.
        public short NodeID { get; set; } // The ID of the attribute.
        public string NodeInnerText { get; set; } // The inner text of the node
        public string NodeName { get; set; } // The name attribute.
        public short NodeParentID { get; set; } // the parent ID of the attribute#
        public XmlNode NodeXml { get; set; } // the XML node of the xnode
        public COMNode2()
        {
            Attributes = new List<COMAttribute2>(); 
        }
    }
}
