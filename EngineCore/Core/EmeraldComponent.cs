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

        /// <summary>
        /// Is this component deprecated?
        /// </summary>
        public static bool Deprecated { get; set; }

        /// <summary>
        /// Does this component exit the Common Language Runtime and call into native code?
        /// </summary>
        public static bool ExitsCLR { get; set; }

        /// <summary>
        /// Is this component external?
        /// </summary>
        public static bool Experimental { get; set; }

        public EmeraldComponent()
        {
            if (Deprecated) SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Warning: You are initialising deprecated component {DEBUG_COMPONENT_NAME}. This component may be removed soon!");
            if (ExitsCLR) SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Warning: The component {DEBUG_COMPONENT_NAME}, which you are currently initialising, exits the Common Language Runtime\n"
                + "and calls into native code. This component is NOT PORTABLE between Emerald platforms and you cannot debug it without having access to the original (likely C/C++)\n"
                + "code!");
            if (Experimental) SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Warning: You are initialising experimental component {DEBUG_COMPONENT_NAME}. This component may be removed soon!");
        }

    }
}
