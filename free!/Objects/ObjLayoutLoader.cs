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

namespace Free
{
    partial class Level
    {
        public bool LoadObjLayout(List<IGameObject> listOfObjects, Level currentlevel) // loads an object layout.
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load(this.OBJLAYOUTPATH);
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "ObjectLayout")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the Objects node.
                int currentIntId = 0;
                
                // WORKAROUND for weird bug
                
                foreach (XmlNode XmlNode in XmlNodes)
                {                        
                    if (XmlNode.Name != "#comment")
                    {
                        XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attribute out of each node. 
                        IGameObject Objx = new Obj(); 

                        foreach (Obj Object in listOfObjects) // yikes. 
                        {
                            // 2020-05-26: This code is literally horrific. Jesus fucking christ.
                            if (Object.OBJID == Convert.ToInt32(XmlNode.Attributes[0].Value))
                            {
                                if (!MainWindow.IsSentientBeing(Object))
                                {
                                    Objx.OBJANIMATIONS = Object.OBJANIMATIONS;
                                    Objx.OBJCOLLIDEDOBJECTS = new List<IGameObject>(); // yeah.
                                    Objx.OBJINTERNALID = currentIntId;
                                    Objx.OBJID = Object.OBJID;
                                    Objx.OBJIMAGE = Object.OBJIMAGE;
                                    Objx.OBJIMAGEPATH = Object.OBJIMAGEPATH;
                                    Objx.OBJNAME = Object.OBJNAME;
                                    Objx.OBJGRAV = Object.OBJGRAV;
                                    Objx.OBJACCELERATION = Object.OBJACCELERATION;
                                    Objx.OBJPLAYER = Object.OBJPLAYER;
                                    Objx.OBJPLAYERDAMAGE = Object.OBJPLAYERDAMAGE;
                                    Objx.OBJPLAYERHEALTH = Object.OBJPLAYERHEALTH;
                                    Objx.OBJPLAYERLEVEL = Object.OBJPLAYERLEVEL;
                                    Objx.OBJPLAYERLIVES = Object.OBJPLAYERLIVES;
                                    Objx.OBJFORCE = Object.OBJFORCE;
                                    Objx.OBJHITBOX = Object.OBJHITBOX;
                                    Objx.OBJMASS = Object.OBJMASS;
                                    Objx.OBJPRIORITY = Object.OBJPRIORITY;
                                    Objx.OBJCANCOLLIDE = Object.OBJCANCOLLIDE;
                                    Objx.OBJCANSNAP = Object.OBJCANSNAP;
                                    Objx.OBJAI = Object.OBJAI;
                                    Objx.OBJCANMOVELEFT = true;
                                    Objx.OBJCANMOVERIGHT = true;
                                    Objx.OBJCONSTANTANIMNUMBER = 0;
                                }
                                else
                                {
                                    Objx = new SentientBeing(Object, Object.OBJPLAYER, Object.OBJPLAYERDAMAGE, Object.OBJPLAYERHEALTH, Object.OBJPLAYERLEVEL, Object.OBJPLAYERLEVELDAMAGE, Object.OBJPLAYERLIVES, currentIntId);

                                }
                                // This is real shit code, like yandere simulator shit, and it is, in fact, an ugly hack. Damn reference types...

                            }
                        }
                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name)
                            {
                                case "posx":
                                    Objx.OBJX = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "posy":
                                    Objx.OBJY = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                            }
                        }

                        if (Objx.OBJPLAYER)
                        {
                            if (currentlevel.PlayerStartPosition.X == 0 || currentlevel.PlayerStartPosition.Y == 0) // default
                            {
                                currentlevel.PlayerStartPosition = new Point(Objx.OBJX, Objx.OBJY);
                            }
                            else
                            {
                                Objx.OBJX = currentlevel.PlayerStartPosition.X;
                                Objx.OBJY = currentlevel.PlayerStartPosition.Y;
                            }
                        }


                        

                        if (Objx.OBJANIMATIONS.Count == 0)
                        {
                            if (Objx.OBJIMAGE.CanFreeze) Objx.OBJIMAGE.Freeze();
                        }

                        currentlevel.OBJLAYOUT.Add(Objx);
                        currentIntId++;
                    }
                    
                }
            }
            catch (XmlException err)
            {
                MessageBox.Show($"A critical error occurred while loading {this.OBJLAYOUTPATH}:\n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6666);
            }
            catch (FormatException err)
            {
                MessageBox.Show($"A critical error occurred while loading {this.OBJLAYOUTPATH}:\n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6666);
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent object layout, or error loading an object layout. This is most likely because the Goal object attempted to trigger its interaction but the next level by ID doesn't have an object layout or it failed to load.", "avant-gardé engine", 8);
            }
            return true;
        }
    }
}
