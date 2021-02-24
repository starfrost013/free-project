using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;

/// <summary>
/// 
/// File: InteractionManager.cs
/// 
/// Created: 2019-11-18
/// 
/// Modified: 2019-12-04
/// 
/// Purpose: Manages interactions between IGameObjects.
/// 
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        // placeholder until IGameObject manager is done
        public void LoadInteractions()
        {
            // Load the interaction XML
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load($@"{GlobalSettings.CurrentGame.ContentFolderLocation}\Interactions.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Interactions")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the IGameObjects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    Interaction interaction = new Interaction();
                    if (XmlNode.Name == "Interaction")
                    {
                        XmlAttributeCollection XmlAttributes = XmlNode.Attributes;
                        foreach (XmlAttribute XmlAttribute in XmlAttributes) // loop through the attributes for every Interaction node
                        {
                            switch (XmlAttribute.Name) // switch the name
                            {
                                case "Obj1Id":
                                case "obj1id":
                                    interaction.ObjId1ID = Convert.ToInt32(XmlAttribute.Value);
                                    continue;
                                case "Obj2Id":
                                case "obj2Id":
                                    interaction.ObjId2ID = Convert.ToInt32(XmlAttribute.Value);
                                    continue;
                                case "Type":
                                case "type":
                                    interaction.GameObjectINTERACTIONTYPE = (InteractionType)Enum.Parse(typeof(InteractionType), XmlAttribute.Value);
                                    continue;

                            }
                        }
                    }
#if DEBUG
                    SDLDebug.LogDebug_C("LoadInteractions()", $"Loaded interaction between {interaction.ObjId1ID} and {interaction.ObjId2ID} with type {interaction.GameObjectINTERACTIONTYPE}. Interactions are deprecated and replaced by SimpleESX on-hit scripts.");
#endif
                    InteractionList.Add(interaction);

                }

            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading Interactions.xml", "avant-gardé engine", 4);
                Environment.Exit(6666);
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading Interactions.xml", "avant-gardé engine", 5);
                Environment.Exit(6666);
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading Interactions.xml", "avant-gardé engine", 6);
                Environment.Exit(6666);
            }
        }
    }
}
