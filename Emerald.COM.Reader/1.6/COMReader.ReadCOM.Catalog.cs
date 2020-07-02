using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM.Reader
{
    public partial class COMReader
    {
        internal COMCatalog ComReadFileCatalog(string COMPath)
        {

            COMCatalog COMCat = new COMCatalog();

            // set the full path to the COM file - this is used internally for the COM APIs
            COMCat.CatalogComName = COMPath;

            try
            {
                using (BinaryReader BW = new BinaryReader(new FileStream(COMPath, FileMode.OpenOrCreate)))
                {
                    // Since the catalog is always 4,096 bytes long, create an int here.

                    int RemainingBytes = 4096;

                    // Jump to byte #33, the end of the header and the beginning of the file catalog.
                    BW.BaseStream.Seek(33, SeekOrigin.Begin);

                    // read the catalog begin marker
                    BW.ReadByte(); //"C". no use.

                    // since we read 1 byte (char), decrement remaining bytes by 1.
                    RemainingBytes -= 1;

                    // instantiate the list of catalog entries
                    COMCat.COMCatalogEntries = new List<COMCatalogEntry>();

                    while (RemainingBytes > 0)
                    {
                        // Read the catalog begin marker ("CB")
                        string CatalogBegin = BW.ReadString();

                        if (CatalogBegin == "FCE") // this is probably bad code but read the rest of the header.
                        {
                            // the end of the catalog
                            while (RemainingBytes > 0)
                            {
                                byte _ = BW.ReadByte();
                                RemainingBytes -= 1;
                            }
                            break;
                        }

                        RemainingBytes -= 3;

                        COMCatalogEntry COMCatEntry = new COMCatalogEntry();

                        // read the file name. 
                        COMCatEntry.FileName = BW.ReadString();

                        // decrement the remaining bytes to read by the length of the string plus 1 for the binarywriter market
                        RemainingBytes -= COMCatEntry.FileName.Length + 1;

                        // read the file size (a uint32, so 4 bytes)
                        COMCatEntry.FileSize = BW.ReadUInt32();

                        // decrement by 4 bytes.
                        RemainingBytes -= 4;

                        // read the file location, or a uint32, so decrement by 4 bytes
                        COMCatEntry.FileLocation = BW.ReadUInt32();

                        RemainingBytes -= 4;

                        // set the full path
                        COMCatEntry.FileNameFull = COMPath;

                        // add to the list
                        COMCat.COMCatalogEntries.Add(COMCatEntry);

                        // read the catalog end string
                        string CatalogEnd = BW.ReadString();
                    }
                }
            }
            catch (EndOfStreamException)
            {
                // somehow, we hit the end of the file - this should not happen
                return null; 
            }

            // add the COM to the Global COM Database

            COMCat.CatalogID = ComDatabase.COMCatalogs.Count;
            ComDatabase.COMCatalogs.Add(COMCat);

            return COMCat;
        }
    }
}
