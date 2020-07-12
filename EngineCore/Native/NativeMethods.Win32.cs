using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static extern bool AllocConsole(); // allocconsole probably works better

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static uint Win32__AttachConsole_Default_PID = 0x0ffffffff; // .NET 

        public const uint Win32__Error_Success = 0;
        public const uint Win32__Error_AccessDenied = 5;
        
        public enum Win32__ShowWindow_Mode { SW_HIDE, SW_SHOWNORMAL, SW_SHOWMINIMIZED, SW_SHOWMAXIMIZED, SW_SHOWNOACTIVATE, SW_SHOW, SW_MINIMIZE, SW_SHOWMINNOACTIVE, SW_SHOWNA, SW_RESTORE } // many ways of showing windows
    }
}
