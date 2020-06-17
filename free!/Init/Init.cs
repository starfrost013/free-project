using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Free
{
    partial class MainWindow
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
                GameTickTimer.Stop();
                MainLoopTimer.Stop(); 
                PhysicsTimer.Stop();

                //Clear ammo

                if (currentlevel != null)
                {
                    foreach (Obj obj in currentlevel.LevelObjects)
                    {
                        if (obj.OBJHELDWEAPON != null)
                        {
                            obj.OBJHELDWEAPON.WEAPONAMMOLIST.Clear(); // Annihilate Ammo 
                        }
                    }
                }

                if (Game.Children.Count > 0) Game.Children.Clear();

                GlobalTimer = 0;

                //currentlevel = LoadLevel(LevelId);
                BootNow_SetCurrentLevel(LevelId);

                currentlevel.LoadLevelObjects(ObjectList, currentlevel);
                ScriptingCore.LoadAllLevelScripts(ObjectList); 
                currentlevel.LoadLevelMusic();

                Gamestate = GameState.Game; //yeah 

                //if we have level text
                if (currentlevel.TEXTPATH != null)
                {
                    LoadLevelText();
                }

                // TEMP CODE REMOVE WHEN HUD ADDED //

                Obj tempgiveweaponobj = new Obj();

                foreach (Obj tempiteration in currentlevel.LevelObjects)
                {
                    if (tempiteration.OBJPLAYER)
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
                    Scrollbar.Width = Settings.Resolution.X;
                    Scrollbar.Height = Settings.Resolution.Y;
                    Game.Width = currentlevel.Size.X;
                    Game.Height = currentlevel.Size.Y;

                    if (Scrollbar.Width > this.Width || Scrollbar.Height > this.Height || Game.Width > this.Width || Game.Height > this.Height)
                    {
                        this.Width = Scrollbar.Width + 30;
                        this.Height = Scrollbar.Height + 40;
                    }
                }


                // Script Init - This will be moved to Engine_PreInit()
                SimpleESX Sesx = new SimpleESX();
                Sesx.LoadReflection();
                SimpleESXScript SEXScript = new SimpleESXScript();
                Sesx.LoadScript("Test.esx");
                Sesx.ExecuteCurrentScript(); // Execute the current script.
                // End Temp Script Init

            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. This is most likely because a change level object attempted to trigger its interaction but the next level by ID doesn't exist yet.", "avant-gardé engine", 10);
                return;
            }

            GameTickTimer.Start();
            //if (Settings.UseSDLX) MainLoopTimer.Start(); 
            PhysicsTimer.Start();
        }
    }
}
