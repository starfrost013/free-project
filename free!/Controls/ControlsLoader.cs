using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// 
/// ControlLoader.cs
/// 
/// File created: 2019-11-17
/// 
/// File modified: 2019-11-17
/// 
/// Purpose: Loads configurable controls.
/// 
/// Created by: Cosmo
/// 
/// Free ver: 0.05 (Engine 2.5.0/01)
/// 
/// </summary>
namespace Free
{
    partial class FreeSDL
    {
        // v2.4.0/06+
        public void LoadControls()
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load("Controls.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Controls")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the Objects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    if (XmlNode.Name == "Control")
                    {
                        XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attributes (we dont need to care what the node is called, we just need the right information)
                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name)
                            {
                                case "name":
                                    KeyConverter KeyConverter = new KeyConverter();
                                    switch (XmlAttribute.Value) // get the value
                                    {
                                        case "Fire": // firing the gun
                                            //get special handling for this boi. 
                                            if (XmlAttributes[1].Value.Contains("MOUSE"))
                                            {
                                                Controls.Fire_MouseButton = XmlAttributes[1].Value; //TEST
                                            }
                                            else
                                            {
                                                Controls.Fire = Controls.ConvertKey(XmlAttributes[1].Value);
                                            }
                                            continue;
                                        case "MoveLeft": // moving left
                                            Controls.MoveLeft = Controls.ConvertKey(XmlAttributes[1].Value);
                                            continue;
                                        case "MoveRight": // moving right
                                            Controls.MoveRight = Controls.ConvertKey(XmlAttributes[1].Value);
                                            continue;
                                        case "Jump": // jumping
                                            Controls.Jump = Controls.ConvertKey(XmlAttributes[1].Value);
                                            continue;
                                        case "Pause": // pausing
                                            Controls.Pause = Controls.ConvertKey(XmlAttributes[1].Value);
                                            continue;

                                    }

                                    continue;
                            }
                        }
                    }
                }
            }
            catch (XmlException err) //TODO: Add the error class from the track maker.
            {
                MessageBox.Show($"A critical error occurred while loading Objects.xml: \n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6666);
            }
            catch (FormatException err)
            {
                MessageBox.Show($"A critical error occurred while loading Objects.xml: \n\n{err}", "avant-gardé engine", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6666);
            }
        }
    }
}
