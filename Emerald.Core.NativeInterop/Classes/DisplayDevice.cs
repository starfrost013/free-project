using Microsoft.Win32;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Core.NativeInterop
{
    /// <summary>
    /// Managed code for Win32 DISPLAY_DEVICE structure. DOES NOT CORRELATE WITH .NET PRACTICES!
    /// </summary>
    public class DISPLAY_DEVICE
    {
        /// <summary>
        /// The size of this structure.
        /// </summary>
        public int cb { get; set; }

        /// <summary>
        /// The name of the display device.
        /// </summary>
        public char[] DeviceName { get; set; }

        /// <summary>
        /// The description of the display device.
        /// </summary>
        public char[] DeviceString { get; set; }

        /// <summary>
        /// Display device flags. [Todo - make this an enum]
        /// </summary>
        public int DeviceFlags { get; set; }
        
        /// <summary>
        /// "Not used". ID of the device, one supposes
        /// </summary>
        public char[] DeviceID { get; set; }

        /// <summary>
        /// "Reserved". Thank you MSDN.
        /// </summary>
        public char[] DeviceKey { get; set; }

        public DISPLAY_DEVICE()
        {
            DeviceKey = new char[128];
            DeviceID = new char[128];
            DeviceName = new char[32];
            DeviceString = new char[128];
            cb = 424; // remove if failing

        }
    }
}
