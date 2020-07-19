using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2.Writer
{
    public class Supernybble
    {
        public BitArray NybbleData { get; set; }

        public List<BitArray> GetSupernybble(string Input) // returns a bit array
        {
            // Supernybbles only support alphanumeric characters...

            List<BitArray> LBA = new List<BitArray>();

            foreach (byte StrByte in Input)
            {
                if (StrByte < 0x30 || StrByte > 0x7A)
                {
                    return null; // Must be alphanumeric
                }
                else
                {
                    if ((StrByte > 0x38 && StrByte < 0x41) || 
                        (StrByte > 0x5A && StrByte < 0x61)) // these are non-alphanumeric characters
                    {
                        return null;
                    }
                    else
                    {
                        byte SubtractBy = 0x00;
                        // If it's numerical...
                        if (StrByte < 0x39)
                        {
                            // subtract 0x30
                            SubtractBy = 0x30;
                        }
                        else // if it's alpha...
                        {
                            // subtract 0x51
                            SubtractBy = 0x51; // squish into six bits
                        }

                        // sanity check probably not required because we filtered out everything else
                        LBA.Add(EightToSixBits(Convert.ToByte(StrByte - SubtractBy)));
                    }
                }
            }
        }
        
        private BitArray EightToSixBits(byte Eight)
        {
            BitArray BA = new BitArray(new byte[] { Eight } );
            BA.Length = 6; // remove bits #7 and #8 to 0 as they are not used
            return BA;
        }
    }
}
