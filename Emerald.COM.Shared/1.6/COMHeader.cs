using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM
{
    /// <summary>
    /// Static class holding the ComX header.
    /// </summary>
    public static class COMHeader
    {
        public static string Header = "EmeraldCOM"; // Header
        public static byte MajorVersion = 1; // Major COM format version
        public static byte MinorVersion = 6; // Minor COM format version
        public static byte Padding = 0; // padding, may make it the COM format revision
        public static string Timestamp = DateTime.UtcNow.ToString(); // the timestamp that the COM file was last modified.
    }
}
