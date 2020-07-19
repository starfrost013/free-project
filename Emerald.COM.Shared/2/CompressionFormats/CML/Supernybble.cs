using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.COM2
{
    public class Supernybble
    {
        public static int SupernybbleMajor = 1;
        public static int SupernybbleMinor = 0;
        public BitArray NybbleData { get; set; }

        public Supernybble()
        {
            NybbleData.Length = 8; // this is changed to 6 later.
        }
    }
}
