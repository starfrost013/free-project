﻿using System;
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
/// Modified: 2019-12-22
/// 
/// Version: 7.50
/// 
/// Purpose: Handles player movement.
/// 
/// </summary>

namespace Free
{
    partial class MainWindow
    {
        /// <summary>
        /// REWRITE! SHITTY CODE!
        /// </summary>
        /// <param name="e"></param>
        public void PlayerMovement(KeyEventArgs e) // pass.
        {
            for (int i = 0; i < currentlevel.OBJLAYOUT.Count; i++)
            {
                IObject obj = currentlevel.OBJLAYOUT[i];

                if (obj.OBJISPLAYER == true) 
                {   
                    if (Gamestate == GameState.Game) // if the game isn't paused.
                    {
                        if (e.Key == Controls.MoveLeft)
                        {
                            if (obj.OBJCANMOVELEFT)
                            {
                                obj.OBJMOVELEFT = true;
                                obj.LastControl = LastCtrl.MoveLeft;
                                Window_ContentRendered(this, new EventArgs()); // re-render the canvas.
                            }
                        }
                        else if (e.Key == Controls.MoveRight)
                        {
                            if (obj.OBJCANMOVERIGHT)
                            {
                                obj.OBJMOVERIGHT = true;
                                obj.LastControl = LastCtrl.MoveRight;
                                Window_ContentRendered(this, new EventArgs()); // re-render the canvas.
                            }
                        }
                        else if (e.Key == Controls.Jump)
                        {
                            if (!obj.OBJISJUMPING & obj.OBJCOLLISIONS > 0 && !e.IsRepeat)
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
                foreach (Obj Object in currentlevel.OBJLAYOUT)
                {

                    // LastCtrl is kept here for physcheck
                    if (Object.OBJPLAYER)
                    {
                        Object.OBJMOVELEFT = false;
                        Object.OBJMOVERIGHT = false;
                        return;
                    }
                }
            }
        }
    }
}
