﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM
{
    /// <summary>
    /// Holds COM file catalogs.
    /// </summary>
    public class COMCatalog
    {
        public static char CatalogBegin = 'C'; // catalog begin (always at 0x20)
        public string CatalogComName { get; set; } // the path to the catalog
        public List<COMCatalogEntry> COMCatalogEntries { get; set; } // list of entries
        public static string CatalogEnd = "FCE"; // end of catalog
        public int CatalogID { get; set; } // the ID of the catalog
        public static int CatalogLen = 4129; // the end of the catalog (decimal)
        public COMCatalog()
        {
            COMCatalogEntries = new List<COMCatalogEntry>();
        }
    }

    /// <summary>
    /// Holds COM file catalog entries.
    /// </summary>
    public class COMCatalogEntry
    {
        public static string CatalogEntryBegin = "CB";
        public string FileName { get; set; } // The name of the file.
        public string FileNameFull { get; set; } // The full path of the file. This is used later in the writing process and is not written to the file.
        public uint FileLocation { get; set; } // The location of the file in the COM file.

        /* The size of the file. Max 4GB filesize. We don't need anything too fancy. Also, going from long to uint decreases the minimum header size by 4 bytes, from 21 bytes to 17,
         * increasing the maximum amount of file entries here from 195 to 240. I could reduce it to 11 bytes, but I don't think more than a few dozen files at most will be needed in a COM file, but increasing to be safe.
         * Since this is 2D, max 4GB file size doesn't hurt - and it can be easily changed later anyway by changing one part of a single line.
        */
        public uint FileSize { get; set; } 
        public static string CatalogEntryEnd = "CE";
    }
}
