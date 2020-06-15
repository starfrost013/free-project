using Emerald.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using Emerald.Utilities;

/// <summary>
/// 
/// /Settings/SettingLoader.cs
/// 
/// Created: 2019-12-22
/// 
/// Modified: 2019-12-22
/// 
/// Version: 1.00 (Will be entirely rewritten)
/// 
/// Purpose: Loads settings
/// 
/// </summary>

namespace Free
{
    public partial class MainWindow
    {
        public void LoadSettings()
        {
            //Setting Engine
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load("Settings.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Settings")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the Objects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    switch (XmlNode.Name)
                    {
                        case "Setting":
                            if (XmlNode.Attributes.Count > 0)
                            {
                                XmlAttributeCollection XmlAttributes = XmlNode.Attributes;

                                foreach (XmlAttribute XmlAttribute in XmlAttributes)
                                {
                                    switch (XmlAttribute.Name)
                                    {
                                        case "Name":
                                        case "name":
                                            switch (XmlAttribute.Value)
                                            {
                                                case "DebugMode": // debug mode
                                                    Settings.DebugMode = Convert.ToBoolean(XmlAttributes[1].Value);
                                                    continue;
                                                case "DemoMode": // demo mode
                                                    Settings.DemoMode = Convert.ToBoolean(XmlAttributes[1].Value);
                                                    continue;
                                                case "DemoModeMaxLevel":
                                                case "demomodemaxlevel":
                                                case "DemoMaxLevel":
                                                case "demomaxlevel":
                                                case "MaxLevel":
                                                case "maxlevel": // demo mode maximum level
                                                    Settings.DemoModeMaxLevel = Convert.ToInt32(XmlAttributes[1].Value);
                                                    continue;
                                                case "GameName": // game name
                                                    Settings.GameName = XmlAttributes[1].Value;
                                                    continue;
                                                case "GameDev": // game developer
                                                case "GameDeveloper":
                                                    Settings.GameDeveloper = XmlAttributes[1].Value;
                                                    continue;
                                                case "Resolution": // window size
                                                    string[] ResolutionStr = XmlAttributes[1].Value.Split(',');
                                                    Settings.Resolution = XmlAttributes[1].Value.SplitXY();
                                                    continue;
                                                case "TitleScreen":
                                                case "TitleScreenPath": // title screen path
                                                    Settings.TitleScreenPath = XmlAttributes[1].Value;
                                                    continue;
                                                case "UseSDLX": //use innertext
                                                    Settings.UseSDLX = Convert.ToBoolean(XmlAttributes[1].Value);
                                                    continue; 
                                                case "WindowType": // window type (min/max/fullscreen)
                                                    Settings.WindowType = (WindowType)Enum.Parse(typeof(WindowType), XmlAttributes[1].Value);
                                                    continue;
                                            }
                                            continue;
                                    }
                                }

                                continue;
                            }
                            else
                            {
                                Error.Throw(new Exception("Empty node found in Settings.xml"), ErrorSeverity.FatalError, "Empty node found in Settings.xml.", "avant-gardé engine", 33);
                                return;
                            }
                        case "#comment":
                            continue;
                        default:
                            Error.Throw(new Exception("Invalid node found in Settings.xml"), ErrorSeverity.FatalError, "Invalid node found in Settings.xml. The only accepted node is the Setting node.", "avant-gardé engine", 32);
                            return;
                    }
                }
            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred loading Settings.xml", "avant-gardé engine", 34);
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Incorrect data was input into Settings.xml", "avant-gardé engine", 35);
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Incorrect data was input into a WindowType setting in Settings.xml", "avant-gardé engine", 36);
            }
            catch (OverflowException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Incorrect data was input into a setting that accepts an int or double in Settings.xml", "avant-gardé engine", 37);
            }
        }
    }
}
