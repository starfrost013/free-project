using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Emerald.COM2.Writer
{
    public partial class COMWriter2
    {
        public bool WriteCrc32(string ComXPath)
        {
            try
            {
                using (BinaryWriter BW = new BinaryWriter(new FileStream(ComXPath, FileMode.Open)))
                {
                    BW.Seek(31, SeekOrigin.Begin);

                    BW.Write(GetCrc32(File.ReadAllBytes(ComXPath).ToList<byte>())); // LAZINESS DETECTED

                    return true; 
                }
            }
            catch (FileNotFoundException err)
            {
                MessageBox.Show($"Attempted to write CRC32 to file that does not exist!.\n\n{err}");
                return false;
            }
            catch (PathTooLongException err)
            {
                MessageBox.Show($"Whoever wrote MAX_PATH deserves to be shot.\n\n{err}");
                return false;
            }
            catch (IOException err)
            {
                MessageBox.Show($"Fatal I/O error occurred writing CRC32.\n\n{err}");
                return false;
            }
        }
    }
}
