using Emerald.Core;
using SDLX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Free
{

    /// <summary>
    /// Emerald Stage 0 initialisation procedure
    /// 
    /// Initialises Win32 console functionality and initialises the SDL rendering platform.
    /// 
    /// Moved from App.xaml.cs 2021-02-20
    /// </summary>
    public static class SDL_Stage0_Init
    {

        /// <summary>
        /// still temp...SDL Renderer
        /// </summary>
        public static Game SDLGame { get; set; }
        public static FreeSDL SDLEngine { get; set; }

        [STAThread]
        public static void Main(string[] Arguments)
        {   

#if DEBUG

            NativeMethods.AllocConsole();

            IntPtr _ = NativeMethods.GetConsoleWindow();

            if (_ == IntPtr.Zero)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"Win32 error occurred while initialising debug console {Marshal.GetLastWin32Error()}");
            }
            else
            {
                NativeMethods.ShowWindow(_, (int)NativeMethods.Win32__ShowWindow_Mode.SW_SHOWNORMAL);
            }

            Console.OpenStandardOutput();
#endif

            SDLGame = new Game();

            SDLEngine = new FreeSDL();
            SDLEngine.Engine_Init(); 

        }
    }
}
