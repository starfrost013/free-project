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
        /// todo
        /// </summary>
        /// <param name="FileToUse"></param>
        /// <param name="FileCatalog"></param>
        /// <returns></returns>
        public COMCatalog WriteCOMXML(string FileToUse, COMCatalog FileCatalog)
        {
            try
            {
                // open the file for writing using a filestream inside a binarywriter. We create the file if it doesn't exist using FileMode.OpenOrCreate.
                using (BinaryWriter BW = new BinaryWriter(new FileStream(FileToUse, FileMode.OpenOrCreate)))
                {
                    // seek to 4,129th byte in the file. this is where the real data begins. 
                    BW.Seek(4129, SeekOrigin.Begin);

                    // iterate through all the files we have
                    foreach (COMCatalogEntry CatalogEntry in FileCatalog.COMCatalogEntries)
                    {
                        // read all bytes of each file
                        byte[] XmlArray = File.ReadAllBytes($@"{CatalogEntry.FileNameFull}");
                        //string CurrentFileName = CatalogEntry.FileName; // the current filename

                        //write them to the file
                        foreach (byte XmlByte in XmlArray)
                        {
                            // write the byte to the file

                            if ((XmlByte == 0xA || XmlByte == 0xD) && CatalogEntry.FileName.Contains(".xml"))
                            {
                                // Strip newlines for v1.5, CRs for 1.6

                                CatalogEntry.FileSize -= 1;

                                int i = 0; 

                                foreach (COMCatalogEntry CatalogEntry2 in FileCatalog.COMCatalogEntries)
                                {
                                    // to prevent corrupting the files that we extract, decrement the position by one except for the file we want - learned this the hard way.
                                    if (CatalogEntry2.FileLocation > CatalogEntry.FileLocation)
                                    {
                                        CatalogEntry2.FileLocation -= 1;
                                    }
                                    i++;
                                }

                                continue;
                            }
                            BW.Write(XmlByte);


                        }
                    }

                    return FileCatalog;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
