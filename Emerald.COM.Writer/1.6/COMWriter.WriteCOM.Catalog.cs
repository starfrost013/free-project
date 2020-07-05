using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emerald.COM.Writer
{
    public partial class COMWriter
    {
        internal bool WriteCOMCatalog(string FileToUse, COMCatalog COMCat)
        {
            /* You may notice that in this method a 3-byte string causes RemainingBytes to be decremented by 4, a 2-byte string by 3, etc.
             * This is because BinaryWriter.Write automatically prefixes a length to any written string.
             * Due to this, this method writes more bytes than it should, so the remainingbyte deductions have to be increased by 1 to account for this. */
            try
            {
                // amount of bytes left. this is so we can have padding. 
                int RemainingBytes = 4096;

                // open the file for writing using a filestream inside a binarywriter. We create the file if it doesn't exist using FileMode.OpenOrCreate.
                using (BinaryWriter BW = new BinaryWriter(new FileStream(FileToUse, FileMode.OpenOrCreate)))
                {
                    // Set ourselves to the end of the header (32 bytes decimal)
                    BW.Seek(33, SeekOrigin.Begin);

                    // write the catalog begin ("C")
                    BW.Write(COMCatalog.CatalogBegin);

                    RemainingBytes -= 2;

                    // loop through every entry in the COM catalog

                    foreach (COMCatalogEntry COMCatEntry in COMCat.COMCatalogEntries)
                    {
                        if (RemainingBytes < 17)
                        {
                            //remove. 
                            MessageBox.Show("Error: Ran out of space alloted for file catalog. Try removing some files.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        // write the bytes marking the beginning of a catalog entry.
                        BW.Write(COMCatalogEntry.CatalogEntryBegin);

                        // because it's a 2 byte string, plus the prefixed length, decrement remaining bytes by 3.
                        RemainingBytes -= 3;

                        // write the file name to the COM file
                        BW.Write(COMCatEntry.FileName);

                        // decrement the length of the filename  plus the prefixed length from the remaining bytes, or.
                        RemainingBytes -= COMCatEntry.FileName.Length + 1;

                        // write the file size to the COM file
                        BW.Write(COMCatEntry.FileSize);

                        // because ints/uints are 4 bytes, decrement remaining bytes by 4.
                        RemainingBytes -= 4;

                        // write the file location to the COM file.
                        BW.Write(COMCatEntry.FileLocation);

                        // because longs/ulongs are 8 bytes, decrement remaining bytes by 8.
                        RemainingBytes -= 8;

                        // Write the bytes marking the end of a catalog entry.
                        BW.Write(COMCatalogEntry.CatalogEntryEnd);

                        // Because the bytes that mark the end of a catalog entry ("CE") are 2 bytes, plus the prefixed length, decrement remaining bytes by 3.
                        RemainingBytes -= 3;

                    }

                    // Write the end of file catalog marker ("FCE"). If it turns out to be an issue we can add a 
                    BW.Write(COMCatalog.CatalogEnd);

                    // As the end of file catalog marker ("FCE") is a 3-byte string, plus the prefixed length, decrement remaining bytes by 4. 
                    RemainingBytes -= 4;

                    /* obsolete as this is done in a different order now
                    while (RemainingBytes > 0)
                    {
                        // write the padding, 0x00 until remainingbytes = 0
                        BW.Write(Byte.MinValue);

                        // decrement the amonut of remaining bytes until the full 4kb is written.
                        RemainingBytes -= 1;
                    }*/ 

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
