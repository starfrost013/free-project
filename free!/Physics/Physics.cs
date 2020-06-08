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
/// Physics.cs
/// 
/// Created: 2019-11-05
/// 
/// Modified: 2019-12-31
/// 
/// Purpose: Controls the physics and collision
/// 
/// Version: 1.55
/// 
/// </summary>

namespace Free
{
    partial class MainWindow
    {
        public IObject HandlePhys(IObject obj)
        {

            obj.OBJACCELERATION /= Physics.Friction + 1;

            if (IsSentientBeing(obj)) 
            {
                if (obj.OBJMOVELEFT)
                {
                    obj.MoveLeft();
                    obj.LastControl = LastCtrl.MoveLeft;
                }
                else if (obj.OBJMOVERIGHT)
                {
                    obj.MoveRight();
                    obj.LastControl = LastCtrl.MoveRight;
                }
            }

            if (obj.OBJGRAV == true & obj.OBJCOLLISIONS == 0)
            {
                obj.ChgAcceleration(0, Physics.Gravity * obj.OBJMASS);
            }
            else if (IsSentientBeing(obj) & obj.OBJISJUMPING == true) // jump physics
            {
                obj.ChgAcceleration(0, (Physics.Acceleration * obj.OBJMASS) / 1.7);

                if (obj.OBJACCELERATIONY > 1) // if we're coming down from a jump, set the jumping to false
                {
                    obj.OBJISJUMPING = false;
                }
            }

            // HANDLE OUTSIDE OF FUNCTION?

           

            if (obj.OBJGRAV)
            {
                if (obj.OBJSPEED != 0)
                {
                    obj.ChgDeceleration(Physics.Friction, 0);
                }
            }

            if (obj.OBJDECELERATION > obj.OBJSPEED / Physics.SpeedConst)
            {
                obj.OBJDECELERATION = obj.OBJSPEED / Physics.SpeedConst;
            }
            //this fixes a bug where your character would humorously veer off to the right if you jumped left.
            else if (obj.OBJDECELERATION < -obj.OBJSPEED / -Physics.SpeedConst)
            {
                obj.OBJDECELERATION = -obj.OBJSPEED / -Physics.SpeedConst;
            }

            if (obj.OBJSPEED > 0)
            {
                if (obj.OBJSPEED - obj.OBJDECELERATION < 0)
                {
                    obj.OBJSPEED = 0;
                }
            }
            else if (obj.OBJSPEED < 0)
            {
                if (obj.OBJSPEED + obj.OBJDECELERATION > 0)
                {
                    obj.OBJSPEED = 0;
                }
            }

            obj.OBJSPEED += obj.OBJACCELERATION;
            obj.OBJSPEEDY += obj.OBJACCELERATIONY;
            obj.OBJSPEED -= obj.OBJDECELERATION;
            obj.OBJSPEEDY -= obj.OBJDECELERATIONY;

            //speedcap
            if (obj.OBJSPEED < -Physics.MaxSpeed)
            {
                obj.OBJSPEED = -Physics.MaxSpeed;
            }
            if (obj.OBJSPEED > Physics.MaxSpeed)
            {
                obj.OBJSPEED = Physics.MaxSpeed;
            }

            if (obj.OBJSPEEDY < -Physics.MaxSpeed)
            {
                obj.OBJSPEEDY = -Physics.MaxSpeed;
            }
            if (obj.OBJSPEEDY > Physics.MaxSpeed)
            {
                obj.OBJSPEEDY = Physics.MaxSpeed;
            }


            obj.OBJX += obj.OBJSPEED;
            obj.OBJY += obj.OBJSPEEDY; // when plr is at bottom of level y pos = level bg height

            if (obj.OBJHELDWEAPON != null & obj.OBJPLAYER) // Draws the ammo.
            {
                if (obj.OBJHELDWEAPON.WEAPONAMMOLIST.Count != 0) // if ammo is being drawn...
                {
                    foreach (Ammo ammo in obj.OBJHELDWEAPON.WEAPONAMMOLIST) // iterate through all ammo.
                    {
                        ammo.X += ammo.SPEEDX; // move it!!
                        ammo.Y += ammo.SPEEDY;
                    }
                }
            }

            return obj; // yeah


        }
    }
}
