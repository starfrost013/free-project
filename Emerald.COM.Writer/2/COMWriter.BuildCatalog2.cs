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
        /// <summary>
        /// Builds a COM2 catalog from a group of files.
        /// </summary>
        /// <param name="SourceFiles"></param>
        internal COMCatalog2 ComBuildCatalog2(List<string> SourceFiles)
        {
            try
            {
                if (SourceFiles.Count == 0)
                {
                    MessageBox.Show("Can't write Com2 file from no source files", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                COMCatalog2 Com2 = new COMCatalog2();

                foreach (string SourceFileName in SourceFiles)
                {
                    // get the filename
                    string[] NameArray = SourceFileName.Split('\\');
                    string FileName = NameArray[NameArray.Length - 1];

                    COMCatalogEntry2 FileEntry = new COMCatalogEntry2();

                    // The file name of the catalog entry.
                    FileEntry.FileName = FileName;

                    FileEntry.FileNameFull = SourceFileName;

                    // The file size

                    FileInfo _1 = new FileInfo(SourceFileName);
                    FileEntry.FileSize = (uint)_1.Length;

                    // Default compression mode
                    if (!FileEntry.FileName.Contains(".xml"))
                    {
                        FileEntry.CompressionType = 0x01; //RLE
                    }
                    else
                    {
                        FileEntry.CompressionType = 0x00; //CompressML 
                    }

                    // New V2 feature: dynamic-size catalogs - file now begins at (36 * (amount of files in catalog)) + 32 bytes instead of at a fixed 4128 bytes
                    FileEntry.FileLocation = Convert.ToUInt32(36 * SourceFiles.Count);

                    // add other files
                    foreach (COMCatalogEntry2 OtherEntry in Com2.COMCatalogEntries)
                    {
                        FileEntry.FileLocation += OtherEntry.FileLocation;
                    }

                    Com2.COMCatalogEntries.Add(FileEntry);
                }

                return Com2;
            }
            catch (FileNotFoundException err)
            {
                MessageBox.Show($"An error occurred building the COM2 catalog - Can't find a source file..\n\n{err}", "Emerald Game Engine Error 49", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }
            catch (IOException err)
            {
                MessageBox.Show($"An error occurred building the COM2 catalog.\n\n{err}", "Emerald Game Engine Error 50", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }
        }
    }
}
