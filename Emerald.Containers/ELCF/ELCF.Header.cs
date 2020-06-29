using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Emerald.Containers/ELCF.Header.cs
/// 
/// Created: 2020-06-28
/// 
/// Modified: 2020-06-28
/// 
/// Version: 1.00
/// 
/// Purpose: The ELCF header format. ELCF is a level container file designed to hold ComX archives for XML based levels and ZIP archives for level data, although it can hold any file.
/// </summary>

namespace Emerald.Containers
{
    /// <summary>
    /// The Emerald Level Container Format header.
    /// </summary>
    public class ELCFHeader
    {
        public static string MagicString = "ELCF"; // Magic string to signify ELCF file.

        public static byte VersionNumberMajor = 1; // Major version number of the ELCF format.

        public static byte VersionNumberMinor = 0; // Minor version number of the ELCF format.

        public string CRC32 { get; set; } // CRC32 of the file for checking file integrity.

        public static string TimeStamp = DateTime.Now.ToString(); // The last modified time of the ELCF file.

    }
}
