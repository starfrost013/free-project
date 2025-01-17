﻿using Emerald.Core;
using Emerald.Utilities.Wpf2Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// File name: InteractionManager.ChangeLevel.cs
/// 
/// Created: 2019-11-21
/// 
/// Modified: 2021-02-27
/// 
/// Version: 1.51
/// 
/// Free Version: Engine Version 4.0.1550.0+
/// 
/// Purpose: Handles changing level when hitting IGameObject.
/// 
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        public void Interaction_ChangeLevel()
        {
            if (!Settings.DemoMode)
            {
                LoadNow(currentlevel.ID + 1); // load the next level by ID. THIS LEVEL DOES NOT HAVE TO EXIST, IT WILL STILL ATTEMPT TO LOAD IT, AND IT WILL CRASH BECAUSE ITS XML FILES DO NOT EXIST!!!!!
            }
            else
            {
                if (currentlevel.ID < Settings.DemoModeMaxLevel) //todo: Settings.DemoMax
                {
                    LoadNow(currentlevel.ID + 1);
                }
                else
                {

                    MessageBox.Show($"The demo has ended.\n\nThank you for playing {Settings.GameName}!", Settings.GameName, MessageBoxButton.OK, MessageBoxImage.Information);

                    //todo: proper engine shutdown function
                    Environment.Exit(7771);  
                }
            }
        }
    }
}
