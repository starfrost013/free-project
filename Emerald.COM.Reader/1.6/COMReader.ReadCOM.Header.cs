using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emerald.COM.Reader
{
    public partial class COMReader
    {

        internal bool ComReadHeader(string COMPath)
        {
            // if the file does not exist return false
            if (!File.Exists(COMPath)) return false;

            try
            {
                using (BinaryReader BW = new BinaryReader(new FileStream(COMPath, FileMode.OpenOrCreate)))
                {
                    // go to byte #0

                    // Read the header ("EmeraldCOM"). we do not do anything with this.
                    string Header = BW.ReadString();

                    // check that the header is EmeraldCOM, error if not
                    if (Header != COMHeader.Header)
                    {
                        MessageBox.Show($"Error - COM file corrupt - Header is not {COMHeader.Header}!.");
                        return false;
                    }

                    // Read the major version of the COM format in this file.
                    byte MajorVersion = BW.ReadByte();

                    // Read the minor version of the COM format in this file.
                    byte MinorVersion = BW.ReadByte();

                    // Prevent incompatibilities - error out if wrong version

                    if (MajorVersion != COMHeader.MajorVersion || MinorVersion != COMHeader.MinorVersion)
                    {
                        MessageBox.Show($"Error - COM file corrupt - Incorrect version - expected {COMHeader.MajorVersion}.{COMHeader.MinorVersion}, got {MajorVersion}.{MinorVersion}!", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Read the timestamp
                    string Timestamp = BW.ReadString();

                    return true;
                }
            }
            catch (EndOfStreamException err)
            {
                MessageBox.Show($"Fatal error - COM file corrupt - EndOfStreamException: \n\n{err}");
                return false; 
            }
        }
    }
}
