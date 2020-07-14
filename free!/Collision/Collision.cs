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
/// Modified: 2020-06-29
/// 
/// Version: 3.10
/// 
/// Purpose: Handles collisions and manages interactions. Split off from Physics.cs 2019-11-20. 
/// 
/// This is a giant-ass hack.
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        public void HandleCollision(IGameObject GameObject)
        {
            Rect GameObjectPos = new Rect();
            Rect GameObject2Pos = new Rect();

            //player death (move this code!!!)
            if (GameObject.Position.Y > currentlevel.PLRKILLY && currentlevel.PLRKILLY != null && IsSentientBeing(GameObject))
            {
                //move trincid and other SentientBeings to their starting positions
                GameObject.Position.X = currentlevel.PlayerStartPosition.X;
                GameObject.Position.Y = currentlevel.PlayerStartPosition.Y;
                GameObject.GameObjectPLAYERLIVES -= 1;
            }

            if (GameObject.GameObjectHITBOX.Count == 0)
            {
                GameObjectPos = new Rect(new Point(GameObject.Position.X, GameObject.Position.Y), new Point(GameObject.Position.X + GameObject.GameObjectIMAGE.PixelWidth, GameObject.Position.Y + GameObject.GameObjectIMAGE.PixelHeight)); // default hitbox - image size
            }
            else
            {
                GameObjectPos = new Rect(new Point(GameObject.Position.X + GameObject.GameObjectHITBOX[0].X, GameObject.Position.Y + GameObject.GameObjectHITBOX[0].Y), new Point(GameObject.Position.X + GameObject.GameObjectHITBOX[1].X, GameObject.Position.Y + GameObject.GameObjectHITBOX[1].Y)); // get the hitbox
            }
            //TODO: CONVERT TO
            for (int i = 0; i < currentlevel.LevelIGameObjects.Count; i++)
            {
                IGameObject GameObject2 = currentlevel.LevelIGameObjects[i];

                if (GameObject.GameObjectINTERNALID != GameObject2.GameObjectINTERNALID)
                {
                    if (GameObject2.GameObjectHITBOX.Count == 0)
                    {
                        GameObject2Pos = new Rect(new Point(GameObject2.Position.X, GameObject2.Position.Y), new Point(GameObject2.Position.X + GameObject2.GameObjectIMAGE.PixelWidth, GameObject2.Position.Y + GameObject2.GameObjectIMAGE.PixelHeight));
                    }
                    else
                    {
                        GameObject2Pos = new Rect(new Point(GameObject2.Position.X + GameObject2.GameObjectHITBOX[0].X, GameObject2.Position.Y + GameObject2.GameObjectHITBOX[0].Y), new Point(GameObject2.Position.X + GameObject2.GameObjectHITBOX[1].X, GameObject2.Position.Y + GameObject2.GameObjectHITBOX[1].Y));
                    }

                    // What the fuck was I on?

                    if (GameObject.GameObjectGRAV && GameObject.GameObjectCOLLISIONS > 0 & !GameObject.GameObjectISJUMPING) // bld 932: don't accidentally assign GameObjectgrav to false!
                    {
                        GameObject.GameObjectSPEEDY = 0;
                        GameObject.GameObjectACCELERATIONY = 0;
                        GameObject.GameObjectDECELERATIONY = 0;
                    }

                    if (!GameObject.CollidedLevelObjects.Contains(GameObject2))
                    {
                        if (GameObjectPos.IntersectsWith(GameObject2Pos))
                        {
                            GameObject.GameObjectCOLLISIONS++;
                            GameObject.CollidedLevelObjects.Add(GameObject2);
                            for (int j = 0; j < InteractionList.Count; j++) // interactions
                            {
                                Interaction interaction = InteractionList[j];

                                if (GameObject.GameObjectID == interaction.ObjId1ID && GameObject2.GameObjectID == interaction.ObjId2ID)
                                {
                                    switch (interaction.GameObjectINTERACTIONTYPE)
                                    {
                                        case InteractionType.Bounce:
                                            Interaction_Bounce(GameObject);
                                            GameObject.GameObjectCOLLISIONS--;
                                            GameObject.CollidedLevelObjects.Remove(GameObject2);
                                            continue;
                                        case InteractionType.BounceLeft:
                                            Interaction_BounceLeft(GameObject);
                                            GameObject.GameObjectCOLLISIONS--;
                                            GameObject.CollidedLevelObjects.Remove(GameObject2);
                                            continue;
                                        case InteractionType.BounceRight:
                                            Interaction_BounceRight(GameObject);
                                            GameObject.GameObjectCOLLISIONS--;
                                            GameObject.CollidedLevelObjects.Remove(GameObject2);
                                            continue;
                                        case InteractionType.ChangeLevel:
                                            Interaction_ChangeLevel();
                                            continue;
                                        case InteractionType.Hurt:
                                            Interaction_Hurt(GameObject2, GameObject.GameObjectPLAYERDAMAGE);
                                            continue;
                                        case InteractionType.Kill:
                                            GameObject = Interaction_Kill(GameObject2);
                                            continue;
                                        case InteractionType.Remove:
                                            // temp
                                            Interaction_Remove(GameObject2, interaction);
                                            GameObject.GameObjectACCELERATIONY += 0.4; //REMOVE HACK
                                            continue;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GameObject2.GameObjectHITBOX.Count == 0)
                        {                            
                            if (GameObject.Position.Y - GameObject2.Position.Y > 1 & GameObject.Position.Y - GameObject2.Position.Y < GameObject2.GameObjectIMAGE.PixelHeight && IsSentientBeing(GameObject))
                            {
                                switch (GameObject2.GameObjectCANSNAP)
                                {
                                    case true:
                                        GameObject.Position.Y -= GameObject.Position.Y - GameObject2.Position.Y;
                                        break;
                                    case false:
                                        GameObject.Position.Y += 10;
                                        GameObject.GameObjectCOLLISIONS--;
                                        GameObject.CollidedLevelObjects.Remove(GameObject2);
                                        //oops (bld 925)

                                        if (GameObject.GameObjectGRAV)
                                        {
                                           GameObject.GameObjectACCELERATIONY = -GameObject.GameObjectACCELERATIONY * 3;
                                        }

                                        return;
                                }

                            }
                        }
                        else
                        {
                            if (GameObject.Position.Y - GameObject2.Position.Y > 1 & GameObject.Position.Y - GameObject2.Position.Y < GameObject2.Position.Y + GameObject2.GameObjectHITBOX[1].Y & IsSentientBeing(GameObject))
                            {
                                switch (GameObject2.GameObjectCANSNAP)
                                {
                                    case true:
                                        GameObject.Position.Y -= GameObject2.GameObjectHITBOX[1].Y; //chg to amount in IGameObject.
                                        break;
                                    case false:
                                        GameObject.Position.Y += GameObject2.GameObjectIMAGE.PixelHeight - GameObject2.GameObjectHITBOX[1].Y;
                                        GameObject.GameObjectCOLLISIONS--;
                                        GameObject.CollidedLevelObjects.Remove(GameObject2);

                                        if (GameObject.GameObjectGRAV)
                                        {
                                            GameObject.GameObjectACCELERATIONY = -GameObject.GameObjectACCELERATIONY * 2;
                                        }

                                        return;
                                }
                            }
                        }


                        if (GameObject.GameObjectHITBOX.Count == 0)
                        {
                            if (GameObject.Position.X - GameObject2.Position.X < 0 
                                && GameObject.Position.X - GameObject2.Position.X > -GameObject.GameObjectIMAGE.PixelWidth 
                                && GameObject.Position.Y - GameObject2.Position.Y < 0 
                                && GameObject.Position.Y - GameObject2.Position.Y > -GameObject.GameObjectIMAGE.PixelHeight / 1.6 & IsSentientBeing(GameObject))
                            {
                                if (GameObject.GameObjectSPEED > 0)
                                {
                                    GameObject.GameObjectSPEED = -GameObject.GameObjectSPEED;

                                    if (GameObject.GameObjectSPEEDY < 0)
                                    {
                                        GameObject.GameObjectSPEEDY = -GameObject.GameObjectSPEEDY;
                                    }

                                    GameObject.GameObjectACCELERATION = 0;
                                    GameObject.GameObjectCOLLISIONS--;
                                    GameObject.CollidedLevelObjects.Remove(GameObject2);
                                    //GameObject.GameObjectCANMOVERIGHT = false;
                                }
                            }
                            else if (GameObject.Position.X - GameObject2.Position.X > GameObject.GameObjectIMAGE.PixelWidth 
                                && GameObject.Position.X - GameObject2.Position.X < GameObject.GameObjectIMAGE.PixelWidth * 2 
                                && GameObject.Position.Y > 0 
                                && GameObject.Position.Y - GameObject2.Position.Y < GameObject.GameObjectIMAGE.PixelHeight / 1.6 & IsSentientBeing(GameObject))
                            {
                                if (GameObject.GameObjectSPEED < 0)
                                {
                                    GameObject.GameObjectSPEED = -GameObject.GameObjectSPEED;

                                    if (GameObject.GameObjectSPEEDY < 0)
                                    {
                                        GameObject.GameObjectSPEEDY = -GameObject.GameObjectSPEEDY;
                                    }

                                    GameObject.GameObjectACCELERATION = 0;
                                    GameObject.GameObjectCOLLISIONS--;
                                    GameObject.CollidedLevelObjects.Remove(GameObject2);
                                    //GameObject.GameObjectCANMOVERIGHT = false;
                                }
                            }
                        }
                        else
                        {
                            if (GameObject.Position.X - GameObject2.Position.X < GameObject.GameObjectHITBOX[0].X 
                                && GameObject.Position.X - GameObject2.Position.X > -GameObject.GameObjectHITBOX[1].X 
                                && GameObject.Position.Y - GameObject2.Position.Y < GameObject.GameObjectHITBOX[0].Y 
                                && GameObject.Position.Y - GameObject2.Position.Y > -GameObject.GameObjectHITBOX[1].Y/1.6 
                                && IsSentientBeing(GameObject))
                            {
                                if (GameObject.GameObjectSPEED > 0)
                                {
                                    GameObject.GameObjectSPEED = -GameObject.GameObjectSPEED;
                                    GameObject.GameObjectACCELERATION = 0;
                                    GameObject.GameObjectCOLLISIONS--;
                                    GameObject.CollidedLevelObjects.Remove(GameObject2);
                                    //GameObject.GameObjectCANMOVERIGHT = false;
                                }

                            }
                            else if (GameObject.Position.X - GameObject2.Position.X > GameObject.GameObjectHITBOX[1].X 
                                && GameObject.Position.X - GameObject2.Position.X < GameObject.GameObjectHITBOX[1].X * 2 
                                && GameObject.Position.Y > GameObject.GameObjectHITBOX[0].Y 
                                && GameObject.Position.Y - GameObject2.Position.Y < GameObject.GameObjectHITBOX[1].Y * 1.6 
                                && IsSentientBeing(GameObject))
                            {
                                if (GameObject.GameObjectSPEED < 0)
                                {
                                    GameObject.GameObjectSPEED = -GameObject.GameObjectSPEED;
                                    GameObject.GameObjectACCELERATION = 0;
                                    GameObject.GameObjectCOLLISIONS--;
                                    GameObject.CollidedLevelObjects.Remove(GameObject2);
                                }
                            }
                        }
                    }

                    //AMMO Annihilation.
                    if (GameObject.GameObjectHELDWEAPON != null)
                    {
                        // yeah
                        if (GameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.Count > 0)
                        {
                            for (int j = 0; j < GameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.Count; j++)
                            {
                                Ammo ammo = GameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST[j];

                                // Get a lock for the ammo. 
                                lock (ammo)
                                {
                                    Rect rect = new Rect(new Point(ammo.X, ammo.Y), new Point(ammo.X + GameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.PixelWidth, ammo.Y + GameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.PixelHeight));

                                    if (rect.IntersectsWith(GameObject2Pos))
                                    {
                                        //annihilate.
                                        PlayAmmoAnimation(GameObject.GameObjectHELDWEAPON, ammo, AnimationType.Hit);

                                        if (IsSentientBeing(GameObject)) //damage the player
                                        {
                                            //still considering having a level system.
                                            GameObject.GameObjectHEALTH -= 10; //TESTING PURPOSES ONLY.
                                        }

                                        ammo.SPEEDX = 0;
                                    }
                                }
                            } 
                        }
                    }
                }
            }

            for (int i = 0; i < GameObject.CollidedLevelObjects.Count; i++) // collided IGameObjects 
            {
                IGameObject GameObject2 = GameObject.CollidedLevelObjects[i];
                if (GameObject.GameObjectINTERNALID != GameObject2.GameObjectINTERNALID)
                {
                    if (GameObject2.GameObjectHITBOX.Count == 0)
                    {
                        GameObject2Pos = new Rect(new Point(GameObject2.Position.X, GameObject2.Position.Y), new Point(GameObject2.Position.X + GameObject2.GameObjectIMAGE.PixelWidth, GameObject2.Position.Y + GameObject2.GameObjectIMAGE.PixelHeight));
                    }
                    else
                    {
                        GameObject2Pos = new Rect(new Point(GameObject2.Position.X + GameObject2.GameObjectHITBOX[0].X, GameObject2.Position.Y + GameObject2.GameObjectHITBOX[0].Y), new Point(GameObject2.Position.X + GameObject2.GameObjectHITBOX[1].X, GameObject2.Position.Y + GameObject2.GameObjectHITBOX[1].Y)); // base this on the hitbox if its not null
                    }
                    if (!GameObjectPos.IntersectsWith(GameObject2Pos))
                    {
                        GameObject.GameObjectCOLLISIONS--;
                        GameObject.CollidedLevelObjects.Remove(GameObject2); // remove the IGameObject
                    }
                }
            }
        }

        private List<Rect> Internal_GetObjectBoundingBoxes(IGameObject ObjId1, IGameObject ObjId2)
        {
            // fix bad code - GameObject.Size (point) 
            List<Rect> Rects = new List<Rect>();

            // CHANGE TO SDLPOINT
            Rects.Add(new Rect(new Point(ObjId1.Position.X, ObjId1.Position.Y), new Point(ObjId1.Position.X + ObjId1.GameObjectIMAGE.PixelWidth, ObjId1.Position.Y + ObjId1.GameObjectIMAGE.PixelHeight)));
            Rects.Add(new Rect(new Point(ObjId2.Position.X, ObjId2.Position.Y), new Point(ObjId2.Position.X + ObjId2.GameObjectIMAGE.PixelWidth, ObjId2.Position.Y + ObjId2.GameObjectIMAGE.PixelHeight)));

            return Rects;
        }

        /// <summary>
        /// Tests if the top of ObjId1's bounding box collides with 2.
        /// </summary>
        public bool TestCollideTop(IGameObject ObjId1, IGameObject ObjId2)
        {
            List<Rect> Rects = Internal_GetObjectBoundingBoxes(ObjId1, ObjId2);

            // Gonna work ons cripting
            double FX = Rects[1].TopLeft.Y + ((Rects[1].BottomRight.Y - Rects[1].TopLeft.Y) / 2);

            if (Rects[0].TopLeft.Y >= Rects[1].TopLeft.Y && Rects[0].TopLeft.Y <= FX) 
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        public bool TestCollideBottom(IGameObject ObjId1, IGameObject ObjId2)
        {
            List<Rect> Rects = Internal_GetObjectBoundingBoxes(ObjId1, ObjId2);

            // Gonna work ons cripting
            double FX = Rects[1].TopLeft.Y + ((Rects[1].BottomRight.Y - Rects[1].TopLeft.Y) / 2);

            if (Rects[0].TopLeft.Y >= FX && Rects[0].TopLeft.Y <= Rects[1].Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TestCollideLeft(IGameObject ObjId1, IGameObject ObjId2)
        {
            List<Rect> Rects = Internal_GetObjectBoundingBoxes(ObjId1, ObjId2);

            double Max = Rects[1].TopLeft.X + ((Rects[1].TopRight.Y - Rects[1].TopLeft.Y) / 2);

            if (Rects[0].TopLeft.X >= Rects[1].TopLeft.X && Rects[0].TopLeft.X <= Max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TestCollideRight(IGameObject ObjId1, IGameObject ObjId2)
        {
            List<Rect> Rects = Internal_GetObjectBoundingBoxes(ObjId1, ObjId2);

            double Max = Rects[1].TopLeft.X + ((Rects[1].TopRight.Y - Rects[1].TopLeft.Y) / 2);

            if (Rects[0].TopLeft.X >= Rects[1].TopLeft.X + Max && Rects[0].TopLeft.X <= Rects[1].TopRight.X)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
