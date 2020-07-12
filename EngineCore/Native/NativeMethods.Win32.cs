﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Static native methods.
/// </summary>

namespace Free
{
    public static class NativeMethods
    {
        [DllImport("kernel32.dll",SetLastError = true)]
        public static extern bool AttachConsole(uint dwProcessId);

        public static uint Win32__AttachConsole_Default_PID = 0x0ffffffff; // .NET 

        public const uint Win32__Error_Success = 0;
        public const uint Win32__Error_AccessDenied = 5;
    }
}
