using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            for (int i = 0; i < currentlevel.LevelIGameObjects.Count; i++)
            {
                IGameObject GameObject = currentlevel.LevelIGameObjects[i];

                if (GameObject.GameObjectISPLAYER) 
                {   
                    if (Gamestate == GameState.Game) // if the game isn't paused.
                    {
                        if (e.Key == Controls.MoveLeft)
                        {
                            if (GameObject.GameObjectCANMOVELEFT && !GameObject.IsCollidingLeft())
                            {
                                GameObject.GameObjectMOVELEFT = true;
                                GameObject.LastControl = LastCtrl.MoveLeft;
                                //DrawScene(); 
                                //DrawScene_Threaded(); 
                            }
                        }
                        else if (e.Key == Controls.MoveRight)
                        {
                            if (GameObject.GameObjectCANMOVERIGHT && !GameObject.IsCollidingRight())
                            {
                                GameObject.GameObjectMOVERIGHT = true;
                                GameObject.LastControl = LastCtrl.MoveRight;
                                //DrawScene();
                                //DrawScene_Threaded(); 
                            }
                        }
                        // This is real bad code, fix it, In fact this whole file needs rewriting.
                        else if (e.Key == Controls.Jump)
                        {

                            GameObject.SpaceHeld = true; 

                            if (GameObject.GameObjectCOLLISIONS > 0 && !e.IsRepeat)
                            {
                                GameObject.Jump();
                                
                            }

                            //Window_ContentRendered(this, new EventArgs());
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
                foreach (GameObject IGameObject in currentlevel.LevelIGameObjects)
                {
                    // LastCtrl is kept here for physcheck
                    if (IGameObject.GameObjectPLAYER)
                    {
                        // overhaul this later
                        IGameObject.GameObjectMOVELEFT = false;
                        IGameObject.GameObjectMOVERIGHT = false;
                        IGameObject.SpaceHeld = false;
                        return;
                    }
                }
            }
        }
    }
}
