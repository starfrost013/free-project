using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// 
/// File name: InteractionManager.ChangeLevel.cs
/// 
/// Created: 2019-11-21
/// 
/// Modified: 2019-12-23
/// 
/// Version: 1.50
/// 
/// Free Version: 0.20+
/// 
/// Purpose: Handles changing level when hitting object.
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
                    Application.Current.Shutdown(); 
                }
            }
        }
    }
}
