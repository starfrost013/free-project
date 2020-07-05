using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    public class COMAttributeCatalog2
    {
        public int Endpoint { get; set; }
        public static string AttributeCatalogStart = "AS";
        public List<COMAttribute2> Attribute { get; set; }

        public static string AttributeCatalogEnd = "AE";
    }
}
