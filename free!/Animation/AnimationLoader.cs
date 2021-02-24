using Emerald.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Media.Imaging;
/// <summary>
/// 
/// AnimationsLoader.cs
/// 
/// Created: 2019-11-22
/// 
/// Modified: 2020-06-13
/// 
/// Version: 1.10
/// 
/// Free Version: bringup-2.21.1361.46+ [v1.00 → v1.10 2020/06/13: Freeze IGameObjects for optimisation purposes]
///  
/// Purpose: Loads the animation list for every IGameObject in the game.
/// 
/// </summary>
namespace Free
{
    partial class FreeSDL
    {
        public void LoadAnimations()
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load($@"{GlobalSettings.CurrentGame.ContentFolderLocation}\Animations.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Animations")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the IGameObjects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    int? GameObjectId = null;
                    Animation Animation = new Animation();
                    Animation.animImages = new List<WriteableBitmap>();
                    Animation.numMs = new List<int>();

                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes;

                    foreach (XmlAttribute XmlAttribute in XmlAttributes)
                    {
                        switch (XmlAttribute.Name)
                        {
                            case "GameObjectID":
                            case "GameObjectid":
                                GameObjectId = Convert.ToInt32(XmlAttribute.Value);
                                continue;
                            case "Type":
                            case "type":
                                Animation.animationType = (AnimationType)Enum.Parse(typeof(AnimationType), XmlAttribute.Value);
                                continue;
                        }
                    }

                    if (!XmlNode.HasChildNodes)
                    {
                        Error.Throw(new Exception($"Attempted to load nonexistent animation for IGameObject ID {GameObjectId} and type {Animation.animationType}"), ErrorSeverity.FatalError, "Cannot load an empty animation.", "avant-gardé engine", 14);
                    }

                    XmlNodeList XmlNode_C = XmlNode.ChildNodes;
                    foreach (XmlNode XmlNode_Cc in XmlNode_C)
                    {
                        if (XmlNode_Cc.Name == "AnimationFrame")
                        {
                            XmlAttributeCollection XmlNode_CcAttributes = XmlNode_Cc.Attributes;

                            foreach (XmlAttribute XmlNode_CcAttribute in XmlNode_CcAttributes)
                            {
                                switch (XmlNode_CcAttribute.Name)
                                {
                                    case "Image":
                                    case "image":  // load the image
                                        WriteableBitmap AnimFrame = new WriteableBitmap(BitmapFactory.FromStream(new FileStream(XmlNode_CcAttribute.Value, FileMode.Open)));

                                        SDLDebug.LogDebug_C("Animation Loader", $"Successfully loaded animation frame at path {XmlNode_CcAttribute.Value} with type {Animation.animationType} for object {GameObjectId}");
                                        Animation.animImages.Add(AnimFrame);
                                        continue;
                                    case "DispMS":
                                    case "Dispms":
                                    case "DisplayMs":
                                    case "DisplayMilliseconds":
                                    case "dispms":
                                        int _ = Convert.ToInt32(XmlNode_CcAttribute.Value);
                                        Animation.numMs.Add(_);
                                        SDLDebug.LogDebug_C("Animation Loader", $"Length = {_}");
                                        continue;
                                }
                            }

                        }
                    }


                    foreach (IGameObject GameObject in IGameObjectList) //add the anim to the IGameObject
                    {
                        if (GameObject.GameObjectID == GameObjectId && GameObject.GameObjectID != -1) // -1 = "no IGameObject"
                        {
                            GameObject.GameObjectANIMATIONS.Add(Animation);
                        }
                    }

                    //added

                    if (GameObjectId == -1)
                    {
                        SDLDebug.LogDebug_C("Animation Loader", "Last loaded animation has no associated object."); 
                        NonGameObjectAnimList.Add(Animation); // add
                    }
                }
            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred loading animations. Most likely, the animation XML was defined incorrectly.", "avant-gardé engine", 13);
            }
        }
    }
}
