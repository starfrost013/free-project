using Emerald.Core;
using Emerald.Utilities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading; 
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

/// <summary>
/// 
/// /Init/EngineInit/Engine_PreInit.cs 
/// 
/// Created: 2020-06-07
/// 
/// Modified: 2020-07-12
/// 
/// Purpose: Initiailises the engine. 
///
/// </summary>

namespace Free
{
    public partial class FreeSDL : Window
    {
        public void Engine_BootNow() 
        {
            // temporary

            NativeMethods.AllocConsole();

#if DEBUG
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
            LogDebug_C("BootNow!", $"BootNow! © 2020 avant-gardé eyes | Engine Now Initialising (version {Utils.GetVersion()})...");
            
            // Log if we're using SDL
            if (Settings.FeatureControl_DisableWPF)
            {
                LogDebug_C("BootNow!", "Using SDL2 renderer");
            }
            else
            {
                LogDebug_C("BootNow!", "Using WPF renderer");
            }

            // Set gamestate
            Gamestate = GameState.Init;

            //temp
            App WinApp = (App)Application.Current;
            SDLGame = WinApp.SDLGame;

            // Load initial structures
            InteractionList = new List<Interaction>();
            NonGameObjectAnimList = new List<Animation>();
            IGameObjectList = new List<IGameObject>();
            TextList = new List<AGTextBlock>();
            WeaponList = new List<Weapon>();
            ScriptingCore = new SimpleESX();
            SDLThread = new Thread(new ThreadStart(WinApp.SDLGame.SDL_Main));
            // Register the game timer. 
            GameTickTimer = new System.Timers.Timer();
            GameTickTimer.Elapsed += GameTick;
            GameTickTimer.Interval = 0.001; // We run AFAP as of 2020-05-26

            // Load everything that we can load at init
            LogDebug_C("BootNow!", "Loading settings...");
            LoadSettings();

            // Init SDL

            LogDebug_C("SDL2 Renderer", "Now Initialising..."); 
            SDLGame.Game_Init();

            LogDebug_C("BootNow!", "Now initialising SimpleESX...");
            ScriptingCore.LoadReflection();
            LogDebug_C("BootNow!", "Now initialising controls...");
            LoadControls();
            LogDebug_C("BootNow!", "Now initialising interactions (deprecated)...");
            LoadInteractions(); // Will be replaced with the scripting engine
            LogDebug_C("BootNow!", "Now initialising weapons...");
            LoadWeapons();
            LogDebug_C("BootNow!", "Now initialising AI...");
            LoadAI();
            LogDebug_C("BootNow!", "Now initialising animations...");
            LoadAnimations();
            LogDebug_C("BootNow!", "Now initialising text XML (deprecated)...");
            LoadTextXml();
            LogDebug_C("BootNow!", "Now initialising master GameObject list...");
            LoadIGameObjects();
            LogDebug_C("BootNow!", "Now initialising main game thread...");
            BootNow_InitMainGameThread();

            if (Settings.FeatureControl_UsePhysicsV2)
            {
                LogDebug_C("BootNow!", "Now initialising physics engine, version 1.0...");
            }
            else
            {
                LogDebug_C("BootNow!", "Now initalising physics engine, version 2.0...");
            }

            InitPhysics();

            // Get version
            GameVersion = new EVersion();

            LogDebug_C("BootNow!", "Acquiring detailed versioning information...");
            GameVersion.GetGameVersion(); // maybe make this a static api

            //TEMP:
            Title = $"free! ({GameVersion.GetVersionString()})";

            LogDebug_C("BootNow!", "Loading levels...");
            Levels = LevelPreloader.LoadLevels(); // Static-class based

            Gamestate = GameState.Menu;

            LogDebug_C("BootNow!", "Fullscreen mode = on");
            if (Settings.WindowType == WindowType.FullScreen) SetFullscreen(); // SET IT
        }
    }
}
