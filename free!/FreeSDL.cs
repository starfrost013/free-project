using Emerald.Core;
using Emerald.Utilities.Wpf2Sdl;
using EngineCore;
using SDLX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading.Tasks;
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

            Engine_BootNow();
            BootNow_SetCurrentLevel(0);
            LoadNow(0);

        }

        public void GameTick(object sender, EventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    return;
                });
            }
            catch (TaskCanceledException) // BAD but all of our wpf code will be soon obsolete anyway
            {
                Close();
            }
        }

        public void MainLoop(object sender, EventArgs e)
        {
           
        }


    }
}
