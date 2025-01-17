﻿using Emerald.Core;
using Emerald.Utilities.Wpf2Sdl; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/// <summary>
/// 
/// IGameObjects/GameObjectLayoutLoader.cs
/// 
/// Created: 2020-06-23
/// 
/// Modified: 2020-07-14
/// 
/// Version: 1.70 (added debug logging: 2020-07-14)
/// 
/// Purpose: Handles IGameObject loading for SDL-based free!. 
/// 
/// </summary>


namespace Free
{
    partial class Level
    {
        public bool LoadLevelObjects(List<IGameObject> listOfIGameObjects, Level currentlevel) // loads an IGameObject layout.
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                // debug this tmrw (2020-06-30)
                XmlDocument.Load(this.LevelIGameObjectsPATH);
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "ObjectLayout")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the IGameObjects node.
                int currentIntId = 0;

                // WORKAROUND for weird bug (RETARDED CODE FIX 2020-06-23 21:43)

                // temp
                FreeSDL MnWindow = SDL_Stage0_Init.SDLEngine; 

                foreach (XmlNode XmlNode in XmlNodes)
                {                        
                    if (XmlNode.Name != "#comment")
                    {
                        XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attribute out of each node. 
                        IGameObject GameObject2 = new GameObject();

                        int id = -1;
                        SDLPoint Position = new SDLPoint(-69, -420); 
                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name)
                            {
                                case "id":
                                case "ID":
                                case "objid":
                                    id = Convert.ToInt32(XmlAttribute.Value);
                                    continue;
                                case "PosX":
                                case "posx":
                                    Position.X = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "PosY":
                                case "posy":
                                    Position.Y = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                            }
                        }

                        if (Position.X == -69 && Position.Y == -420)
                        {
                            Error.Throw(null, ErrorSeverity.FatalError, "Position not defined.", "Emerald Engine 3.0 (Error 97)", 97);
                        }
#if DEBUG
                        SDLDebug.LogDebug_C("On demand level loader", $"Placed object with global ID {id} and internal ID {currentIntId} @ {Position.X},{Position.Y}");
#endif
                        /*
                        if (IGameObject.GameObjectPLAYER)
                        {
                            if (currentlevel.PlayerStartPosition.X == 0 || currentlevel.PlayerStartPosition.Y == 0) // default
                            {
                                currentlevel.PlayerStartPosition = new Point(IGameObject.Position.X, IGameObject.Position.Y);
                            }
                            else
                            {
                                IGameObject.Position.X = currentlevel.PlayerStartPosition.X;
                                IGameObject.Position.Y = currentlevel.PlayerStartPosition.Y;
                            }
                        }

                        if (IGameObject.GameObjectANIMATIONS.Count == 0)
                        {
                            if (IGameObject.GameObjectIMAGE.CanFreeze) IGameObject.GameObjectIMAGE.Freeze();
                        }
                        */

                        // 2020-07-21 00:59 make this better

                        // Get current object
                        IGameObject IGO = MnWindow.GetGlobalObject(id);

                        // Load (SDL) (TEMP 64x64) 
                        MnWindow.SDLGame.CurrentScene.LoadImage(id, Position, 64, 64, MnWindow.currentlevel.LevelIGameObjects.Count - 1);

                        // Load (WPF)
                        MnWindow.AddIGameObject(id, Position);


                        currentIntId++;
                    }
                    
                }
            }
            catch (XmlException err)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"A critical error occurred while loading {this.LevelIGameObjectsPATH}:\n\n:{err}", "Emerald Engine (E98)", 98);
            }
            catch (FormatException err)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"A critical error occurred while loading { this.LevelIGameObjectsPATH} (Critical level data file malformed!):\n\n{err}", "Emerald Engine (E99)", 99);
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent IGameObject layout, or error loading an level object layout because a critical level data file was not found. This is most likely because the a level-changing object attempted to trigger its interaction or run its OnHit SimpleESX script," +
                    " but the next level by ID doesn't have an IGameObject layout or it failed to load.", "Emerald Engine (E8)", 8);
            }



            return true;
        }
    }
}
