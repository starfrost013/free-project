using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Shapes;

/// <summary>
/// 
/// PlayerMovement.cs
/// 
/// Created: 2019-11-03
/// 
/// Modified: 2020-06-14 [bringup-2.21.1368.49 v7.60 → v7.70: Call DrawScene instead of whatever the fuck we were doing before]
/// 
/// Version: 7.70 [Needs rewrite]
/// 
/// Purpose: Handles player movement.
/// 
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        /// <summary>
        /// REWRITE! SHITTY CODE!
        /// </summary>
        /// <param name="e"></param>
        public void PlayerMovement(KeyEventArgs e) // pass.
        {
            for (int i = 0; i < currentlevel.LevelObjects.Count; i++)
            {
                IGameObject obj = currentlevel.LevelObjects[i];

                if (obj.OBJISPLAYER) 
                {   
                    if (Gamestate == GameState.Game) // if the game isn't paused.
                    {
                        if (e.Key == Controls.MoveLeft)
                        {
                            if (obj.OBJCANMOVELEFT)
                            {
                                obj.OBJMOVELEFT = true;
                                obj.LastControl = LastCtrl.MoveLeft;
                                DrawScene(); 
                                //DrawScene_Threaded(); 
                            }
                        }
                        else if (e.Key == Controls.MoveRight)
                        {
                            if (obj.OBJCANMOVERIGHT)
                            {
                                obj.OBJMOVERIGHT = true;
                                obj.LastControl = LastCtrl.MoveRight;
                                DrawScene();
                                //DrawScene_Threaded(); 
                            }
                        }
                        // This is real bad code, fix it, In fact this whole file needs rewriting.
                        else if (e.Key == Controls.Jump)
                        {

                            obj.SpaceHeld = true; 

                            if (obj.OBJCOLLISIONS > 0 && !e.IsRepeat)
                            {
                                obj.Jump();
                                
                            }

                            Window_ContentRendered(this, new EventArgs());
                        }
                    }
                    if (e.Key == Controls.Pause)
                    {
                        switch (Gamestate)
                        {
                            case GameState.Pause:
                                Gamestate = GameState.Game;
                                return;
                            case GameState.Game:
                                Gamestate = GameState.Pause;
                                return;
                        }
                    }
                }

            }
        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentlevel != null) // ADD ISLOADED!
            {
                foreach (Obj Object in currentlevel.LevelObjects)
                {
                    // LastCtrl is kept here for physcheck
                    if (Object.OBJPLAYER)
                    {
                        // overhaul this later
                        Object.OBJMOVELEFT = false;
                        Object.OBJMOVERIGHT = false;
                        Object.SpaceHeld = false;
                        return;
                    }
                }
            }
        }
    }
}
