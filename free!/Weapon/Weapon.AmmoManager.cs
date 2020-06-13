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
    public partial class MainWindow
    {
        public void FireAmmo() // determines the correct obj to fire a bullet from
        {
            foreach (Obj obj in currentlevel.LevelObjects)
            {
                if (obj.OBJHELDWEAPON != null)
                {
                    obj.OBJHELDWEAPON.FireAmmo_Core(obj);
                    return;
                }
            }
            return;
        }
    }
    public partial class Weapon
    {
        internal void FireAmmo_Core(Obj obj)
        {
            Ammo ammo = new Ammo();
            ammo.X = this.WEAPONPOSITIONX + this.WEAPONIMAGE.Width;
            ammo.Y = this.WEAPONPOSITIONY + this.WEAPONIMAGE.Height / 2;

            // - SHITTY CODE - REPLACE WITH FACINGDIRECTION FOR PLAYERS WHEN WE ADD MORE ANIMATIONS - // 
            if (obj.OBJSPEED < 0)
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
