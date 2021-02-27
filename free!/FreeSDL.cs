using Emerald.Core;
using Emerald.Utilities.Wpf2Sdl;
using EngineCore;
using SDLX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Timers;
using System.Threading;
using Emerald.Utilities;

namespace Free
{
    /// <summary>
    /// Emerald Game Engine SDL Core
    /// 
    /// 2021-01-29 (created 2019-10-30)
    /// </summary>
    public partial class FreeSDL
    {
        public Level currentlevel { get; set; }
        public List<Level> Levels { get; set; }
        public GameState Gamestate { get; set; }
        public enum GameState { Init = 0, Menu, Loading, Game, Pause, Options, EditMode, Exiting }
        public System.Timers.Timer GameTickTimer { get; set; }
        public System.Timers.Timer PhysicsTimer { get; set; }
        public System.Timers.Timer MainLoopTimer { get; set; }
        public List<Interaction> InteractionList { get; set; }
        public List<IGameObject> IGameObjectList { get; set; } // might need to change this
        public List<Animation> NonGameObjectAnimList { get; set; }
        public List<AGTextBlock> TextList { get; set; }
        public List<Weapon> WeaponList { get; set; }
        public double DbgMouseClickLevelX { get; set; }
        public double DbgMouseClickLevelY { get; set; }
        public static int GlobalTimer { get; set; }
        public bool TitleInitialized { get; set; }
        public bool Paused { get; set; }
        public bool FullScreen { get; set; }
        //TEMP
        public Game SDLGame { get; set; }
        public SimpleESX ScriptingCore { get; set; }
        public Thread SDLThread { get; set; }
        public EngineVersion GameVersion { get; set; }

        public FreeSDL()
        {
            // moved to Engine_Init
            // v4.0.1524.1 [2021-02-21]
        }

        /// <summary>
        /// TEMP
        /// </summary>
        public void Engine_Init()
        {
            Engine_BootNow();
            BootNow_SetCurrentLevel(0);
            LoadNow(0);
        }

        public void GameTick(object sender, EventArgs e)
        {
            // in the future,
            // this will update everything
            // as WPF has been completely gutted we are currently doing stuff.
            return; 
        }

        public void MainLoop(object sender, EventArgs e)
        {
           
        }


    }
}
