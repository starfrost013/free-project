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
/// Purpose: Initialises the engine. 
///
/// </summary>

namespace Free
{
    public partial class FreeSDL
    {
        public void Engine_BootNow() 
        {

            SDLDebug.LogDebug_C("BootNow!", $"BootNow! © 2020 avant-gardé eyes | Engine Now Initialising (version {Utils.GetVersion()})...");
            
            // Log if we're using SDL
            if (Settings.FeatureControl_DisableWPF)
            {
                SDLDebug.LogDebug_C("BootNow!", "Using SDL2 renderer");
            }
            else
            {
                SDLDebug.LogDebug_C("BootNow!", "Using WPF renderer");
            }

            // Set gamestate
            Gamestate = GameState.Init;

            //temp
            SDLGame = SDL_Stage0_Init.SDLGame;

            // Load initial structures
            InteractionList = new List<Interaction>();
            NonGameObjectAnimList = new List<Animation>();
            IGameObjectList = new List<IGameObject>();
            TextList = new List<AGTextBlock>();
            WeaponList = new List<Weapon>();
            ScriptingCore = new SimpleESX();

            SDLThread = new Thread(new ThreadStart(SDL_Stage0_Init.SDLGame.SDL_Main));


            // Load everything that we can load at init
            SDLDebug.LogDebug_C("BootNow!", "Loading settings...");
            SettingLoader.LoadSettings();

            // Init SDL

            SDLDebug.LogDebug_C("SDL2 Renderer", "Now Initialising...");


            //TEMP:
            // Set SDL title here.
            //Title = $"free! ({GameVersion.GetVersionString()})";


            if (!SDLGame.Game_Init())
            {
                Error.Throw(null, ErrorSeverity.FatalError, "SDL Initialisation failure. Cannot initalise K19.", "SDLEmerald", 0x400DEAD);
            }

            // Register the game timer. 
            GameTickTimer = new System.Timers.Timer();
            GameTickTimer.Elapsed += GameTick;
            GameTickTimer.Interval = 0.001; // We run AFAP as of 2020-05-26

            SDLDebug.LogDebug_C("BootNow!", "Loading levels...");
            Levels = LevelPreloader.LoadLevels(); // Static-class based

            SDLDebug.LogDebug_C("BootNow!", "Now initialising SimpleESX...");
            ScriptingCore.LoadReflection();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising controls...");
            LoadControls();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising interactions (deprecated)...");
            LoadInteractions(); // Will be replaced with the scripting engine
            SDLDebug.LogDebug_C("BootNow!", "Now initialising weapons...");
            LoadWeapons();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising AI...");
            LoadAI();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising animations...");
            LoadAnimations();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising text XML (deprecated)...");
            LoadTextXml();

            SDLDebug.LogDebug_C("BootNow!", "Now initialising master GameObject list...");
            LoadIGameObjects();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising main game thread...");
            BootNow_InitMainGameThread();
            SDLDebug.LogDebug_C("BootNow!", "Now initialising physics engine [old]...");
            InitPhysics();

            // Get version
            GameVersion = new EngineVersion();

            SDLDebug.LogDebug_C("BootNow!", "Acquiring detailed versioning information...");
            GameVersion.GetGameVersion(); // maybe make this a static api



            Gamestate = GameState.Menu;

            SDLDebug.LogDebug_C("BootNow!", "Fullscreen mode = on");

            if (Settings.WindowType == WindowType.FullScreen) SetFullscreen(); // SET IT

            // Temporary Code //
        }
    }
}
