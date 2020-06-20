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
    public partial class MainWindow : Window
    {
        public Level currentlevel { get; set; }
        public List<Level> Levels { get; set; }
        public GameState Gamestate { get; set; } 
        public enum GameState { Init = 0, Menu, Loading, Game, Pause, Options, EditMode, Exiting }
        public Timer GameTickTimer { get; set; }
        public Timer PhysicsTimer { get; set; }
        public Timer MainLoopTimer { get; set; }
        public List<Interaction> InteractionList { get; set; }
        public List<IGameObject> ObjectList { get; set; } // might need to change this
        public List<Animation> NonObjAnimList { get; set; }
        public List<AGTextBlock> TextList { get; set; }
        public List<Weapon> WeaponList { get; set; }
        public double DbgMouseClickLevelX { get; set; }
        public double DbgMouseClickLevelY { get; set; }
        public static int GlobalTimer { get; set; }
        public bool TitleInitialized { get; set; }
        public BitmapImage BG { get; set; } // used for the title screen, maybe change 
        public bool Paused { get; set; }
        public bool FullScreen { get; set; }
        //TEMP
        public SimpleESX ScriptingCore { get; set; }
        public MainWindow()
        {
            // Yay
            InitializeComponent();
        }
        
        public void GameTick(object sender, EventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {


                    App CApp = (App)Application.Current;
                    Task.Factory.StartNew(() => CApp.SDLGame.SDL_Main());

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
            foreach (IGameObject Object in currentlevel.LevelObjects)
            {
                if (Object.OBJCANCOLLIDE != false) HandleCollision(Object);
                HandleAnimations(Object); 
                HandleAI(Object);
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
                    FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    MessageBox.Show($"avant-gardé engine\nVer. {FVI.FileVersion} (pre-release)\n© 2019-2020 avant-gardé eyes", "About Engine", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                case Key.F9:
                    LevelSelect LevelSelect = new LevelSelect(this);
                    LevelSelect.Show();
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

                    if (Settings.UseSDLX)
                    {
                        DrawScene_Threaded();
                    }
                    else
                    {
                        DrawScene();
                    }

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
            Settings.Resolution = new Point(this.Width, this.Height);
        }
    }
}
