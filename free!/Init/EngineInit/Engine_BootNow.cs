using Emerald.Utilities; 
using System;
using System.Collections.Generic;
using System.Linq;
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
/// Modified: 2020-06-07
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
            // Set gamestate
            Utils.LogDebug("BootNow!", $"BootNow! © 2020 avant-gardé eyes | Engine Now Initialising (version {Utils.GetVersion()})...");
            
            // Log if we're using SDL
            if (Settings.UseSDLX)
            {
                Utils.LogDebug("BootNow!", "Using SDL2 renderer");
            }
            else
            {
                Utils.LogDebug("BootNow!", "Using WPF renderer");
            }

            Gamestate = GameState.Init;

            App WinApp = (App)Application.Current;

            // Load initial structures
            InteractionList = new List<Interaction>();
            NonObjAnimList = new List<Animation>();
            ObjectList = new List<IGameObject>();
            TextList = new List<AGTextBlock>();
            WeaponList = new List<Weapon>();
            ScriptingCore = new SimpleESX();
            SDLThread = new Thread(new ThreadStart(WinApp.SDLGame.SDL_Main));
            // Register the game timer. 
            GameTickTimer = new System.Timers.Timer();
            GameTickTimer.Elapsed += GameTick;
            GameTickTimer.Interval = 0.001; // We run AFAP as of 2020-05-26

            //TEMP:
            Title = "free! (nightly build for June 24th, 2020)";

            // Load everything that we can load at init
            LoadSettings();

            // Init SDL


            WinApp.SDLGame.Game_Init();


            ScriptingCore.LoadReflection(); 
            LoadControls();
            LoadInteractions(); // Will be replaced with the scripting engine
            LoadWeapons();
            LoadAI();
            LoadAnimations();
            LoadTextXml();
            LoadObjects();
            BootNow_InitMainGameThread();
            InitPhysics();

            Levels = LevelPreloader.LoadLevels(); // Static-class based

            Gamestate = GameState.Menu; 

            if (Settings.WindowType == WindowType.FullScreen) SetFullscreen(); // SET IT
        }
    }
}
