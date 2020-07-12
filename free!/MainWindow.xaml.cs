using Emerald.Core;
using Emerald.COM2;
using Emerald.COM2.Writer;
using Emerald.Utilities.Wpf2Sdl;
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

/// <summary>
/// 
/// free!
/// 
/// © 2019-2020 Cosmo.  
/// 
/// Version 0.21 (avant-gardé engine Ver. 2.20 alpha)
/// 
/// Level and interaction editor included for development and modding purposes.
/// 
/// ALPHA RELEASE
/// 
/// </summary>

namespace Free
{
    public partial class FreeSDL : Window
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
        public EVersion GameVersion { get; set; }
        

        public FreeSDL()
        {
            // Yay

            // Just temporary things for some stuff
            COMWriter2 CW2 = new COMWriter2();
            CW2.WriteCom2FromGroupOfFiles("ComX-v2.5.ComX", new List<string> { "ComXTest.xml " }); //ComX-platform v2.5

            InitializeComponent();

             
        }
        
        public void GameTick(object sender, EventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    Window_ContentRendered(this, new EventArgs());
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
            foreach (IGameObject IGameObject in currentlevel.LevelIGameObjects)
            {
                if (IGameObject.GameObjectCANCOLLIDE != false) HandleCollision(IGameObject);
                HandleAnimations(IGameObject); 
                HandleAI(IGameObject);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) // handles stuff like controls? maybe?
        {
            switch (e.Key)
            {
                case Key.F7:
                    MessageBox.Show("free!\n[nightly]\n© 2019-2020 avant-gardé eyes", "About", MessageBoxButton.OK, MessageBoxImage.Information);
                    return; 
                case Key.F8:
                    MessageBox.Show($"avant-gardé engine\nVer. {GameVersion.GetVersionString()} (pre-release)\n© 2019-2020 avant-gardé eyes", "About Engine", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                case Key.F9:
                    if (Settings.DebugMode)
                    {
                        LevelSelect LevelSelect = new LevelSelect(this);
                        LevelSelect.Show();
                    }

                    return;
                case Key.F11:
                    SetFullscreen();
                    return; 
                case Key.F12:
                    EnterEditMode();
                    return; 
                case Key.Enter:
                    if (Gamestate == GameState.Menu) LoadNow(0);
                    return;
                default:
                    if (Gamestate == GameState.Game || Gamestate == GameState.Pause) PlayerMovement(e);
                    return; 
            }
        }


        private void PhysicsTimerElapsed(object sender, EventArgs e)
        {
            if (Gamestate == GameState.Game) RunPhysics();
            return; 
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            GlobalTimer++;
            
            switch (Gamestate)
            {
                case GameState.Game:
                case GameState.EditMode:
                    // Temporary

                    DrawScene();

                    return;
                case GameState.Menu:
                    if (!TitleInitialized)
                    {
                        InitTitle();
                        return;
                    }
                    else
                    {
                        //DrawTitle(); - when we want to do stuff with the title screen in later versions we will add this
                        return;
                    }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Engine_BootNow(); 
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Settings.Resolution = new SDLPoint(e.NewSize.Width, e.NewSize.Height); 
        }

    }
}
