using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Emerald.COM2.Writer
{
    public partial class COMWriter2
    {
        internal bool WriteCOMCatalog2(string FileToUse, COMCatalog2 COMCat)
        {

            try
            {
                int RemainingBytes = 36 * COMCat.COMCatalogEntries.Count;
                COMCat.Endpoint = RemainingBytes;

                if (RemainingBytes > 2359260) // we get a short from this - max 65,536 entries - could increase to 2147483647 actually, but i defined a short lol 
                {
                    // catalog size max 64k
                    MessageBox.Show("More than 65,535 files in a COM2 file catalog is not supported. A catalog size of more than 2359260 bytes (2.36 MB) is not supported.");
                    return false;
                }

                // Open the COM2 file and write to it
                using (BinaryWriter BW = new BinaryWriter(new FileStream(FileToUse, FileMode.Open)))
                {
                    // Write the header's catalog size
                    BW.BaseStream.Seek(7, SeekOrigin.Begin);

                    BW.Write((short)RemainingBytes);

                    // seek - go to the end of the header

                    BW.BaseStream.Seek(39, SeekOrigin.Begin);

                    BW.Write(COMCatalog2.CatalogBegin);
                    RemainingBytes -= 2; // decrement by (length of string) + 1
                    COMCat.Endpoint += 2; // Decrement by (length of string) + 1 - this is the point where the catalog ends so we can write the node catalog.

                    foreach (COMCatalogEntry2 COM2Entry in COMCat.COMCatalogEntries)
                    {
                        // Write the catalog entry begin symbol (do we need this?)
                        BW.Write(COMCatalogEntry2.CatalogEntryBegin);
                        RemainingBytes -= 3;
                        COMCat.Endpoint += 3;

                        // Write the name
                        BW.Write(COM2Entry.FileName);

                        RemainingBytes -= COM2Entry.FileName.Length;

                        // Increment endpoint and decrement remaining bytes
                        COMCat.Endpoint += COM2Entry.FileName.Length;

                        // Write the file size
                        BW.Write(COM2Entry.FileSize);

                        // Since ints / uints are 4 bytes, decrement remaining bytes by 4
                        RemainingBytes -= 4;
                        COMCat.Endpoint += 4;

                        // Write the file location
                        BW.Write(COM2Entry.FileLocation);

                        // Since ints / uints are 4 bytes, decrement remaining bytes by 4
                        RemainingBytes -= 4;
                        COMCat.Endpoint += 4;

                        // Write the cpmpression type. 
                        BW.Write(COM2Entry.CompressionType);

                        // The compression type is a single byte, so decrement remaining bytes by 1
                        RemainingBytes -= 1;
                        COMCat.Endpoint += 1;

                        // Write the catalog entry end symbol (do we need this?)
                        BW.Write(COMCatalogEntry2.CatalogEntryEnd);
                        RemainingBytes -= 3;
                        COMCat.Endpoint += 3; 

                    }

                    BW.Write(COMCatalog2.CatalogEnd);
                    RemainingBytes -= 2;
                    COMCat.Endpoint += 2; 


                    // fill out whatever is left

                    for (int i = 0; i <= RemainingBytes; i++)
                    {
                        // write 0x00 - hopefully less filler
                        BW.Write(0x00);
                    }

                    return true;
                }
            }
            catch (FileNotFoundException err) // how does this happen 
            {
                MessageBox.Show($"Something bad happened. FileNotFoundException!! - We may have failed to create the Com2 file.\n\n{err}", "Error 44", MessageBoxButton.OK, MessageBoxImage.Error); 
                return false; 
            }
            catch (DirectoryNotFoundException err) // how does this happen 
            {
                MessageBox.Show($"Something bad happened. DirectoryNotFoundException!! - What?\n\n{err}", "Error 45", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (IOException err) // An error occurred writing the COM2 catalog.
            {
                MessageBox.Show($"An error has occurred writing the COM2 catalog.\nA common reason for this error to occur is because MAX_PATH.\nMAX_PATH sucks and whoever came up with it should be fired and never allowed to touch an OS again.\n\n{err}", "Error 46", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
