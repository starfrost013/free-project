﻿using Microsoft.Win32;
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
        public void EnterEditMode()
        {
            switch (Gamestate)
            {
                case GameState.Game: // display scrollbars if edit mode is on
                    Gamestate = GameState.EditMode;

                    if (!FullScreen) // prevent the drawing from not happening properly if fullscreen off
                    {
                        this.Width = 817;
                        this.Height = 463;
                    }

                    Scrollbar.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                    Scrollbar.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    EditModeMenu.Height = 19;
                    return;
                case GameState.EditMode:
                    Gamestate = GameState.Game;

                    if (!FullScreen)
                    {
                        this.Width = 817;
                        this.Height = 457;
                    }
                    Scrollbar.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    Scrollbar.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    EditModeMenu.Height = 0;
                    return;
            }
        }

        private void FileMenu_Load_Click(object sender, RoutedEventArgs e) // we want to load a level, just opens level select
        {
            LevelSelect LevelSelect = new LevelSelect(this);
            LevelSelect.Owner = this;
            LevelSelect.Show();
        }

        
        private void InteractionMenu_Add_Click(object sender, RoutedEventArgs e)
        {
            ManageInteractions ManageInteractions = new ManageInteractions(this);
            ManageInteractions.Owner = this;
            ManageInteractions.Show();
        }

        private void FileMenu_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.DefaultExt = ".xml";
            SFD.Filter = "XML files|*.xml";
            SFD.Title = "Save Object Layout";
            SFD.ShowDialog();

            if (SFD.FileName != null) //save
            {
                if (File.Exists(SFD.FileName))
                {
                    File.Delete(SFD.FileName);
                }
                XmlDocument Xdoc = new XmlDocument();
                XmlNode XRootnode = Xdoc.CreateElement("ObjectLayout");
                Xdoc.AppendChild(XRootnode); //create the root node.

                foreach (Obj obj in currentlevel.LevelObjects)
                {
                    XmlNode XChild = Xdoc.CreateElement("Object"); // main
                    XmlAttribute XChildObjId = Xdoc.CreateAttribute("id"); // object id
                    XChildObjId.Value = obj.OBJID.ToString(); // convert to string
                    XmlAttribute XChildObjPosX = Xdoc.CreateAttribute("posx"); // x pos
                    XChildObjPosX.Value = obj.OBJX.ToString(); // convert to string
                    XmlAttribute XChildObjPosY = Xdoc.CreateAttribute("posy"); // y pos
                    XChildObjPosY.Value = obj.OBJY.ToString(); // convert to string
                    XChild.Attributes.Append(XChildObjId);
                    XChild.Attributes.Append(XChildObjPosX);
                    XChild.Attributes.Append(XChildObjPosY);
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
                    XmlAttribute XChildObj1Id = Xdoc.CreateAttribute("obj1"); // object id
                    XChildObj1Id.Value = interaction.OBJ1ID.ToString(); // convert to string
                    XmlAttribute XChildObj2Id = Xdoc.CreateAttribute("obj2"); // x pos
                    XChildObj2Id.Value = interaction.OBJ2ID.ToString(); // convert to string
                    XmlAttribute XChildInteractionType = Xdoc.CreateAttribute("type"); // y pos
                    XChildInteractionType.Value = interaction.OBJINTERACTIONTYPE.ToString(); // convert to string
                    XChild.Attributes.Append(XChildObj1Id);
                    XChild.Attributes.Append(XChildObj2Id);
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


        private void ObjMenu_ObjManager_Click(object sender, RoutedEventArgs e)
        {
            ObjectManager ObjectManager = new ObjectManager(this);
            ObjectManager.Owner = this;
            ObjectManager.Show();
        }

    }
}
