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
/// Physics.cs
/// 
/// Created: 2019-11-05
/// 
/// Modified: 2019-12-31
/// 
/// Purpose: Controls the physics and collision
/// 
/// Version: 1.60 [bringup-2.21.1352.0 v1.55 → v1.60 make it slightly less shit, implement jumpintensity]
/// 
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        public object HandlePhys(IGameObject GameObject)
        {

            GameObject.GameObjectACCELERATION /= Physics.Friction + 1;

            if (IsSentientBeing(GameObject)) 
            {
                if (GameObject.GameObjectMOVELEFT)
                {
                    GameObject.MoveLeft();
                    GameObject.LastControl = LastCtrl.MoveLeft;
                }
                else if (GameObject.GameObjectMOVERIGHT)
                {
                    GameObject.MoveRight();
                    GameObject.LastControl = LastCtrl.MoveRight;
                }
            }

            if (GameObject.GameObjectGRAV && GameObject.GameObjectCOLLISIONS == 0)
            {
                GameObject.ChgAcceleration(0, Physics.Gravity * GameObject.GameObjectMASS);
            }
            else if (IsSentientBeing(GameObject) && GameObject.GameObjectISJUMPING) // jump physics
            {
                GameObject.ChgAcceleration(0, (Physics.Acceleration * GameObject.GameObjectMASS) / (3.1 - GameObject.JumpIntensity));

                if (GameObject.SpaceHeld) GameObject.JumpIntensity += 0.0333;

                //TEMP
                if (GameObject.JumpIntensity > Physics.MaxJumpIntensity) GameObject.JumpIntensity = 1.5;

                if (GameObject.GameObjectACCELERATIONY > 1) // if we're coming down from a jump, set the jumping to false
                {
                    GameObject.GameObjectISJUMPING = false;
                    GameObject.JumpIntensity = 1;
                }
            }

            // HANDLE OUTSIDE OF FUNCTION?

           

            if (GameObject.GameObjectGRAV)
            {
                if (GameObject.GameObjectSPEED != 0)
                {
                    GameObject.ChgDeceleration(Physics.Friction, 0);
                }
            }

            if (GameObject.GameObjectDECELERATION > GameObject.GameObjectSPEED / Physics.SpeedConst)
            {
                GameObject.GameObjectDECELERATION = GameObject.GameObjectSPEED / Physics.SpeedConst;
            }
            //this fixes a bug where your character would humorously veer off to the right if you jumped left.
            else if (GameObject.GameObjectDECELERATION < -GameObject.GameObjectSPEED / -Physics.SpeedConst)
            {
                GameObject.GameObjectDECELERATION = -GameObject.GameObjectSPEED / -Physics.SpeedConst;
            }

            if (GameObject.GameObjectSPEED > 0)
            {
                if (GameObject.GameObjectSPEED - GameObject.GameObjectDECELERATION < 0)
                {
                    GameObject.GameObjectSPEED = 0;
                }
            }
            else if (GameObject.GameObjectSPEED < 0)
            {
                if (GameObject.GameObjectSPEED + GameObject.GameObjectDECELERATION > 0)
                {
                    GameObject.GameObjectSPEED = 0;
                }
            }

            GameObject.GameObjectSPEED += GameObject.GameObjectACCELERATION;
            GameObject.GameObjectSPEEDY += GameObject.GameObjectACCELERATIONY;
            GameObject.GameObjectSPEED -= GameObject.GameObjectDECELERATION;
            GameObject.GameObjectSPEEDY -= GameObject.GameObjectDECELERATIONY;

            //speedcap
            if (GameObject.GameObjectSPEED < -Physics.MaxSpeed)
            {
                GameObject.GameObjectSPEED = -Physics.MaxSpeed;
            }
            if (GameObject.GameObjectSPEED > Physics.MaxSpeed)
            {
                GameObject.GameObjectSPEED = Physics.MaxSpeed;
            }

            if (GameObject.GameObjectSPEEDY < -Physics.MaxSpeed)
            {
                GameObject.GameObjectSPEEDY = -Physics.MaxSpeed;
            }
            if (GameObject.GameObjectSPEEDY > Physics.MaxSpeed)
            {
                GameObject.GameObjectSPEEDY = Physics.MaxSpeed;
            }


            GameObject.Position.X += GameObject.GameObjectSPEED;
            GameObject.Position.Y += GameObject.GameObjectSPEEDY; // when plr is at bottom of level y pos = level bg height

            if (GameObject.GameObjectHELDWEAPON != null && GameObject.GameObjectPLAYER) // Draws the ammo.
            {
                if (GameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.Count != 0) // if ammo is being drawn...
                {
                    foreach (Ammo ammo in GameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST) // iterate through all ammo.
                    {
                        ammo.X += ammo.SPEEDX; // move it!!
                        ammo.Y += ammo.SPEEDY;
                    }
                }
            }

            return GameObject; // yeah


        }
    }
}
