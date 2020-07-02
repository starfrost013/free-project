using Emerald.COM2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2.Writer
{
    public partial class COMWriter2
    {
        /// <summary>
        /// Writes the header to a COM file.
        /// </summary>
        /// <param name="FileToUse">The file to write to.</param>
        /// <returns></returns>
        internal bool WriteCOMHeader2(string FileToUse)
        {
            try
            {
                // prevent corruption of the file if it already exists. 
                if (File.Exists(FileToUse))
                {
                    File.Delete(FileToUse);
                }

                // open the file for writing using a filestream inside a binarywriter. We create the file if it doesn't exist using FileMode.OpenOrCreate.
                using (BinaryWriter BW = new BinaryWriter(new FileStream(FileToUse, FileMode.OpenOrCreate)))
                {
                    // Seek to the first byte in the file. this is just in case we're doing something with an already extant file.
                    BW.Seek(0, SeekOrigin.Begin);

                    // Write the header defined in COMShared.COMHeader ("COM2")
                    BW.Write(COMHeader2.Header);

                    // Write the major version defined in COMShared.COMHeader (1 currently)
                    BW.Write(COMHeader2.MajorVersion);

                    // Write the minor version defined in COMShared.COMHeader (0 currently)
                    BW.Write(COMHeader2.MinorVersion);

                    // Write the timestamp defined in COMShared.COMHeader (the current time in UTC in a string)
                    // Also, the catalog writing routine fills in the header, so we write an empty short as a "slot" for it.
                    BW.Write(0x0000);

                    BW.Write(COMHeader2.Timestamp);

                    /// * We used to write placeholder bytes for the header here,
                    /// * but we don't do this anymore as COM2 has a non-fixed header size
                    
                    BW.Write(0x00);

                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
