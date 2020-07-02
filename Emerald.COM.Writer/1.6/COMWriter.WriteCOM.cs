using Emerald.COM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emerald.COM.Writer
{
    public partial class COMWriter
    {
        /// <summary>
        /// Writes a COM file.
        /// </summary>
        /// <param name="COMLocation">The location of the COM file to write.</param>
        /// <param name="SourceDirectory">The source directory.</param>
        /// <returns></returns>
        internal bool WriteCOMFromDirectory(string COMLocation, string SourceDirectory = null)
        {
            //temp code
            COMCatalog Catalog = BuildCOMCatalogFromDirectory($@"{SourceDirectory}");

            if (Catalog == null) 
            {
                // catalog error. show a message box, and delete the file.
                MessageBox.Show("An error occurred while creating the file catalog.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // delete the file as it may be corrupted
                File.Delete(COMLocation);
                return false;
            }
            
            // write the COM file.
            return WriteCOM(COMLocation, Catalog);
        }

        internal bool WriteCOMFromGroupOfFiles(string COMLocation, string[] SourceFiles)
        {
            COMCatalog Catalog = BuildCOMCatalogFromGroupOfFiles(SourceFiles);

            if (Catalog == null)
            {
                // catalog error. show a message box, and delete the file.
                MessageBox.Show("An error occurred while creating the file catalog.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // delete the file as it may be corrupted
                File.Delete(COMLocation);
                return false;
            }

            // write the COM file.
            return WriteCOM(COMLocation, Catalog);
        }

        internal bool WriteCOM(string COMLocation, COMCatalog Catalog)
        {
            // write the header using WriteCOMHeader
            bool HeaderResult = WriteCOMHeader(COMLocation);

            // did something fail? if so, return false

            if (HeaderResult == false)
            {
                // catalog error. show a message box, and delete the file.
                MessageBox.Show("An error occurred while writing the COM header.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // delete the file as it may be corrupted
                File.Delete(COMLocation);
                return false;
            }

            // write the files themselves to the file.
            COMCatalog FileResult = WriteCOMXML(COMLocation, Catalog);

            // again, if failed
            if (FileResult == null)
            {
                // catalog error. show a message box, and delete the file.
                MessageBox.Show("An error occurred while writing a file to the COM file.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // delete the file as it certainly is corrupted
                File.Delete(COMLocation);
                return false;
            }

            // write the catalog to the file.
            bool CatalogResult = WriteCOMCatalog(COMLocation, FileResult);

            // again, if failed
            if (CatalogResult == false)
            {
                // catalog error. show a message box, and delete the file.
                MessageBox.Show("An error occurred while writing the file catalog.", "Fatal Error Writing Compressed Object Metadata File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // delete the file as it certainly is corrupted
                File.Delete(COMLocation);
                return false;
            }


            return true;
        }

    }
}
