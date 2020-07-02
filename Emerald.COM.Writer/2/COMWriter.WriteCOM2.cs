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

        public bool ReadCom2FromGroupOfFiles(string ComFilePath, List<string> Files)
        {
            COMCatalog2 Cat2 = ComBuildCatalog2(Files);
            
            // Write the com2.
            return WriteCOM2(ComFilePath, Cat2); 

        }

        /// <summary>
        /// * This is the COM2 writing API for 2.0 COM format files.
        /// * It writes, either from a group of files or a directory, out to a COM2 file. 
        /// </summary>
        /// <returns></returns>
        internal bool WriteCOM2(string ComFilePath, COMCatalog2 Cat2)
        {
            bool HeaderResult = WriteCOMHeader2(ComFilePath); 

            if (!HeaderResult)
            {
                // catalog error. show a message box, and delete the file.
                MessageBox.Show("An error occurred while creating the Com2 file header.", "Fatal Error Writing Compressed Object Metadata 2 File", MessageBoxButton.OK, MessageBoxImage.Error);

                // delete the file as it may be corrupted
                File.Delete(ComFilePath);
                return false;
            }


            bool CatalogResult = WriteCOMCatalog2(ComFilePath, Cat2);

            // An error has occurred writing the COM catalog.

            if (!CatalogResult)
            {
                MessageBox.Show("An error occurred while creating the Com2 file catalog.", "Fatal Error Writing Compressed Object Metadata 2 File", MessageBoxButton.OK, MessageBoxImage.Error);

                // delete the file as it may be corrupted
                File.Delete(ComFilePath);
                return false;
            }

            COMNodeCatalog2 NodeCatalogResult = WriteNodeCatalog2(ComFilePath, Cat2);

            if (NodeCatalogResult == null)
            {
                MessageBox.Show("An error occurred while creating the Com2 node catalog.", "Fatal Error Writing Compressed Object Metadata 2 File", MessageBoxButton.OK, MessageBoxImage.Error);

                // delete the file as it may be corrupted
                File.Delete(ComFilePath);
                return false;
            }

            CompressML CML = new CompressML();
            bool CompressionResult = CML.Compress(ComFilePath, Cat2, NodeCatalogResult);

            if (!CompressionResult)
            {
                MessageBox.Show("An error occurred while compressing a file to CompressML format.", "Fatal Error Writing Compressed Object Metadata 2 File", MessageBoxButton.OK, MessageBoxImage.Error);

                // delete the file as it may be corrupted
                File.Delete(ComFilePath);
                return false;

            }

            return true;
        }
    }
}
