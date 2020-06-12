using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Free
{
    public partial class MainWindow
    {
        public void HandleAnimations(IGameObject obj)
        {
            foreach (Animation animation in obj.OBJANIMATIONS)
            {
                if (animation.animationType == AnimationType.Constant)
                {
                    if (obj.OBJCONSTANTANIMNUMBER < animation.numMs.Count)
                    {
                        if (GlobalTimer % animation.numMs[obj.OBJCONSTANTANIMNUMBER] == 0)
                        {
                            if (obj.OBJCONSTANTANIMNUMBER < animation.animImages.Count)
                            {
                                obj.OBJIMAGE = animation.animImages[obj.OBJCONSTANTANIMNUMBER];
                                obj.OBJCONSTANTANIMNUMBER++;
                            }
                            else
                            {
                                obj.OBJCONSTANTANIMNUMBER = 0;
                            }
                        }
                    }
                    else
                    {
                        obj.OBJCONSTANTANIMNUMBER = 0;
                    }
                }
            }
        }

        public void PlayAnimation(Obj obj, AnimationType anim)
        {
            foreach (Animation animation in obj.OBJANIMATIONS)
            {
                if (animation.animationType == anim)
                {
                    WriteableBitmap OriginalImage = new WriteableBitmap(obj.OBJIMAGE);

                    if (obj.OBJANIMNUMBER == 0)
                    {
                        OriginalImage = obj.OBJIMAGE;
                    }
                    if (obj.OBJANIMNUMBER < animation.numMs.Count)
                    {
                        if (GlobalTimer % animation.numMs[obj.OBJANIMNUMBER] == 0)
                        {
                            if (obj.OBJANIMNUMBER < animation.animImages.Count)
                            {
                                obj.OBJIMAGE = animation.animImages[obj.OBJANIMNUMBER];
                            }
                            else
                            {
                                obj.OBJIMAGE = OriginalImage;
                            }
                        }
                    }
                    else
                    {
                        obj.OBJANIMNUMBER = 0;
                    }
                }
            }
        }

        //ADD SPECIFIC ANIMATION OVERLOAD
        public void PlayAmmoAnimation(Weapon weapon, Ammo ammotoanimate, AnimationType anim) //aaaa
        {
            if (NonObjAnimList.Count > 0 & weapon.WEAPONAMMOLIST.Count > 0)
            {
                foreach (Animation animation in NonObjAnimList)
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

        public void PlayAnimation(Obj obj, Animation anim)
        {
            foreach (Animation animation in obj.OBJANIMATIONS)
            {
                if (animation == anim)
                {
                    WriteableBitmap OriginalImage = new WriteableBitmap(obj.OBJIMAGE);
                    if (obj.OBJANIMNUMBER == 0)
                    {
                        OriginalImage = obj.OBJIMAGE;
                    }
                    if (obj.OBJANIMNUMBER < animation.numMs.Count)
                    {
                        if (GlobalTimer % animation.numMs[obj.OBJANIMNUMBER] == 0)
                        {
                            if (obj.OBJANIMNUMBER < animation.animImages.Count)
                            {
                                obj.OBJIMAGE = animation.animImages[obj.OBJANIMNUMBER];
                            }
                            else
                            {
                                obj.OBJIMAGE = OriginalImage;
                            }
                        }
                    }
                    else
                    {
                        obj.OBJANIMNUMBER = 0;
                    }
                }
            }
        }
    }
}
