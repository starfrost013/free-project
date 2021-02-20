using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Core
{
    /// <summary>
    /// EmeraldComponent abstract class 
    /// 
    /// Defines a component of Emerald. Mostly used for debug logging and error checking; it may be expanded for other services and might have ome standard init code shared between
    /// all engine components.
    /// 
    /// 2020-02-18
    /// </summary>
    public abstract class EmeraldComponent
    {
        public static int API_VERSION_MAJOR = 0;
        public static int API_VERSION_MINOR = 0;
        public static int API_VERSION_REVISION = 0;
        public static string DEBUG_COMPONENT_NAME = "SDLEmerald Component";
        

    }
}
