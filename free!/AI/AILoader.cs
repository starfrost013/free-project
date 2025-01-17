﻿using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Free
{
    public partial class FreeSDL
    {
        public void LoadAI()
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load($@"{GlobalSettings.CurrentGame.ContentFolderLocation}\AI.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "AIDefinitions")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the IGameObjects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes;


                    if (XmlAttributes.Count > 0)
                    {
                        AI AI = new AI();
                        switch (XmlNode.Name)
                        {
                            case "AI": 
                                foreach (XmlAttribute XmlAttribute in XmlAttributes)
                                {
                                    switch (XmlAttribute.Name)
                                    {
                                        case "ObjId1":
                                        case "objId1":
                                            AI.ObjId1Id = Convert.ToInt32(XmlAttribute.Value);
                                            continue;
                                        case "ObjId2":
                                        case "objId2":
                                            AI.ObjId2Id = Convert.ToInt32(XmlAttribute.Value);
                                            continue;
                                        case "AIType":
                                        case "aitype":
                                        case "type":
                                        case "ai":
                                            AI.AIType = (AIType)Enum.Parse(typeof(AIType), XmlAttribute.Value); // get the AI type using Enum.Parse (convert from string.
                                            continue;
                                        case "Intensity":
                                        case "intensity":
                                            AI.AIIntensity = Convert.ToInt32(XmlAttribute.Value);
                                            continue;
                                    }
                                }

                                foreach (IGameObject GameObject in IGameObjectList)
                                {
                                    if (AI.ObjId1Id == GameObject.GameObjectID && IsSentientBeing(GameObject))// assign the aitype
                                    {
                                        GameObject.GameObjectAI = AI;
                                    }

                                }

                                SDLDebug.LogDebug_C("AI Loader", $"Loaded AI relationship between {AI.ObjId1Id} and {AI.ObjId2Id} of type {AI.AIType} with intensity {AI.AIIntensity}");

                                continue;
                        }
                    }
                }
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Error: Invalid AIType specified or conversion overflow.", "avant-gardé engine", 38);
                return;
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Error converting string value to int32 in AI.xml: most likely you put non-numerical characters in", "avant-gardé engine", 39);
                return;
            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Error loading AI.xml. Must exit. Please reinstall free!.", "avant-gardé engine", 40);
                return;
            }
        }
    }
}
