using Emerald.COM;
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
        public bool WriteCOMFromFolder(string Path, string DirectoryPath)
        {
            //write the COM file using WriteCOMFromFile
            WriteCOMFromDirectory(Path, $@"{DirectoryPath}");

            return true;
        }

        public bool WriteCOMFromFileGroup(string Path, string[] SourceFiles)
        {
            //write the COM file using WriteCOMFromFile
            WriteCOMFromGroupOfFiles(Path, SourceFiles);

            return true;
        }

    }
}
