using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emerald.COM2.Reader
{
    public partial class COMReader2
    {

        internal bool ComReadHeader2(string COMPath)
        {
            // if the file does not exist return false
            if (!File.Exists(COMPath)) return false;

            try
            {
                using (BinaryReader BW = new BinaryReader(new FileStream(COMPath, FileMode.OpenOrCreate)))
                {
                    // Read the header ("EmeraldCOM"). we do not do anything with this.
                    string Header = BW.ReadString();

                    // Check that the header is COM2, error if not
                    if (Header != COMHeader2.Header)
                    {
                        // Check for our legacy 1.6 header
                        if (Header == "EmeraldCOM")
                        {
                            MessageBox.Show($"Cannot load a legacy v1.6 COM file using the COM2 APIs. Please use the COM 1.6 APIs, but be warned - these are deprecated and may be removed at ANY TIME."); 
                        }
                        else
                        {
                            MessageBox.Show($"Error - COM file corrupt - Header is not {COMHeader2.Header}!.");
                        }

                        return false;
                    }

                    // Read the major version of the COM format in this file.
                    byte MajorVersion = BW.ReadByte();

                    // Read the minor version of the COM format in this file.
                    byte MinorVersion = BW.ReadByte();

                    // Prevent incompatibilities - error out if wrong version

                    if (MajorVersion != COMHeader2.MajorVersion || MinorVersion != COMHeader2.MinorVersion)
                    {
                        MessageBox.Show($"Error - COM file corrupt - Incorrect version - expected {COMHeader2.MajorVersion}.{COMHeader2.MinorVersion}, got {MajorVersion}.{MinorVersion}!", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
