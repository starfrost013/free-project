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
    partial class FreeSDL
    {
        // implement in IGameObject class? (0.06)
        public void LoadIGameObjects()
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

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the IGameObjects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    IGameObject GameObject = new GameObject();
                    // initial values
                    //GameObject.GameObjectGRAV = false;
                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attributes (we dont need to care what the node is called, we just need the right information)

                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name)
                            {
                                case "id":
                                case "Id":
                                    GameObject.GameObjectID = Convert.ToInt32(XmlAttribute.Value); // yeah.
                                    continue;
                                case "name":
                                case "Name":
                                    GameObject.GameObjectNAME = XmlAttribute.Value;
                                    continue;
                                case "objimage":
                                case "ObjImage":
                                case "ObjectImage":
                                    GameObject.GameObjectIMAGEPATH = XmlAttribute.Value;
                                    continue;
                                case "gravity":
                                case "Gravity":
                                    GameObject.GameObjectGRAV = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "playerdamage":
                                case "PlayerDamage":
                                    GameObject.GameObjectPLAYERDAMAGE = Convert.ToInt32(XmlAttribute.Value);
                                    continue;
                                case "isplayer":
                                case "IsPlayer":
                                    GameObject.GameObjectPLAYER = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "hitbox":
                                case "Hitbox":
                                    string XmlValue = XmlAttribute.Value.Trim();
                                    XmlValue = XmlValue.Replace(" ", "");
                                    string[] hitboxpre = XmlValue.Split(',');
                                    GameObject.GameObjectHITBOX = new List<Point>();
                                    GameObject.GameObjectHITBOX.Add(new Point(Convert.ToDouble(hitboxpre[0]), Convert.ToDouble(hitboxpre[1]))); // convert.
                                    GameObject.GameObjectHITBOX.Add(new Point(Convert.ToDouble(hitboxpre[2]), Convert.ToDouble(hitboxpre[3])));
                                    continue;
                                case "mass": // the mass of the IGameObject
                                case "Mass":
                                    GameObject.GameObjectMASS = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "priority": //todo: optimize
                                case "Priority":
                                    switch (XmlAttribute.Value)
                                    {
                                        case "invisible":
                                        case "Invisible":
                                        case "InVisible":
                                            GameObject.GameObjectPRIORITY = Priority.Invisible;
                                            continue; 
                                        case "Background1":
                                        case "background1":
                                        case "BG1:":
                                        case "bg1":
                                            GameObject.GameObjectPRIORITY = Priority.Background1;
                                            continue;
                                        case "Background2":
                                        case "background2":
                                        case "BG2:":
                                        case "bg2":
                                            GameObject.GameObjectPRIORITY = Priority.Background2;
                                            continue;
                                        case "Low":
                                        case "low":
                                            GameObject.GameObjectPRIORITY = Priority.Low;
                                            continue;
                                        case "Medium":
                                        case "medium":
                                            GameObject.GameObjectPRIORITY = Priority.Medium;
                                            continue;
                                        case "high":
                                        case "High":
                                            GameObject.GameObjectPRIORITY = Priority.High;
                                            continue;
                                    }

                                    continue;
                                case "cansnap":
                                case "CanSnap":
                                    GameObject.GameObjectCANSNAP = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "collision":
                                case "Collision":
                                case "CanCollide":
                                    GameObject.GameObjectCANCOLLIDE = Convert.ToBoolean(XmlAttribute.Value);
                                    continue;
                                case "issentient":
                                case "IsSentient":
                                    GameObject = new SentientBeing(GameObject, GameObject.GameObjectPLAYER, GameObject.GameObjectPLAYERDAMAGE, GameObject.GameObjectPLAYERHEALTH, GameObject.GameObjectPLAYERLEVEL, GameObject.GameObjectPLAYERDAMAGE, GameObject.GameObjectPLAYERLIVES, GameObject.GameObjectINTERNALID); // convert to sentientbeing
                                    continue;
                            }
                        }
                    
                        // ADD ERROR CHECKING DUMBASS

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

                                if (!XGrandchildNode.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "E78: Attempted to load an empty AssociatedScript node!", "Error!", 90);

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
                                                        SRRO.EventClass = (EventClass)Enum.Parse(typeof(EventClass), XGGGrandchildNode.ChildNodes[0].Value); // hack to get around shitty system.xml handling
                                                        continue;
                                                    case "Name":
                                                        SRRO.Name = XGGGrandchildNode.ChildNodes[0].Value;
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

                                GameObject.AssociatedScriptPaths.Add(SR);
                                continue; 
                        }
                    }


                    //set default values if null

                    GameObject.JumpIntensity = 1;

                    if (GameObject.GameObjectMASS == 0.0)
                    {
                        GameObject.GameObjectMASS = 1;
                    }
                    
                    if (GameObject.GameObjectPLAYERDAMAGE == 0)
                    {
                        GameObject.GameObjectPLAYERDAMAGE = -1;
                    }

                    if (GameObject.GameObjectPRIORITY == Priority.Invisible)
                    {
                        GameObject.GameObjectPRIORITY = Priority.Medium;
                    }

                    if (GameObject.GameObjectCANCOLLIDE == null)
                    {
                        GameObject.GameObjectCANCOLLIDE = true;
                    }

                    if (GameObject.GameObjectPLAYERHEALTH == 0 && GameObject.GameObjectPLAYER)
                    {
                        GameObject.GameObjectPLAYERHEALTH = 100;//TEST value.
                    }

                    GameObject.GameObjectIMAGE = new WriteableBitmap(BitmapFactory.FromStream(new FileStream(GameObject.GameObjectIMAGEPATH, FileMode.Open)));
                    
                    GameObject.GameObjectANIMATIONS = new List<Animation>();
                    IGameObjectList.Add(GameObject);

                }
            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, $"IGameObjects.xml is corrupted or malformed: \n\n{err}", "avant-gardé engine", 87);
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, $"A critical error occurred while loading IGameObjects.xml: \n\n{err}", "avant-gardé engine", 79);
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, $"A critical error occurred while loading IGameObjects.xml: Error converting to enum (most likely RunOn)\n\n{err}", "avant-gardé engine", 91);
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent IGameObject, or error loading an IGameObject. This is most likely because the IGameObject doesn't exist yet.", "avant-gardé engine", 9);
                return;
            }
        }
    }
}
