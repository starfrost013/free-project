using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM.Writer
{
    public partial class COMWriter
    {
        /// <summary>
        /// Calculates a CRC32. 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        private uint GetCrc32(List<byte> Source)
        {
            // The final CRC
            uint CRC0 = 0xFFFFFFFF; // because it cannot be negative so unsigned!

            foreach (byte SourceByte in Source)
            {
                byte sb = SourceByte;

                for (int i = 0; i < 8; i++)
                {
                    uint b = (sb ^ CRC0) & 1; // do math
                    CRC0 >>= 1; // shift right by 1 bit.

                    if (b != 0) CRC0 = CRC0 ^ 0xEDB88320; // Do some more math
                    
                    sb >>= 1; // MATH!!!

                    continue; // we did it. We generated a crc32. guys. 
                }
            }

            return CRC0; // This might be a bad idea. 
        }
    }
}
