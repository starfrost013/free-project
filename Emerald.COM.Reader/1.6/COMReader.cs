using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emerald.COM.Reader
{
    public partial class COMReader
    {
        /// <summary>
        /// TODO: RENAME TO ComLoadComX [2020-04-07]
        /// </summary>
        /// <param name="COMPath"></param>
        /// <returns></returns>
        public COMCatalog ComReadFile(string COMPath)
        {
            // COM read utilities.
            ComReadHeader(COMPath);
            COMCatalog COMCatalog = ComReadFileCatalog(COMPath);
            return COMCatalog; 
        }
    }
}
