using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// /Weapons/Weapon.AmmoManager.cs
/// 
/// Version: 1.10
/// 
/// Created: 2019-12-18
/// 
/// Modified: 2019-12-23
/// 
/// Purpose: Manages firing ammo.
/// 
/// </summary>

namespace Free
{
    public partial class FreeSDL
    {
        public void FireAmmo() // determines the correct GameObject to fire a bullet from
        {
            foreach (GameObject GameObject in currentlevel.LevelIGameObjects)
            {
                if (GameObject.GameObjectHELDWEAPON != null)
                {
                    GameObject.GameObjectHELDWEAPON.FireAmmo_Core(GameObject);
                    return;
                }
            }
            return;
        }
    }
    public partial class Weapon
    {
        internal void FireAmmo_Core(GameObject GameObject)
        {
            Ammo ammo = new Ammo();
            ammo.X = this.WEAPONPOSITIONX + this.WEAPONIMAGE.Width;
            ammo.Y = this.WEAPONPOSITIONY + this.WEAPONIMAGE.Height / 2;

            // - SHITTY CODE - REPLACE WITH FACINGDIRECTION FOR PLAYERS WHEN WE ADD MORE ANIMATIONS - // 
            if (GameObject.GameObjectSPEED < 0)
            {
                ammo.SPEEDX = -20;
            }
            else
            {
                ammo.SPEEDX = 20;
            }
            // - END SHITTY CODE - REPLACE WITH FACINGDIRECTION FOR PLAYERS WHEN WE ADD MORE ANIMATIONS - //

            ammo.SPEEDY = 0;
            ammo.AMMOIMAGE = this.WEAPONIMAGEAMMO;

            this.WEAPONAMMOLIST.Add(ammo);
            return;
        }
    }
}
