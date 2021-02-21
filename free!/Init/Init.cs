using Emerald.Core; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Free
{
    partial class FreeSDL
    {
        /// <summary>
        /// Make this less messy. Preloader efforts have worked great but it's still a mess. 
        /// </summary>
        /// <param name="LevelId"></param>
        public void LoadNow(int LevelId)
        {
            try
            {
                Gamestate = GameState.Loading;
                // Ok this is temp but we are gonna get rid of sdlthread and have a few threads for rendering, physics, etc
                if (SDLThread.IsAlive) SDLThread.Suspend(); 
                GameTickTimer.Stop();
                MainLoopTimer.Stop(); 
                PhysicsTimer.Stop();

                //Clear ammo

                if (currentlevel != null)
                {
                    foreach (GameObject GameObject in currentlevel.LevelIGameObjects)
                    {
                        if (GameObject.GameObjectHELDWEAPON != null)
                        {
                            GameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.Clear(); // Annihilate Ammo 
                        }
                    }
                }

                //if (Game.Children.Count > 0) Game.Children.Clear();

                GlobalTimer = 0;

                //currentlevel = LoadLevel(LevelId);
                BootNow_SetCurrentLevel(LevelId);

                currentlevel.LoadLevelObjects(IGameObjectList, currentlevel);
                ScriptingCore.LoadAllLevelScripts(IGameObjectList); 
                currentlevel.LoadLevelMusic();

                Gamestate = GameState.Game; //yeah 

                //if we have level text
                if (currentlevel.TEXTPATH != null)
                {
                    LoadLevelText();
                }

                // TEMP CODE REMOVE WHEN HUD ADDED //

                GameObject tempgiveweaponGameObject = new GameObject();

                foreach (GameObject tempiteration in currentlevel.LevelIGameObjects)
                {
                    if (tempiteration.GameObjectPLAYER)
                    {
                        GiveWeapon(tempiteration, "Test Weapon");
                    }
                }

                // END TEMP CODE //
                // DEBUG CODE - SHOWS GLOBAL TIMER VALUE //

                if (Settings.DebugMode)
                {
                    AGTextBlock GlobalTimerD = LoadText(GlobalTimer.ToString(), 0, 0);
                    SetTextColour(GlobalTimerD, new Color { R = 0, G = 0, B = 127, A = 255 });
                    SetTextColourBg(GlobalTimerD, new Color { R = 255, G = 255, B = 255, A = 192 });
                    SetTextInternalName(GlobalTimerD, "GlobalTimerDebug");
                    SetTextVisibility(GlobalTimerD, true);
                }

                // Set resolution=
                if (Settings.Resolution.X > 0 || Settings.Resolution.Y > 0) // setresolution()
                {
                    //Legacy WPF Code Removed 2020-12-30

                    /*
                    //Scrollbar.Width = Settings.Resolution.X;
                    //Scrollbar.Height = Settings.Resolution.Y;
                    //Game.Width = currentlevel.Size.X;
                    //Game.Height = currentlevel.Size.Y;

                    if (Scrollbar.Width > this.Width || Scrollbar.Height > this.Height || Game.Width > this.Width || Game.Height > this.Height)
                    {
                        this.Width = Scrollbar.Width + 30;
                        this.Height = Scrollbar.Height + 40;
                    }
                    */
                }

                SimpleESXScript SEXScript = new SimpleESXScript();
                // fix - 2021-02-19
                ScriptingCore.LoadScript(@"Content\Test.esx");
                ScriptingCore.ExecuteCurrentScript(); // Execute the current script.


            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. This is most likely because a change level IGameObject attempted to trigger its interaction but the next level by ID doesn't exist yet.", "avant-gardé engine", 10);
                return;
            }
            
            GameTickTimer.Start();
            //if (Settings.UseSDLX) MainLoopTimer.Start(); 

            // Temporary. We will have an SDLLoad function. 
            SDLThread.Start();
            PhysicsTimer.Start();
        }
    }
}
