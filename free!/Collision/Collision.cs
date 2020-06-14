using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

/// <summary>
/// 
/// Collision.cs
/// 
/// Created: 2019-11-20
/// 
/// Modified: 2020-05-27
/// 
/// Free Version: 0.20+ (Engine version 2.20)
/// 
/// Version: 3.01
/// 
/// Purpose: Handles collisions and manages interactions. Split off from Physics.cs 2019-11-20. 
/// 
/// This is a giant-ass hack.
/// </summary>

namespace Free
{
    partial class MainWindow
    {
        public void HandleCollision(IGameObject obj)
        {
            Rect ObjPos = new Rect();
            Rect ObjxPos = new Rect();

            //player death (move this code!!!)
            if (obj.OBJY > currentlevel.PLRKILLY && currentlevel.PLRKILLY != null && IsSentientBeing(obj))
            {
                //move trincid and other SentientBeings to their starting positions
                obj.OBJX = currentlevel.PlayerStartPosition.X;
                obj.OBJY = currentlevel.PlayerStartPosition.Y;
                obj.OBJPLAYERLIVES -= 1;
            }

            if (obj.OBJHITBOX == null)
            {
                ObjPos = new Rect(new Point(obj.OBJX, obj.OBJY), new Point(obj.OBJX + obj.OBJIMAGE.PixelWidth, obj.OBJY + obj.OBJIMAGE.PixelHeight)); // default hitbox - image size
            }
            else
            {
                ObjPos = new Rect(new Point(obj.OBJX + obj.OBJHITBOX[0].X, obj.OBJY + obj.OBJHITBOX[0].Y), new Point(obj.OBJX + obj.OBJHITBOX[1].X, obj.OBJY + obj.OBJHITBOX[1].Y)); // get the hitbox
            }
            //TODO: CONVERT TO
            for (int i = 0; i < currentlevel.LevelObjects.Count; i++)
            {
                IGameObject objx = currentlevel.LevelObjects[i];

                if (obj.OBJINTERNALID != objx.OBJINTERNALID)
                {
                    if (objx.OBJHITBOX == null)
                    {
                        ObjxPos = new Rect(new Point(objx.OBJX, objx.OBJY), new Point(objx.OBJX + objx.OBJIMAGE.PixelWidth, objx.OBJY + objx.OBJIMAGE.PixelHeight));
                    }
                    else
                    {
                        ObjxPos = new Rect(new Point(objx.OBJX + objx.OBJHITBOX[0].X, objx.OBJY + objx.OBJHITBOX[0].Y), new Point(objx.OBJX + objx.OBJHITBOX[1].X, objx.OBJY + objx.OBJHITBOX[1].Y));
                    }

                    // What the fuck was I on?

                    if (obj.OBJGRAV & obj.OBJCOLLISIONS > 0 & !obj.OBJISJUMPING) // bld 932: don't accidentally assign objgrav to false!
                    {
                        obj.OBJSPEEDY = 0;
                        obj.OBJACCELERATIONY = 0;
                        obj.OBJDECELERATIONY = 0;
                    }

                    if (!obj.OBJCOLLIDEDOBJECTS.Contains(objx))
                    {
                        if (ObjPos.IntersectsWith(ObjxPos))
                        {
                            obj.OBJCOLLISIONS++;
                            obj.OBJCOLLIDEDOBJECTS.Add(objx);
                            for (int j = 0; j < InteractionList.Count; j++) // interactions
                            {
                                Interaction interaction = InteractionList[j];

                                if (obj.OBJID == interaction.OBJ1ID && objx.OBJID == interaction.OBJ2ID)
                                {
                                    switch (interaction.OBJINTERACTIONTYPE)
                                    {
                                        case InteractionType.Bounce:
                                            Interaction_Bounce(obj);
                                            obj.OBJCOLLISIONS--;
                                            obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                            continue;
                                        case InteractionType.BounceLeft:
                                            Interaction_BounceLeft(obj);
                                            obj.OBJCOLLISIONS--;
                                            obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                            continue;
                                        case InteractionType.BounceRight:
                                            Interaction_BounceRight(obj);
                                            obj.OBJCOLLISIONS--;
                                            obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                            continue;
                                        case InteractionType.ChangeLevel:
                                            Interaction_ChangeLevel();
                                            continue;
                                        case InteractionType.Hurt:
                                            Interaction_Hurt(objx, obj.OBJPLAYERDAMAGE);
                                            continue;
                                        case InteractionType.Kill:
                                            obj = Interaction_Kill(objx);
                                            continue;
                                        case InteractionType.Remove:
                                            // temp
                                            Interaction_Remove(objx, interaction);
                                            obj.OBJACCELERATIONY += 0.4; //REMOVE HACK
                                            continue;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (objx.OBJHITBOX == null)
                        {                            
                            if (obj.OBJY - objx.OBJY > 1 & obj.OBJY - objx.OBJY < objx.OBJIMAGE.PixelHeight & IsSentientBeing(obj))
                            {
                                switch (objx.OBJCANSNAP)
                                {
                                    case true:
                                        obj.OBJY -= obj.OBJY - objx.OBJY;
                                        break;
                                    case false:
                                        obj.OBJY += 10;
                                        obj.OBJCOLLISIONS--;
                                        obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                        //oops (bld 925)

                                        if (obj.OBJGRAV)
                                        {
                                           obj.OBJACCELERATIONY = -obj.OBJACCELERATIONY * 3;
                                        }

                                        return;
                                }

                            }
                        }
                        else
                        {
                            if (obj.OBJY - objx.OBJY > 1 & obj.OBJY - objx.OBJY < objx.OBJY + objx.OBJHITBOX[1].Y & IsSentientBeing(obj))
                            {
                                switch (objx.OBJCANSNAP)
                                {
                                    case true:
                                        obj.OBJY -= objx.OBJHITBOX[1].Y; //chg to amount in object.
                                        break;
                                    case false:
                                        obj.OBJY += objx.OBJIMAGE.PixelHeight - objx.OBJHITBOX[1].Y;
                                        obj.OBJCOLLISIONS--;
                                        obj.OBJCOLLIDEDOBJECTS.Remove(objx);

                                        if (obj.OBJGRAV)
                                        {
                                            obj.OBJACCELERATIONY = -obj.OBJACCELERATIONY * 2;
                                        }

                                        return;
                                }
                            }
                        }


                        if (obj.OBJHITBOX == null)
                        {
                            if (obj.OBJX - objx.OBJX < 0 & obj.OBJX - objx.OBJX > -obj.OBJIMAGE.PixelWidth & obj.OBJY - objx.OBJY < 0 & obj.OBJY - objx.OBJY > -obj.OBJIMAGE.PixelHeight / 1.6 & IsSentientBeing(obj))
                            {
                                if (obj.OBJSPEED > 0)
                                {
                                    obj.OBJSPEED = -obj.OBJSPEED;

                                    if (obj.OBJSPEEDY < 0)
                                    {
                                        obj.OBJSPEEDY = -obj.OBJSPEEDY;
                                    }

                                    obj.OBJACCELERATION = 0;
                                    obj.OBJCOLLISIONS--;
                                    obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                    //obj.OBJCANMOVERIGHT = false;
                                }
                            }
                            else if (obj.OBJX - objx.OBJX > obj.OBJIMAGE.PixelWidth & obj.OBJX - objx.OBJX < obj.OBJIMAGE.PixelWidth * 2 & obj.OBJY > 0 & obj.OBJY - objx.OBJY < obj.OBJIMAGE.PixelHeight / 1.6 & IsSentientBeing(obj))
                            {
                                if (obj.OBJSPEED < 0)
                                {
                                    obj.OBJSPEED = -obj.OBJSPEED;

                                    if (obj.OBJSPEEDY < 0)
                                    {
                                        obj.OBJSPEEDY = -obj.OBJSPEEDY;
                                    }

                                    obj.OBJACCELERATION = 0;
                                    obj.OBJCOLLISIONS--;
                                    obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                    //obj.OBJCANMOVERIGHT = false;
                                }
                            }
                        }
                        else
                        {
                            if (obj.OBJX - objx.OBJX < obj.OBJHITBOX[0].X & obj.OBJX - objx.OBJX > -obj.OBJHITBOX[1].X & obj.OBJY - objx.OBJY < obj.OBJHITBOX[0].Y & obj.OBJY - objx.OBJY > -obj.OBJHITBOX[1].Y/1.6 & IsSentientBeing(obj))
                            {
                                if (obj.OBJSPEED > 0)
                                {
                                    obj.OBJSPEED = -obj.OBJSPEED;
                                    obj.OBJACCELERATION = 0;
                                    obj.OBJCOLLISIONS--;
                                    obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                    //obj.OBJCANMOVERIGHT = false;
                                }

                            }
                            else if (obj.OBJX - objx.OBJX > obj.OBJHITBOX[1].X & obj.OBJX - objx.OBJX < obj.OBJHITBOX[1].X * 2 & obj.OBJY > obj.OBJHITBOX[0].Y & obj.OBJY - objx.OBJY < obj.OBJHITBOX[1].Y * 1.6 & IsSentientBeing(obj))
                            {
                                if (obj.OBJSPEED < 0)
                                {
                                    obj.OBJSPEED = -obj.OBJSPEED;
                                    obj.OBJACCELERATION = 0;
                                    obj.OBJCOLLISIONS--;
                                    obj.OBJCOLLIDEDOBJECTS.Remove(objx);
                                }
                            }
                        }
                    }

                    //AMMO Annihilation.
                    if (obj.OBJHELDWEAPON != null)
                    {
                        // yeah
                        if (obj.OBJHELDWEAPON.WEAPONAMMOLIST.Count > 0)
                        {
                            for (int j = 0; j < obj.OBJHELDWEAPON.WEAPONAMMOLIST.Count; j++)
                            {
                                Ammo ammo = obj.OBJHELDWEAPON.WEAPONAMMOLIST[j];

                                // Get a lock for the ammo. 
                                lock (ammo)
                                {
                                    Rect rect = new Rect(new Point(ammo.X, ammo.Y), new Point(ammo.X + obj.OBJHELDWEAPON.WEAPONIMAGEAMMO.PixelWidth, ammo.Y + obj.OBJHELDWEAPON.WEAPONIMAGEAMMO.PixelHeight));

                                    if (rect.IntersectsWith(ObjxPos))
                                    {
                                        //annihilate.
                                        PlayAmmoAnimation(obj.OBJHELDWEAPON, ammo, AnimationType.Hit);

                                        if (IsSentientBeing(obj)) //damage the player
                                        {
                                            //still considering having a level system.
                                            obj.OBJHEALTH -= 10; //TESTING PURPOSES ONLY.
                                        }

                                        ammo.SPEEDX = 0;
                                    }
                                }
                            } 
                        }
                    }
                }
            }

            for (int i = 0; i < obj.OBJCOLLIDEDOBJECTS.Count; i++) // collided objects 
            {
                IGameObject objx = obj.OBJCOLLIDEDOBJECTS[i];
                if (obj.OBJINTERNALID != objx.OBJINTERNALID)
                {
                    if (objx.OBJHITBOX == null)
                    {
                        ObjxPos = new Rect(new Point(objx.OBJX, objx.OBJY), new Point(objx.OBJX + objx.OBJIMAGE.PixelWidth, objx.OBJY + objx.OBJIMAGE.PixelHeight));
                    }
                    else
                    {
                        ObjxPos = new Rect(new Point(objx.OBJX + objx.OBJHITBOX[0].X, objx.OBJY + objx.OBJHITBOX[0].Y), new Point(objx.OBJX + objx.OBJHITBOX[1].X, objx.OBJY + objx.OBJHITBOX[1].Y)); // base this on the hitbox if its not null
                    }
                    if (!ObjPos.IntersectsWith(ObjxPos))
                    {
                        obj.OBJCOLLISIONS--;
                        obj.OBJCOLLIDEDOBJECTS.Remove(objx); // remove the object
                    }
                }
            }

        }

        /// <summary>
        /// Tests if the top of obj1's bounding box collides with 2.
        /// </summary>
        public bool TestCollideTop(Obj Obj1, Obj Obj2)
        {
            List<Rect> Rects = Internal_GetObjectBoundingBoxes(Obj1, Obj2);

            // Gonna work ons cripting
            double FX = Rects[1].TopLeft.Y + ((Rects[1].BottomRight.Y - Rects[1].TopLeft.Y) / 2);

            if (Rects[0].TopLeft.Y > Rects[1].TopLeft.Y && Rects[0].TopLeft.Y < Rects[1].TopLeft.Y + FX) 
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        public bool TestCollideBottom(Obj Obj1, Obj Obj2)
        {
            List<Rect> Rects = Internal_GetObjectBoundingBoxes(Obj1, Obj2);

            // Gonna work ons cripting
            double FX = Rects[1].TopLeft.Y + ((Rects[1].BottomRight.Y - Rects[1].TopLeft.Y) / 2);

            if (Rects[0].TopLeft.Y > Rects[1].TopLeft.Y + FX && Rects[0].TopLeft.Y < Rects[1].Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Rect> Internal_GetObjectBoundingBoxes(Obj Obj1, Obj Obj2)
        {
            // fix bad code - obj.Size (point) 
            List<Rect> Rects = new List<Rect>();
            Rects.Add(new Rect(new Point(Obj1.OBJX, Obj1.OBJY), new Point(Obj1.OBJX + Obj1.OBJIMAGE.PixelWidth, Obj1.OBJY + Obj1.OBJIMAGE.PixelHeight)));
            Rects.Add(new Rect(new Point(Obj2.OBJX, Obj2.OBJY), new Point(Obj2.OBJX + Obj2.OBJIMAGE.PixelWidth, Obj2.OBJY + Obj2.OBJIMAGE.PixelHeight)));

            return Rects;
        }
    }
}
