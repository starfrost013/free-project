using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;

/// <summary>
/// 
/// File Name: EditMode.cs
/// 
/// Created: 2019-11-23
/// 
/// Modified: 2019-12-16
/// 
/// Version: 1.97
/// 
/// free! Version: 0.14+ (Engine Version 2.14+)
/// 
/// Purpose: Handles Edit Mode for levels.
/// 
/// </summary>


namespace Free
{
    public partial class FreeSDL
    {


        private void FileMenu_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.DefaultExt = ".xml";
            SFD.Filter = "XML files|*.xml";
            SFD.Title = "Save IGameObject Layout";
            SFD.ShowDialog();

            if (SFD.FileName != null) //save
            {
                if (File.Exists(SFD.FileName))
                {
                    File.Delete(SFD.FileName);
                }
                XmlDocument Xdoc = new XmlDocument();
                XmlNode XRootnode = Xdoc.CreateElement("IGameObjectLayout");
                Xdoc.AppendChild(XRootnode); //create the root node.

                foreach (GameObject GameObject in currentlevel.LevelIGameObjects)
                {
                    XmlNode XChild = Xdoc.CreateElement("IGameObject"); // main
                    XmlAttribute XChildGameObjectId = Xdoc.CreateAttribute("id"); // IGameObject id
                    XChildGameObjectId.Value = GameObject.GameObjectID.ToString(); // convert to string
                    XmlAttribute XChildGameObjectPosX = Xdoc.CreateAttribute("posx"); // x pos
                    XChildGameObjectPosX.Value = GameObject.Position.X.ToString(); // convert to string
                    XmlAttribute XChildGameObjectPosY = Xdoc.CreateAttribute("posy"); // y pos
                    XChildGameObjectPosY.Value = GameObject.Position.Y.ToString(); // convert to string
                    XChild.Attributes.Append(XChildGameObjectId);
                    XChild.Attributes.Append(XChildGameObjectPosX);
                    XChild.Attributes.Append(XChildGameObjectPosY);
                    XRootnode.AppendChild(XChild);
                }

                Xdoc.Save(SFD.FileName); 

            }
            else
            {
                return;
            }
        }

        internal static double RoundNearest(double raw, double n)
        {
            return (Math.Round(raw / n)) * n;
        }

        private void InteractionMenu_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.DefaultExt = ".xml";
            SFD.Filter = "XML files|*.xml";
            //SFD.InitialDirectory = ".";
            SFD.Title = "Save Interactions";
            
            SFD.ShowDialog();

            if (SFD.FileName != null) //save
            {
                if (File.Exists(SFD.FileName))
                {
                    File.Delete(SFD.FileName);
                }
                XmlDocument Xdoc = new XmlDocument();
                XmlNode XRootnode = Xdoc.CreateElement("Interactions");
                Xdoc.AppendChild(XRootnode); //create the root node.

                foreach (Interaction interaction in InteractionList)
                {
                    XmlNode XChild = Xdoc.CreateElement("Interaction"); // main
                    XmlAttribute XChildObjId1Id = Xdoc.CreateAttribute("ObjId1"); // IGameObject id
                    XChildObjId1Id.Value = interaction.ObjId1ID.ToString(); // convert to string
                    XmlAttribute XChildObjId2Id = Xdoc.CreateAttribute("ObjId2"); // x pos
                    XChildObjId2Id.Value = interaction.ObjId2ID.ToString(); // convert to string
                    XmlAttribute XChildInteractionType = Xdoc.CreateAttribute("type"); // y pos
                    XChildInteractionType.Value = interaction.GameObjectINTERACTIONTYPE.ToString(); // convert to string
                    XChild.Attributes.Append(XChildObjId1Id);
                    XChild.Attributes.Append(XChildObjId2Id);
                    XChild.Attributes.Append(XChildInteractionType);
                    XRootnode.AppendChild(XChild);
                }

                Xdoc.Save(SFD.FileName);

            }
            else
            {
                return;
            }
        }



    }
}
