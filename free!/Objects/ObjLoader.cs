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
                                case "Id":
                                    obj.OBJID = Convert.ToInt32(XmlAttribute.Value); // yeah.
                                    continue;
                                case "name":
                                case "Name":
                                    obj.OBJNAME = XmlAttribute.Value;
                                    continue;
                                case "objimage":
                                case "ObjImage":
                                case "ObjectImage":
                                    obj.OBJIMAGEPATH = XmlAttribute.Value;
                                    continue;
                                case "gravity":
                                case "Gravity":
                                    obj.OBJGRAV = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "playerdamage":
                                case "PlayerDamage":
                                    obj.OBJPLAYERDAMAGE = Convert.ToInt32(XmlAttribute.Value);
                                    continue;
                                case "isplayer":
                                case "IsPlayer":
                                    obj.OBJPLAYER = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "hitbox":
                                case "Hitbox":
                                    string XmlValue = XmlAttribute.Value.Trim();
                                    XmlValue = XmlValue.Replace(" ", "");
                                    string[] hitboxpre = XmlValue.Split(',');
                                    obj.OBJHITBOX = new List<Point>();
                                    obj.OBJHITBOX.Add(new Point(Convert.ToDouble(hitboxpre[0]), Convert.ToDouble(hitboxpre[1]))); // convert.
                                    obj.OBJHITBOX.Add(new Point(Convert.ToDouble(hitboxpre[2]), Convert.ToDouble(hitboxpre[3])));
                                    continue;
                                case "mass": // the mass of the object
                                case "Mass":
                                    obj.OBJMASS = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "priority": //todo: optimize
                                case "Priority":
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
                                case "CanSnap":
                                    obj.OBJCANSNAP = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "collision":
                                case "Collision":
                                case "CanCollide":
                                    obj.OBJCANCOLLIDE = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "issentient":
                                case "IsSentient":
                                    obj = new SentientBeing(obj, obj.OBJPLAYER, obj.OBJPLAYERDAMAGE, obj.OBJPLAYERHEALTH, obj.OBJPLAYERLEVEL, obj.OBJPLAYERDAMAGE, obj.OBJPLAYERLIVES, obj.OBJINTERNALID); // convert to sentientbeing
                                    continue;
                            }
                        }
                    // NEW 2020-06-08
                    // Parse AssociatedScripts

                    XmlNodeList XGrandchildNodes = XmlNode.ChildNodes;

                    // Loop through all the scripts in XML

                    foreach (XmlNode XGrandchildNode in XmlNode.ChildNodes)
                    {
                        switch (XGrandchildNode.Name)
                        {
                            // The scripts associated with this node. 
                            case "AssociatedScript":

                                if (!XGrandchildNode.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "E78: Attempted to load an empty AssociatedScript node!", "Error!", 78);

                                XmlNodeList XGreatGrandchildNodes = XGrandchildNode.ChildNodes;

                                ScriptReference SR = new ScriptReference(); 

                                // Iterate through the associatedscript properties.
                                
                                foreach (XmlNode XGreatGrandchildNode in XGreatGrandchildNodes)
                                {
                                    switch (XGreatGrandchildNode.Name)
                                    {
                                        // If the node name is...
                                        case "Name":
                                            SR.Name = XGreatGrandchildNode.InnerText;
                                            continue;
                                        case "Path":
                                            SR.Path = XGreatGrandchildNode.InnerText;
                                            continue;
                                        case "RunOn":

                                            // What this event runs on. 
                                            if (!XGreatGrandchildNode.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "E88: Attempted to load an empty RunOn with no parameters! (old format RunOn?)", "Error!", 88);

                                            ScriptReferenceRunOn SRRO = new ScriptReferenceRunOn();

                                            XmlNodeList XGGGrandchildNodes = XGreatGrandchildNode.ChildNodes;

                                            foreach (XmlNode XGGGrandchildNode in XGGGrandchildNodes)
                                            {
                                                switch (XGGGrandchildNode.Name)
                                                {
                                                    // The event class.
                                                    case "EventClass":
                                                        SRRO.EventClass = (EventClass)Enum.Parse(typeof(EventClass), XGreatGrandchildNode.InnerText);
                                                        continue;
                                                    case "Name":
                                                        SRRO.Name = XGreatGrandchildNode.InnerText;
                                                        continue;
                                                    case "Parameter":
                                                        if (!XGGGrandchildNode.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "E89: Attempted to load an empty RunOn parameter! (old format RunOn?)", "Error!", 89);

                                                        XmlNodeList XOhGod = XGGGrandchildNode.ChildNodes;

                                                        ScriptReferenceRunOnParameter SRROP = new ScriptReferenceRunOnParameter();

                                                        foreach (XmlNode XJeez in XOhGod)
                                                        {
                                                            // Iterate through the parameters
                                                            switch (XJeez.Name)
                                                            {
                                                                case "Name":
                                                                    SRROP.Name = XJeez.InnerText;
                                                                    continue;
                                                                case "Value":
                                                                    SRROP.Value.Add(XJeez.InnerText);
                                                                    continue;
                                                            }

                                                        }

                                                        SRRO.ReferenceRunOn.Add(SRROP);

                                                        continue;


                                                }
                                            }

                                            SR.RunOnParameters.Add(SRRO);
                                            
                                            continue;
                                    }
                                }

                                obj.AssociatedScriptPaths.Add(SR);
                                continue; 
                        }
                    }


                    //set default values if null

                    if (obj.OBJMASS == 0.0)
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
                Error.Throw(err, ErrorSeverity.FatalError, $"Objects.xml is corrupted or malformed: \n\n{err}", "avant-gardé engine", 87);
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, $"A critical error occurred while loading Objects.xml: \n\n{err}", "avant-gardé engine", 79);
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, $"A critical error occurred while loading Objects.xml: Error converting to enum (most likely RunOn)\n\n{err}", "avant-gardé engine", 90);
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent object, or error loading an object. This is most likely because the object doesn't exist yet.", "avant-gardé engine", 9);
                return;
            }
        }
    }
}
