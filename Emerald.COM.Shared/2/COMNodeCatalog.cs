using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    /// <summary>
    /// Contains the information for a version 2 COM file's node catalog.
    /// 
    /// This is a single catalog that holds all of the information for the nodes located within a CompressML file. 
    /// </summary>
    public class COMNodeCatalog2
    {
        public int Endpoint { get; set; } // The endpoint of the node catalog.
        public static string NodeCatalogBegin = "NC"; // The beginning of the node catalog
        public List<COMNode2> Nodes { get; set; } // The XML nodes inside the node catalog.
        public static string NodeCatalogEnd = "NE"; // The end of the node catalog
        public COMNodeCatalog2()
        {
            Nodes = new List<COMNode2>(); 
        }
    }
}
