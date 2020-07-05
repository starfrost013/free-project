using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM
{
    /// <summary>
    /// * This is used by Emerald to access currently loaded COM files. After the file catalog is read the COMCatalog is put in here.
    /// * It then accesses the COM file at the location specified and extracts it.
    /// </summary>
    public class COMDatabase
    {
        public List<COMCatalog> COMCatalogs { get; set; }

        public COMDatabase()
        {
            COMCatalogs = new List<COMCatalog>();
        }

    }
}
