using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Free
{
    public partial class FreeSDL
    {
        public void HandleAnimations(IGameObject GameObject)
        {
            foreach (Animation animation in GameObject.GameObjectANIMATIONS)
            {
                if (animation.animationType == AnimationType.Constant)
                {
                    if (GameObject.GameObjectCONSTANTANIMNUMBER < animation.numMs.Count)
                    {
                        if (GlobalTimer % animation.numMs[GameObject.GameObjectCONSTANTANIMNUMBER] == 0)
                        {
                            if (GameObject.GameObjectCONSTANTANIMNUMBER < animation.animImages.Count)
                            {
                                GameObject.GameObjectIMAGE = animation.animImages[GameObject.GameObjectCONSTANTANIMNUMBER];
                                GameObject.GameObjectCONSTANTANIMNUMBER++;
                            }
                            else
                            {
                                GameObject.GameObjectCONSTANTANIMNUMBER = 0;
                            }
                        }
                    }
                    else
                    {
                        GameObject.GameObjectCONSTANTANIMNUMBER = 0;
                    }
                }
            }
        }

        public void PlayAnimation(GameObject GameObject, AnimationType anim)
        {
            foreach (Animation animation in GameObject.GameObjectANIMATIONS)
            {
                if (animation.animationType == anim)
                {
                    WriteableBitmap OriginalImage = new WriteableBitmap(GameObject.GameObjectIMAGE);

                    if (GameObject.GameObjectANIMNUMBER == 0)
                    {
                        OriginalImage = GameObject.GameObjectIMAGE;
                    }
                    if (GameObject.GameObjectANIMNUMBER < animation.numMs.Count)
                    {
                        if (GlobalTimer % animation.numMs[GameObject.GameObjectANIMNUMBER] == 0)
                        {
                            if (GameObject.GameObjectANIMNUMBER < animation.animImages.Count)
                            {
                                GameObject.GameObjectIMAGE = animation.animImages[GameObject.GameObjectANIMNUMBER];
                            }
                            else
                            {
                                GameObject.GameObjectIMAGE = OriginalImage;
                            }
                        }
                    }
                    else
                    {
                        GameObject.GameObjectANIMNUMBER = 0;
                    }
                }
            }
        }

        //ADD SPECIFIC ANIMATION OVERLOAD
        public void PlayAmmoAnimation(Weapon weapon, Ammo ammotoanimate, AnimationType anim) //aaaa
        {
            if (NonGameObjectAnimList.Count > 0 & weapon.WEAPONAMMOLIST.Count > 0)
            {
                foreach (Animation animation in NonGameObjectAnimList)
                {
                    if (animation.animationType == anim)
                    {
                        WriteableBitmap OriginalImage = new WriteableBitmap(weapon.WEAPONIMAGEAMMO);

                        for (int i = 0; i < weapon.WEAPONAMMOLIST.Count; i++)
                        {
                            Ammo ammo = weapon.WEAPONAMMOLIST[i];

                            if (/*ammo == ammotoanimate*/ ammo.X == ammotoanimate.X & ammo.Y == ammotoanimate.Y)
                            {
                                //elseif?
                                if (ammo.AMMOANIMNUMBER == 0)
                                {
                                    OriginalImage = weapon.WEAPONIMAGEAMMO;
                                }
                                if (ammo.AMMOANIMNUMBER < animation.animImages.Count)
                                {
                                    if (GlobalTimer % animation.numMs[ammo.AMMOANIMNUMBER] == 0)
                                    {
                                        if (ammo.AMMOANIMNUMBER < animation.animImages.Count)
                                        {
                                            ammo.AMMOIMAGE = animation.animImages[ammo.AMMOANIMNUMBER];
                                            ammo.AMMOANIMNUMBER++;
                                        }
                                        else
                                        {
                                            ammo.AMMOIMAGE = OriginalImage;
                                            weapon.WEAPONAMMOLIST.Remove(ammo);
                                        }
                                    }
                                }
                                else
                                {
                                    ammo.AMMOANIMNUMBER = 0;
                                    weapon.WEAPONAMMOLIST.Remove(ammo);
                                }

                            }
                        }
                    }
                }
            }
        }

        public void PlayAnimation(GameObject GameObject, Animation anim)
        {
            foreach (Animation animation in GameObject.GameObjectANIMATIONS)
            {
                if (animation == anim)
                {
                    WriteableBitmap OriginalImage = new WriteableBitmap(GameObject.GameObjectIMAGE);
                    if (GameObject.GameObjectANIMNUMBER == 0)
                    {
                        OriginalImage = GameObject.GameObjectIMAGE;
                    }
                    if (GameObject.GameObjectANIMNUMBER < animation.numMs.Count)
                    {
                        if (GlobalTimer % animation.numMs[GameObject.GameObjectANIMNUMBER] == 0)
                        {
                            if (GameObject.GameObjectANIMNUMBER < animation.animImages.Count)
                            {
                                GameObject.GameObjectIMAGE = animation.animImages[GameObject.GameObjectANIMNUMBER];
                            }
                            else
                            {
                                GameObject.GameObjectIMAGE = OriginalImage;
                            }
                        }
                    }
                    else
                    {
                        GameObject.GameObjectANIMNUMBER = 0;
                    }
                }
            }
        }
    }
}
