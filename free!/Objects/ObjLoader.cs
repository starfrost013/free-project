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
    partial class MainWindow
    {
        // implement in Object class? (0.06)
        public void LoadObjects()
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load("Objects.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Objects")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the Objects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    IObject obj = new Obj();
                    // initial values
                    //obj.OBJGRAV = false;
                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attributes (we dont need to care what the node is called, we just need the right information)

                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name)
                            {
                                case "id":
                                    obj.OBJID = Convert.ToInt32(XmlAttribute.Value); // yeah.
                                    continue;
                                case "name":
                                    obj.OBJNAME = XmlAttribute.Value;
                                    continue;
                                case "objimage":
                                    obj.OBJIMAGEPATH = XmlAttribute.Value;
                                    continue;
                                case "gravity":
                                    obj.OBJGRAV = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "playerdamage":
                                    obj.OBJPLAYERDAMAGE = Convert.ToInt32(XmlAttribute.Value);
                                    continue;
                                case "isplayer":
                                    obj.OBJPLAYER = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "hitbox":
                                    string XmlValue = XmlAttribute.Value.Trim();
                                    XmlValue = XmlValue.Replace(" ", "");
                                    string[] hitboxpre = XmlValue.Split(',');
                                    obj.OBJHITBOX = new List<Point>();
                                    obj.OBJHITBOX.Add(new Point(Convert.ToDouble(hitboxpre[0]), Convert.ToDouble(hitboxpre[1]))); // convert.
                                    obj.OBJHITBOX.Add(new Point(Convert.ToDouble(hitboxpre[2]), Convert.ToDouble(hitboxpre[3])));
                                    continue;
                                case "mass": // the mass of the object
                                    obj.OBJMASS = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "priority": //todo: optimize

                                    switch (XmlAttribute.Value)
                                    {
                                        case "invisible":
                                        case "Invisible":
                                        case "InVisible":
                                            obj.OBJPRIORITY = Priority.Invisible;
                                            continue; 
                                        case "Background1":
                                        case "background1":
                                        case "BG1:":
                                        case "bg1":
                                            obj.OBJPRIORITY = Priority.Background1;
                                            continue;
                                        case "Background2":
                                        case "background2":
                                        case "BG2:":
                                        case "bg2":
                                            obj.OBJPRIORITY = Priority.Background2;
                                            continue;
                                        case "Low":
                                        case "low":
                                            obj.OBJPRIORITY = Priority.Low;
                                            continue;
                                        case "Medium":
                                        case "medium":
                                            obj.OBJPRIORITY = Priority.Medium;
                                            continue;
                                        case "high":
                                        case "High":
                                            obj.OBJPRIORITY = Priority.High;
                                            continue;
                                    }
                                    continue;
                                case "cansnap":
                                    obj.OBJCANSNAP = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "collision":
                                    obj.OBJCANCOLLIDE = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "issentient":
                                    obj = new SentientBeing(obj, obj.OBJPLAYER, obj.OBJPLAYERDAMAGE, obj.OBJPLAYERHEALTH, obj.OBJPLAYERLEVEL, obj.OBJPLAYERDAMAGE, obj.OBJPLAYERLIVES, obj.OBJINTERNALID); // convert to sentientbeing
                                    continue;
                            }
                        }
                    //set default values if null

                    if (obj.OBJMASS == 0.0d)
                    {
                        obj.OBJMASS = 1;
                    }
                    
                    if (obj.OBJPLAYERDAMAGE == 0)
                    {
                        obj.OBJPLAYERDAMAGE = -1;
                    }

                    if (obj.OBJPRIORITY == Priority.Invisible)
                    {
                        obj.OBJPRIORITY = Priority.Medium;
                    }

                    if (obj.OBJCANCOLLIDE == null)
                    {
                        obj.OBJCANCOLLIDE = true;
                        //obj.OBJCANCOLLIDE = (bool)obj.OBJCANCOLLIDE; // bool? to bool
                    }
                    if (obj.OBJPLAYERHEALTH == 0 && obj.OBJPLAYER)
                    {
                        obj.OBJPLAYERHEALTH = 100;//TEST value.
                    }

                    obj.OBJIMAGE = new WriteableBitmap(BitmapFactory.FromStream(new FileStream(obj.OBJIMAGEPATH, FileMode.Open)));
                    
                    obj.OBJANIMATIONS = new List<Animation>();
                    ObjectList.Add(obj);

                }
            }
            catch (XmlException err)
            {
                MessageBox.Show($"A critical error occurred while loading Objects.xml: \n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6666);
            }
            catch (FormatException err)
            {
                MessageBox.Show($"A critical error occurred while loading Objects.xml: \n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6666);
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent object, or error loading an object. This is most likely because the object doesn't exist yet.", "avant-gardé engine", 9);
                return;
            }
        }
    }
}
