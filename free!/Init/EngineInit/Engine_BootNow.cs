using Emerald.Utilities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class MainWindow : Window
    {
        public void Engine_BootNow() 
        {
            // Set gamestate
            Utils.LogDebug("BootNow!", $"BootNow! © 2020 avant-gardé eyes | Engine Now Initialising (version {Utils.GetVersion()})...");
            Gamestate = GameState.Init; 

            // Load initial structures
            InteractionList = new List<Interaction>();
            NonObjAnimList = new List<Animation>();
            ObjectList = new List<IGameObject>();
            TextList = new List<AGTextBlock>();
            WeaponList = new List<Weapon>();
            ScriptingCore = new SimpleESX();

            // Register the game timer. 
            GameTickTimer = new Timer();
            GameTickTimer.Elapsed += GameTick;
            GameTickTimer.Interval = 0.001; // We run AFAP as of 2020-05-26

            //TEMP:
            Title = "free! (nightly build for June 14th, 2020)";

            // Load everything that we can load at init
            LoadSettings();
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
