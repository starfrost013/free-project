using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    public interface ICompressionFormat
    {
        string Name { get; set; }

        bool Compress(string COMFileName, COMCatalog2 COMCat2, COMNodeCatalog2 COMNodeCat);

        void Decompress(string COMFileName, COMCatalog2 COMCat2, COMNodeCatalog2 NodeCatalog2);
    }
}
