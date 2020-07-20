using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Emerald.COM2.Writer
{
    public partial class COMWriter2
    {
        public List<Supernybble> SupernybbleFromString(string Input) // returns a bit array
        {
            // Supernybbles only support alphanumeric characters...

            try
            {
                List<Supernybble> LBA = new List<Supernybble>();

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
                            else if (StrByte > 0x40 && StrByte < 0x5B) // if it's alpha... (3a-3f not used)
                            {
                                // subtract 0x51
                                SubtractBy = 0x31; // squish into six bits
                            }
                            else
                            {
                                SubtractBy = 0x37;
                            }
                            // sanity check probably not required because we filtered out everything else
                            Supernybble SN = new Supernybble();
                            SN.NybbleData = SupernybbleEightToSixBits(Convert.ToByte(StrByte - SubtractBy));
                            LBA.Add(SN);
                        }
                    }
                }

                return LBA;
            }
            catch (OverflowException err)
            {
                MessageBox.Show($"An error has occurred attempting to compress using CompressML 2.5 and Supernybble 6-bit bitsmashing enabled.\n\n{err}", "Fatal Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }
        }

        public List<byte> SupernybbleToList(List<Supernybble> SN)
        {
            List<byte> Result = new List<byte>();

            foreach (Supernybble SNx in SN)
            {
                Result.Add(SupernybbleToByte(SNx.NybbleData));
            }

            return Result; 
        }

        public byte SupernybbleToByte(BitArray NybbleData)
        {
            byte[] _ = new byte[1]; 
            NybbleData.CopyTo(_, 0); 
            return _[0]; // reverses the endianness! Do check for this.
        }

        private BitArray SupernybbleEightToSixBits(byte Eight)
        {
            BitArray BA = new BitArray(new byte[] { Eight });
            BA.Length = 6; // remove bits #7 and #8 to 0 as they are not used
            return BA;
        }
    }
}
