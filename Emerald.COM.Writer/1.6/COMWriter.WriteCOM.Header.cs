using Emerald.COM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM.Writer
{
    public partial class COMWriter
    {
        /// <summary>
        /// Writes the header to a COM file.
        /// </summary>
        /// <param name="FileToUse">The file to write to.</param>
        /// <returns></returns>
        internal bool WriteCOMHeader(string FileToUse)
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
                    // seek to the first byte in the file. this is just in case we're doing something with an already extant file.
                    BW.Seek(0, SeekOrigin.Begin);

                    // write the header defined in COMShared.COMHeader ("EmeraldCOM")
                    BW.Write(COMHeader.Header);

                    // write the major version defined in COMShared.COMHeader (1 currently)
                    BW.Write(COMHeader.MajorVersion);

                    // write the minor version defined in COMShared.COMHeader (0 currently)
                    BW.Write(COMHeader.MinorVersion);

                    // write the timestamp defined in COMShared.COMHeader (the current time in UTC in a string)
                    BW.Write(COMHeader.Timestamp);

                    for (int i = 0; i <= 4096; i++)
                    {
                        BW.Write(0x00);
                    }

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
