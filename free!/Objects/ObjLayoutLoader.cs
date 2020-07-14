using Emerald.Utilities.Wpf2Sdl; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Shapes;

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
                FreeSDL MnWindow = (FreeSDL)Application.Current.MainWindow; 

                foreach (XmlNode XmlNode in XmlNodes)
                {                        
                    if (XmlNode.Name != "#comment")
                    {
                        XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attribute out of each node. 
                        IGameObject GameObject2 = new GameObject(); 

                        foreach (GameObject IGameObject in listOfIGameObjects) // yikes. 
                        {
                            // 2020-05-26: This code is literally horrific. Jesus fucking christ.
                            // 2020-07-14: Get rid of this code once and for all. Still kinda bad but 

                            /*
                            if (IGameObject.GameObjectID == Convert.ToInt32(XmlNode.Attributes[0].Value))
                            {
                                if (!FreeSDL.IsSentientBeing(IGameObject))
                                {
                                    Position.X.GameObjectANIMATIONS = IGameObject.GameObjectANIMATIONS;
                                    Position.X.CollidedLevelObjects = new List<IGameObject>(); // yeah.
                                    Position.X.GameObjectINTERNALID = currentIntId;
                                    Position.X.GameObjectID = IGameObject.GameObjectID;
                                    Position.X.GameObjectIMAGE = IGameObject.GameObjectIMAGE;
                                    Position.X.GameObjectIMAGEPATH = IGameObject.GameObjectIMAGEPATH;
                                    Position.X.GameObjectNAME = IGameObject.GameObjectNAME;
                                    Position.X.GameObjectGRAV = IGameObject.GameObjectGRAV;
                                    Position.X.GameObjectACCELERATION = IGameObject.GameObjectACCELERATION;
                                    Position.X.GameObjectPLAYER = IGameObject.GameObjectPLAYER;
                                    Position.X.GameObjectPLAYERDAMAGE = IGameObject.GameObjectPLAYERDAMAGE;
                                    Position.X.GameObjectPLAYERHEALTH = IGameObject.GameObjectPLAYERHEALTH;
                                    Position.X.GameObjectPLAYERLEVEL = IGameObject.GameObjectPLAYERLEVEL;
                                    Position.X.GameObjectPLAYERLIVES = IGameObject.GameObjectPLAYERLIVES;
                                    Position.X.GameObjectFORCE = IGameObject.GameObjectFORCE;
                                    Position.X.GameObjectHITBOX = IGameObject.GameObjectHITBOX;
                                    Position.X.GameObjectMASS = IGameObject.GameObjectMASS;
                                    Position.X.GameObjectPRIORITY = IGameObject.GameObjectPRIORITY;
                                    Position.X.GameObjectCANCOLLIDE = IGameObject.GameObjectCANCOLLIDE;
                                    Position.X.GameObjectCANSNAP = IGameObject.GameObjectCANSNAP;
                                    Position.X.GameObjectAI = IGameObject.GameObjectAI;
                                    Position.X.GameObjectCANMOVELEFT = true;
                                    Position.X.GameObjectCANMOVERIGHT = true;
                                    Position.X.GameObjectCONSTANTANIMNUMBER = 0;
                                }
                                else
                                {
                                    Position.X = new SentientBeing(IGameObject, IGameObject.GameObjectPLAYER, IGameObject.GameObjectPLAYERDAMAGE, IGameObject.GameObjectPLAYERHEALTH, IGameObject.GameObjectPLAYERLEVEL, IGameObject.GameObjectPLAYERLEVELDAMAGE, IGameObject.GameObjectPLAYERLIVES, currentIntId);
                                }
                                // This is real shit code, like yandere simulator shit, and it is, in fact, an ugly hack. Damn reference types...

                            }
                            */

                            foreach (XmlAttribute XmlAttribute in XmlAttributes)
                            {
                                switch (XmlAttribute.Name)
                                {
                                    case "PosX":
                                    case "posx":
                                        IGameObject.Position.X = Convert.ToDouble(XmlAttribute.Value);
                                        continue;
                                    case "PosY":
                                    case "posy":
                                        IGameObject.Position.Y = Convert.ToDouble(XmlAttribute.Value);
                                        continue;
                                }
                            }

#if DEBUG
                            FreeSDL.LogDebug_C("On demand level loader", $"Placed object with global ID {IGameObject.GameObjectID} and internal ID {IGameObject.GameObjectINTERNALID} @ {IGameObject.Position.X},{IGameObject.Position.Y}");
#endif
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

                            // Send to SDL for rendering.
                            App AppSDL = (App)Application.Current;

                            // Here we temporarily use WPF.variables. THIS IS EXTREMELY TEMPORARY CODE WE WILL HAVE OUR OWN SDLLOAD METHOD OK
                            if (!AppSDL.SDLGame.CurrentScene.LoadImage(IGameObject.GameObjectIMAGEPATH, new SDLPoint(IGameObject.Position.X, IGameObject.Position.Y), IGameObject.GameObjectIMAGE.PixelWidth, IGameObject.GameObjectIMAGE.PixelHeight))
                            {
                                Error.Throw(null, ErrorSeverity.FatalError, "Fatal error loading image", "avant-garde engine", 93);
                            }

                            currentlevel.LevelIGameObjects.Add(IGameObject);
                        }
                        



                        currentIntId++;
                    }
                    
                }
            }
            catch (XmlException err)
            {
                MessageBox.Show($"A critical error occurred while loading {this.LevelIGameObjectsPATH}:\n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0x6666DEAD);
            }
            catch (FormatException err)
            {
                MessageBox.Show($"A critical error occurred while loading {this.LevelIGameObjectsPATH}:\n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0x6667DEAD);
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent IGameObject layout, or error loading an IGameObject layout. This is most likely because the Goal IGameObject attempted to trigger its interaction but the next level by ID doesn't have an IGameObject layout or it failed to load.", "avant-gardé engine", 8);
                Environment.Exit(0x6668DEAD);
            }



            return true;
        }
    }
}
