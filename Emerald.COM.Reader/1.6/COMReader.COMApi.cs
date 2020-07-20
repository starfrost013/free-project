using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Emerald.COM.Reader
{
    public partial class COMReader
    {
        internal COMDatabase ComDatabase { get; set; }

        public void ComInit()
        {
            // initialise the COM database.
            ComDatabase = new COMDatabase();

        }

        public COMCatalog ComGetCatalogById(int id)
        {
            foreach (COMCatalog ComCat in ComDatabase.COMCatalogs)
            {
                // check the database id against the supplied one and return it if not, null if there is no catalog with the supplied if
                if (ComCat.CatalogID == id)
                {
                    return ComCat;
                }
            }
            return null;
        }

        public COMCatalog ComGetCatalogByFilename(string filename)
        {
            foreach (COMCatalog ComCat in ComDatabase.COMCatalogs)
            {
                // check the database id against the supplied one and return it if not, null if there is no catalog with the supplied if
                if (ComCat.CatalogComName == filename)
                {
                    return ComCat;
                }
            }
            return null;
        }

        public List<string> ComGetFileList(COMCatalog COMCat)
        {
            // create a list of strings
            List<string> ComFileList = new List<string>(); 

            //iterate through the entries within
            foreach (COMCatalogEntry COMCatEntry in COMCat.COMCatalogEntries)
            {
                ComFileList.Add(COMCatEntry.FileName);
            }

            return ComFileList;
        }
        
        public string ComLoadFile(COMCatalog COMCat, string FileName) //2020-03-07: Make public so other apps cna use it as a general-purpose ComX extraction api.
        {
            try
            {
                // Iterate through all of the catalogentries of COMCat
                foreach (COMCatalogEntry COMCatEntry in COMCat.COMCatalogEntries)
                {
                    // get the catalog entry we want to yank out
                    if (COMCatEntry.FileName == FileName)
                    {
                        // prepare to save to temp folder
                        string TempFolder = Path.GetTempPath();

                        using (BinaryReader BW = new BinaryReader(File.Open(COMCat.CatalogComName, FileMode.Open)))
                        {
                            // set the stream position to the position of the file
                            BW.BaseStream.Seek((long)COMCatEntry.FileLocation, SeekOrigin.Begin);

                            // read the file using its filesize
                            byte[] FileByteArray = BW.ReadBytes((int)COMCatEntry.FileSize);

                            // write the bytes to a temp file - if the ComX temp file store directory does not exist then create it

                            string _ = $@"{TempFolder}\Emerald COM1.6 Legacy";

                            if (!Directory.Exists(_))
                            {
                                Directory.CreateDirectory(_);
                            }

                            // random number for creating a unique file

                            Random Random = new Random();
                            int FileIdentifier = Random.Next(100000, 999999); // generate a random identifer between 100000 and 999999

                            // write the temporary file

                            File.WriteAllBytes($@"{_}\ComX.{FileIdentifier}.{COMCatEntry.FileName}", FileByteArray);

                            // lazy
                            _ = $@"{_}\ComX.{FileIdentifier}.{COMCatEntry.FileName}";

                            return _;
                        }
                    }
                }
                return null;
            }
            catch (IOException err)
            {
                MessageBox.Show($"Fatal Error: An IOException occurred while loading XML from a COM file.\nDetailed Information: {err}", "ComLoadXml Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public XmlDocument ComLoadXml(COMCatalog COMCat, string FileName)
        {
            try
            {
                // Get the Path from ComLoadFile
                string Path = ComLoadFile(COMCat, FileName);

                if (Path == null)
                {
                    MessageBox.Show("Critical Error 32: Error opening ComX (cannot load non-existent ComX!)\n\nPress OK to exit.", "Critical Error 32", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // create a new xmldocument
                XmlDocument XDocument = new XmlDocument();

                //load path
                XDocument.Load(Path);
                return XDocument; 
                
            }
            catch (IOException err)
            {
                MessageBox.Show($"Fatal Error: An IOException occurred while loading XML from a ComX file.\n\nDetailed Information: {err}", "ComLoadXml Fatal Error (Error 33)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (XmlException err)
            {
                MessageBox.Show($"Fatal Error: An XmlException occurred while loading XML from a ComX file.\n\nDetailed Information: {err}", "ComLoadXml Fatal Error (Error 34)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
