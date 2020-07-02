using Emerald.COM;
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
        /// <summary>
        /// Builds a COM catalog from a directory.
        /// </summary>
        /// <param name="SourceDirectoryPath">The source directory.</param>
        /// <returns></returns>
        internal COMCatalog BuildCOMCatalogFromDirectory(string SourceDirectoryPath)
        {
            // does the directory exist?

            if (!Directory.Exists(SourceDirectoryPath)) return null;

            // get a list of all of the file names.
            string[] Files = Directory.GetFiles(SourceDirectoryPath);

            // build the catalog
            COMCatalog BuildResult = BuildCOMCatalog(Files);
            return BuildResult;
        }

        internal COMCatalog BuildCOMCatalogFromGroupOfFiles(string[] SourceFiles)
        {
            // build the catalog from the string array passed
            COMCatalog BuildResult = BuildCOMCatalog(SourceFiles);
            return BuildResult;
        }

        /// <summary>
        /// Builds a COM catalog from source files.
        /// </summary>
        /// <param name="SourceFiles">The string array of filenames to use for building.</param>
        /// <returns></returns>
        internal COMCatalog BuildCOMCatalog(string[] SourceFiles)
        {
            try
            {
                // Create the COM catalog.
                COMCatalog COMCat = new COMCatalog();

                // Create new list of entries.
                COMCat.COMCatalogEntries = new List<COMCatalogEntry>();

                // If there are no files to build a catalog out of, show a message fail
                if (SourceFiles.Length == 0)
                {
                    MessageBox.Show("There are no files to build a catalog with.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Iterate through all of the source files.
                foreach (string SourceFile in SourceFiles)
                {
                    // Preprocessing: Get rid of the path.
                    string[] SourceFileSplit = SourceFile.Split('\\');

                    // Get the last section of the path, split by \\ (the file itself)
                    string SourceFileNoPath = SourceFileSplit[SourceFileSplit.Length - 1];

                    // Create a COM catalog entry.
                    COMCatalogEntry SourceFileEntry = new COMCatalogEntry();

                    // Create the file name.
                    SourceFileEntry.FileName = SourceFileNoPath;

                    // Create the full fule name (used later in the process).
                    SourceFileEntry.FileNameFull = SourceFile;

                    // Compute the file size using FileInfo.
                    FileInfo SourceFileInfo = new FileInfo($@"{SourceFile}");
                    SourceFileEntry.FileSize = (uint)SourceFileInfo.Length; // +10 for efile string?

                    /* 32-byte header in decimal, plus 4,096 bytes for catalog = 4,112 bytes. Catalog size 4kb = ~128 files at 32-bytes/catalog entry, which SHOULD be enough for what COM files are used for.
                     * In truth, each catalog entry is almost certainly less than 32 bytes unless you have a longer filename.
                     * Max files:
                     * 512 at 8-bytes per entry.
                     * 256 at 16-bytes per entry.
                     * 204 at 20-bytes per entry.
                     * 170 at 24-bytes per entry.
                     * 128 at 32-bytes per entry.
                     * Considering the fact that the minimum size for a catalog entry is 17 bytes/240 files and it could easily be reduced to 11 with few sacrifices, we're fine for now.
                     * Also, subdirectory support could be added later if needed.
                    */

                    SourceFileEntry.FileLocation = 4129;

                    // Compute the file location.
                    foreach (COMCatalogEntry OtherEntry in COMCat.COMCatalogEntries)
                    {
                        // Add the file sizes together. 
                        SourceFileEntry.FileLocation += OtherEntry.FileSize;
                    }

                    // Add the catalog entry to the catalog.
                    COMCat.COMCatalogEntries.Add(SourceFileEntry);
                }

                // return the catalog
                return COMCat; // temp
            }
            catch (OverflowException)
            {
                // we weren't successful, so return null
                return null; // error code 1: max 4gb.
            }
        }


    }
}
