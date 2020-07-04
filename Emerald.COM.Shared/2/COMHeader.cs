using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    /// <summary>
    /// Static class holding the ComX header.
    /// </summary>
    public static class COMHeader2
    {
        public static string Header = "COM2"; // Header
        public static byte MajorVersion = 2; // Major COM format version
        public static byte MinorVersion = 5; // Minor COM format version
        public static byte CatalogSize = 36; // Size of the catalog - 36*n bytes, where n is the total number of files
        public static string Timestamp = DateTime.UtcNow.ToString(); // the timestamp that the COM file was last modified.
        public static uint CRC32; // File integrity checking
    }
}
