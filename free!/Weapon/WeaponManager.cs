using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Free
{
    public partial class FreeSDL
    {
        public void GiveWeapon(GameObject GameObject, string WeaponName)
        {
            if (WeaponName == "" || WeaponName == null) // Remove currently held weapon
            {
                GameObject.GameObjectHELDWEAPON = null;
                return;
            }

            foreach (Weapon weapon in WeaponList)
            {
                if (weapon.WEAPONNAME == WeaponName)
                {
                    GameObject.GameObjectHELDWEAPON = weapon;
                }
            }
        }


        private void Window_MouseDown(IGameObject sender, MouseButtonEventArgs e)
        {
            if (Gamestate == GameState.EditMode)
            {
                //editmode - for add-IGameObject picking
                DbgMouseClickLevelX = e.GetPosition(Game).X; //easy scrolling lol
                DbgMouseClickLevelY = e.GetPosition(Game).Y;
            }
            else if (Gamestate == GameState.Game) // fire in the hole
            {
                switch (Controls.Fire_MouseButton)
                {
                    case "MOUSE1":
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            FireAmmo(); //Fire in the hole
                        }
                        return;
                    case "MOUSE2":
                        if (e.RightButton == MouseButtonState.Pressed)
                        {
                            FireAmmo(); //Fire in the hole
                        }
                        return;
                    case "MOUSE3":
                        if (e.MiddleButton == MouseButtonState.Pressed)
                        {
                            FireAmmo(); //Fire in the hole
                        }
                        return;

                }
            }
        }
    }
}
