using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

/// <summary>
/// 
/// Weapon.cs
/// 
/// Created: 2019-12-04
/// 
/// Modified: 2019-12-04
/// 
/// Version: 1.00
/// 
/// Purpose: Holds the Weapon class.
/// 
/// </summary>

namespace Free
{
    public partial class Weapon
    {
        public double WEAPONACCURACY { get; set; } // accuracy (%)
        public double WEAPONAMMO { get; set; } // ammo
        public List<Ammo> WEAPONAMMOLIST { get; set; } // ammo list.
        public double WEAPONCRITICALHITCHANCE { get; set; } // critical hit chance
        public double WEAPONCRITICALHITDAMAGEMULTIPLIER { get; set; } // critical hit damage multiplier
        public double WEAPONCRITICALHITDAMAGEUNCERTAINTY { get; set; } // random crit damage (will modify the damage by a minimum of -WEAPONCRITICALHITDAMAGEUNCERTAINTY and a maximum of +WEAPONCRITICALHITDAMAGEUNCERTAINTY)
        public double WEAPONDAMAGE { get; set; } // Damage
        public double WEAPONDAMAGEUNCERTAINTY { get; set; } // random damage (will modify the damage by a minimum of -WEAPONDAMAGEUNCERTAINTY and a maximum of +WEAPONDAMAGEUNCERTAINTY)
        public double WEAPONFIRERATE { get; set; } // Fire rate (bullets/second)
        public WriteableBitmap WEAPONIMAGE { get; set; } // image
        public WriteableBitmap WEAPONIMAGEAMMO { get; set; } // ammo image
        public string WEAPONNAME { get; set; } // name
        public double WEAPONPOSITIONX { get; set; } // weapon posx
        public double WEAPONPOSITIONY { get; set; }

        public Weapon()
        {
            WEAPONAMMOLIST = new List<Ammo>(); // initialize the ammo list
        }
        
    }

    public class Ammo
    {


        // this class only exists so it is easier to draw ammo
        // i might put something in here like the ammo image at some point

        public int AMMOANIMNUMBER { get; set; }
        public WriteableBitmap AMMOIMAGE { get; set; }
        public double X { get; set; } //ai purposes
        public double Y { get; set; } //ai purposes
        public double SPEEDX { get; set; }
        public double SPEEDY { get; set; }


    }
}
