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
    partial class FreeSDL
    {
        public void HandleCollision(IGameObject GameObject)
        {
            Rect GameObjectPos = new Rect();
            Rect GameObjectxPos = new Rect();

            //player death (move this code!!!)
            if (GameObject.GameObjectY > currentlevel.PLRKILLY && currentlevel.PLRKILLY != null && IsSentientBeing(GameObject))
            {
                //move trincid and other SentientBeings to their starting positions
                GameObject.GameObjectX = currentlevel.PlayerStartPosition.X;
                GameObject.GameObjectY = currentlevel.PlayerStartPosition.Y;
                GameObject.GameObjectPLAYERLIVES -= 1;
            }

            if (GameObject.GameObjectHITBOX.Count == 0)
            {
                GameObjectPos = new Rect(new Point(GameObject.GameObjectX, GameObject.GameObjectY), new Point(GameObject.GameObjectX + GameObject.GameObjectIMAGE.PixelWidth, GameObject.GameObjectY + GameObject.GameObjectIMAGE.PixelHeight)); // default hitbox - image size
            }
            else
            {
                GameObjectPos = new Rect(new Point(GameObject.GameObjectX + GameObject.GameObjectHITBOX[0].X, GameObject.GameObjectY + GameObject.GameObjectHITBOX[0].Y), new Point(GameObject.GameObjectX + GameObject.GameObjectHITBOX[1].X, GameObject.GameObjectY + GameObject.GameObjectHITBOX[1].Y)); // get the hitbox
            }
            //TODO: CONVERT TO
            for (int i = 0; i < currentlevel.LevelIGameObjects.Count; i++)
            {
                IGameObject GameObjectx = currentlevel.LevelIGameObjects[i];

                if (GameObject.GameObjectINTERNALID != GameObjectx.GameObjectINTERNALID)
                {
                    if (GameObjectx.GameObjectHITBOX.Count == 0)
                    {
                        GameObjectxPos = new Rect(new Point(GameObjectx.GameObjectX, GameObjectx.GameObjectY), new Point(GameObjectx.GameObjectX + GameObjectx.GameObjectIMAGE.PixelWidth, GameObjectx.GameObjectY + GameObjectx.GameObjectIMAGE.PixelHeight));
                    }
                    else
                    {
                        GameObjectxPos = new Rect(new Point(GameObjectx.GameObjectX + GameObjectx.GameObjectHITBOX[0].X, GameObjectx.GameObjectY + GameObjectx.GameObjectHITBOX[0].Y), new Point(GameObjectx.GameObjectX + GameObjectx.GameObjectHITBOX[1].X, GameObjectx.GameObjectY + GameObjectx.GameObjectHITBOX[1].Y));
                    }

                    // What the fuck was I on?

                    if (GameObject.GameObjectGRAV && GameObject.GameObjectCOLLISIONS > 0 & !GameObject.GameObjectISJUMPING) // bld 932: don't accidentally assign GameObjectgrav to false!
                    {
                        GameObject.GameObjectSPEEDY = 0;
                        GameObject.GameObjectACCELERATIONY = 0;
                        GameObject.GameObjectDECELERATIONY = 0;
                    }

                    if (!GameObject.CollidedLevelObjects.Contains(GameObjectx))
                    {
                        if (GameObjectPos.IntersectsWith(GameObjectxPos))
                        {
                            GameObject.GameObjectCOLLISIONS++;
                            GameObject.CollidedLevelObjects.Add(GameObjectx);
                            for (int j = 0; j < InteractionList.Count; j++) // interactions
                            {
                                Interaction interaction = InteractionList[j];

                                if (GameObject.GameObjectID == interaction.GameObject1ID && GameObjectx.GameObjectID == interaction.GameObject2ID)
                                {
                                    switch (interaction.GameObjectINTERACTIONTYPE)
                                    {
                                        case InteractionType.Bounce:
                                            Interaction_Bounce(GameObject);
                                            GameObject.GameObjectCOLLISIONS--;
                                            GameObject.CollidedLevelObjects.Remove(GameObjectx);
                                            continue;
                                        case InteractionType.BounceLeft:
                                            Interaction_BounceLeft(GameObject);
                                            GameObject.GameObjectCOLLISIONS--;
                                            GameObject.CollidedLevelObjects.Remove(GameObjectx);
                                            continue;
                                        case InteractionType.BounceRight:
                                            Interaction_BounceRight(GameObject);
                                            GameObject.GameObjectCOLLISIONS--;
                                            GameObject.CollidedLevelObjects.Remove(GameObjectx);
                                            continue;
                                        case InteractionType.ChangeLevel:
                                            Interaction_ChangeLevel();
                                            continue;
                                        case InteractionType.Hurt:
                                            Interaction_Hurt(GameObjectx, GameObject.GameObjectPLAYERDAMAGE);
                                            continue;
                                        case InteractionType.Kill:
                                            GameObject = Interaction_Kill(GameObjectx);
                                            continue;
                                        case InteractionType.Remove:
                                            // temp
                                            Interaction_Remove(GameObjectx, interaction);
                                            GameObject.GameObjectACCELERATIONY += 0.4; //REMOVE HACK
                                            continue;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GameObjectx.GameObjectHITBOX.Count == 0)
                        {                            
                            if (GameObject.GameObjectY - GameObjectx.GameObjectY > 1 & GameObject.GameObjectY - GameObjectx.GameObjectY < GameObjectx.GameObjectIMAGE.PixelHeight & IsSentientBeing(GameObject))
                            {
                                switch (GameObjectx.GameObjectCANSNAP)
                                {
                                    case true:
                                        GameObject.GameObjectY -= GameObject.GameObjectY - GameObjectx.GameObjectY;
                                        break;
                                    case false:
                                        GameObject.GameObjectY += 10;
                                        GameObject.GameObjectCOLLISIONS--;
                                        GameObject.CollidedLevelObjects.Remove(GameObjectx);
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
                            if (GameObject.GameObjectY - GameObjectx.GameObjectY > 1 & GameObject.GameObjectY - GameObjectx.GameObjectY < GameObjectx.GameObjectY + GameObjectx.GameObjectHITBOX[1].Y & IsSentientBeing(GameObject))
                            {
                                switch (GameObjectx.GameObjectCANSNAP)
                                {
                                    case true:
                                        GameObject.GameObjectY -= GameObjectx.GameObjectHITBOX[1].Y; //chg to amount in IGameObject.
                                        break;
                                    case false:
                                        GameObject.GameObjectY += GameObjectx.GameObjectIMAGE.PixelHeight - GameObjectx.GameObjectHITBOX[1].Y;
                                        GameObject.GameObjectCOLLISIONS--;
                                        GameObject.CollidedLevelObjects.Remove(GameObjectx);

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
                            if (GameObject.GameObjectX - GameObjectx.GameObjectX < 0 & GameObject.GameObjectX - GameObjectx.GameObjectX > -GameObject.GameObjectIMAGE.PixelWidth & GameObject.GameObjectY - GameObjectx.GameObjectY < 0 & GameObject.GameObjectY - GameObjectx.GameObjectY > -GameObject.GameObjectIMAGE.PixelHeight / 1.6 & IsSentientBeing(GameObject))
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
                                    GameObject.CollidedLevelObjects.Remove(GameObjectx);
                                    //GameObject.GameObjectCANMOVERIGHT = false;
                                }
                            }
                            else if (GameObject.GameObjectX - GameObjectx.GameObjectX > GameObject.GameObjectIMAGE.PixelWidth & GameObject.GameObjectX - GameObjectx.GameObjectX < GameObject.GameObjectIMAGE.PixelWidth * 2 & GameObject.GameObjectY > 0 & GameObject.GameObjectY - GameObjectx.GameObjectY < GameObject.GameObjectIMAGE.PixelHeight / 1.6 & IsSentientBeing(GameObject))
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
                                    GameObject.CollidedLevelObjects.Remove(GameObjectx);
                                    //GameObject.GameObjectCANMOVERIGHT = false;
                                }
                            }
                        }
                        else
                        {
                            if (GameObject.GameObjectX - GameObjectx.GameObjectX < GameObject.GameObjectHITBOX[0].X & GameObject.GameObjectX - GameObjectx.GameObjectX > -GameObject.GameObjectHITBOX[1].X & GameObject.GameObjectY - GameObjectx.GameObjectY < GameObject.GameObjectHITBOX[0].Y & GameObject.GameObjectY - GameObjectx.GameObjectY > -GameObject.GameObjectHITBOX[1].Y/1.6 & IsSentientBeing(GameObject))
                            {
                                if (GameObject.GameObjectSPEED > 0)
                                {
                                    GameObject.GameObjectSPEED = -GameObject.GameObjectSPEED;
                                    GameObject.GameObjectACCELERATION = 0;
                                    GameObject.GameObjectCOLLISIONS--;
                                    GameObject.CollidedLevelObjects.Remove(GameObjectx);
                                    //GameObject.GameObjectCANMOVERIGHT = false;
                                }

                            }
                            else if (GameObject.GameObjectX - GameObjectx.GameObjectX > GameObject.GameObjectHITBOX[1].X & GameObject.GameObjectX - GameObjectx.GameObjectX < GameObject.GameObjectHITBOX[1].X * 2 & GameObject.GameObjectY > GameObject.GameObjectHITBOX[0].Y & GameObject.GameObjectY - GameObjectx.GameObjectY < GameObject.GameObjectHITBOX[1].Y * 1.6 & IsSentientBeing(GameObject))
                            {
                                if (GameObject.GameObjectSPEED < 0)
                                {
                                    GameObject.GameObjectSPEED = -GameObject.GameObjectSPEED;
                                    GameObject.GameObjectACCELERATION = 0;
                                    GameObject.GameObjectCOLLISIONS--;
                                    GameObject.CollidedLevelObjects.Remove(GameObjectx);
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

                                    if (rect.IntersectsWith(GameObjectxPos))
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
                IGameObject GameObjectx = GameObject.CollidedLevelObjects[i];
                if (GameObject.GameObjectINTERNALID != GameObjectx.GameObjectINTERNALID)
                {
                    if (GameObjectx.GameObjectHITBOX.Count == 0)
                    {
                        GameObjectxPos = new Rect(new Point(GameObjectx.GameObjectX, GameObjectx.GameObjectY), new Point(GameObjectx.GameObjectX + GameObjectx.GameObjectIMAGE.PixelWidth, GameObjectx.GameObjectY + GameObjectx.GameObjectIMAGE.PixelHeight));
                    }
                    else
                    {
                        GameObjectxPos = new Rect(new Point(GameObjectx.GameObjectX + GameObjectx.GameObjectHITBOX[0].X, GameObjectx.GameObjectY + GameObjectx.GameObjectHITBOX[0].Y), new Point(GameObjectx.GameObjectX + GameObjectx.GameObjectHITBOX[1].X, GameObjectx.GameObjectY + GameObjectx.GameObjectHITBOX[1].Y)); // base this on the hitbox if its not null
                    }
                    if (!GameObjectPos.IntersectsWith(GameObjectxPos))
                    {
                        GameObject.GameObjectCOLLISIONS--;
                        GameObject.CollidedLevelObjects.Remove(GameObjectx); // remove the IGameObject
                    }
                }
            }
        }

        private List<Rect> Internal_GetIGameObjectBoundingBoxes(GameObject GameObject1, GameObject GameObject2)
        {
            // fix bad code - GameObject.Size (point) 
            List<Rect> Rects = new List<Rect>();
            Rects.Add(new Rect(new Point(GameObject1.GameObjectX, GameObject1.GameObjectY), new Point(GameObject1.GameObjectX + GameObject1.GameObjectIMAGE.PixelWidth, GameObject1.GameObjectY + GameObject1.GameObjectIMAGE.PixelHeight)));
            Rects.Add(new Rect(new Point(GameObject2.GameObjectX, GameObject2.GameObjectY), new Point(GameObject2.GameObjectX + GameObject2.GameObjectIMAGE.PixelWidth, GameObject2.GameObjectY + GameObject2.GameObjectIMAGE.PixelHeight)));

            return Rects;
        }

        /// <summary>
        /// Tests if the top of GameObject1's bounding box collides with 2.
        /// </summary>
        public bool TestCollideTop(GameObject GameObject1, GameObject GameObject2)
        {
            List<Rect> Rects = Internal_GetIGameObjectBoundingBoxes(GameObject1, GameObject2);

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

        public bool TestCollideBottom(GameObject GameObject1, GameObject GameObject2)
        {
            List<Rect> Rects = Internal_GetIGameObjectBoundingBoxes(GameObject1, GameObject2);

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

        public bool TestCollideLeft(GameObject GameObject1, GameObject GameObject2)
        {
            List<Rect> Rects = Internal_GetIGameObjectBoundingBoxes(GameObject1, GameObject2);

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

        public bool TestCollideRight(GameObject GameObject1, GameObject GameObject2)
        {
            List<Rect> Rects = Internal_GetIGameObjectBoundingBoxes(GameObject1, GameObject2);

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
