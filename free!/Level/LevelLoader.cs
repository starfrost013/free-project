using Emerald.Utilities;
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
    public partial class MainWindow : Window
    {
        public Level LoadLevel(int id)
        {
            Level level = new Level();
            Game.DataContext = level;
            LevelBackground.DataContext = level;

            // read the level xml file
            // implement this code in the Level class? polymorphic addition.

            // 2020-06-05 FIX THIS SHIT CODE
            try
            {
                
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load("Levels.xml");

                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Levels")
                {
                    if (XmlRootNode.NextSibling == null) Error.Throw(null, ErrorSeverity.FatalError, "Error: Can't find Levels node in Levels.xml.", "Error!", 50);
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                int LvID = 0;
                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the Levels node.
                
                foreach (XmlNode XmlNode in XmlNodes)
                {
                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes; // get the attributes

                    foreach (XmlAttribute XmlAttribute in XmlAttributes)
                    {
                        switch (XmlAttribute.Name) //todo: fix bad code - make it use XmlAttributes[0].Value...maybe?
                        {
                            //I'm not sure what hellspawn wrote this code,
                            //And it needs to be fixed,
                            //But I can't look at it without wanting to vomit

                            case "id":
                                LvID = Convert.ToInt32(XmlAttribute.Value);
                                if (Convert.ToInt32(XmlAttribute.Value) == id) // convert to an int. 
                                {
                                    level.ID = Convert.ToInt32(XmlAttribute.Value);
                                    
                                }
                                continue;
                            case "name":
                                if (LvID == level.ID) // check the level id
                                {
                                    level.NAME = XmlAttribute.Value;
                                }
                                continue;
                            case "bgpath":
                                if (LvID == level.ID)
                                {
                                    level.BGPATH = XmlAttribute.Value;
                                }
                                continue;
                            case "objlayout":
                                if (LvID == level.ID)
                                {
                                    level.OBJLAYOUTPATH = XmlAttribute.Value;
                                }
                                continue;
                            case "music":
                                if (LvID == level.ID)
                                {
                                    level.MUSICPATH = XmlAttribute.Value;
                                }
                                continue;
                            case "sizex":
                                if (LvID == level.ID)
                                {
                                    level.SIZEX = Convert.ToInt32(XmlAttribute.Value);
                                }
                                continue;
                            case "sizey":
                                if (LvID == level.ID)
                                {
                                    level.SIZEY = Convert.ToInt32(XmlAttribute.Value);
                                }
                                continue;
                            case "plrstartx":
                                if (LvID == level.ID)
                                {
                                    level.PLRSTARTX = Convert.ToDouble(XmlAttribute.Value);
                                }
                                continue;
                            case "plrstarty":
                                if (LvID == level.ID)
                                {
                                    level.PLRSTARTY = Convert.ToDouble(XmlAttribute.Value);
                                }
                                continue;
                            case "killplaney":
                                if (LvID == level.ID)
                                {
                                    level.PLRKILLY = Convert.ToDouble(XmlAttribute.Value);
                                }
                                continue;
                            case "text":
                                if (LvID == level.ID)
                                {
                                    level.TEXTPATH = XmlAttribute.Value;
                                }
                                continue;
                            /// WILL FIX THIS CARNAL SIN
                            case "BackgroundSize":
                                if (LvID == level.ID)
                                {
                                    // yay
                                    level.BackgroundSize = Utils.SplitXY(XmlAttribute.Value);
                                }
                                continue;

                        }
                    }
                }

                if (level.Size.X == 0 || level.Size.Y == 0) // we didn't put level size in the xml
                {
                    Error.Throw(new Exception("Level size not defined!"), ErrorSeverity.FatalError, "An error occurred while loading a level. This is most likely because its size was not defined.", "avant-gardé engine", 86);
                }

                /*
                 * Old pre-2.21.1304.9 background init
                level.BG = new BitmapImage();
                level.BG.BeginInit();
                level.BG.UriSource = new Uri($"{level.BGPATH}", UriKind.Relative);
                level.BG.EndInit();
                Game.Width = level.SIZEX;
                Game.Height = level.SIZEY;
                Game.Background = new ImageBrush(level.BG); */
                
                BitmapImage LvBackNew = (BitmapImage)LevelBackground.Source;
                LvBackNew = new BitmapImage(); // a massive hack but w/e
                LvBackNew.BeginInit();
                LvBackNew.DecodePixelWidth = (int)level.BackgroundSize.X;
                LvBackNew.DecodePixelHeight = (int)level.BackgroundSize.Y;
                LvBackNew.UriSource = new Uri(level.BGPATH, UriKind.Relative);
                LevelBackground.Width = LvBackNew.DecodePixelWidth;
                LevelBackground.Height = LvBackNew.DecodePixelHeight; // something.getsomething function?????????
                LvBackNew.EndInit();
                UpdateLayout();
                
                LevelBackgroundImage = LvBackNew;

                /*
                if (Game.Width > Width || Game.Height > Height)
                {
                    Game.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                    Game.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor); // "pixelated" look -- IMPORTANT!! LEVEL SIZE MUST BE MULTIPLE OF BG IMAGE SIZE OR IT WILL LOOK LIKE SHIT!!!!
                    Game.SnapsToDevicePixels = true;
                } */

                if (LvBackNew.DecodePixelWidth > Width || LvBackNew.DecodePixelHeight > Height)
                {
                    LvBackNew.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                    LvBackNew.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor); // "pixelated" look -- IMPORTANT!! LEVEL SIZE MUST BE MULTIPLE OF BG IMAGE SIZE OR IT WILL LOOK LIKE SHIT!!!!

                }

                return level;
            }

            catch (XmlException err)
            {
                // Uh oh
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. This is most likely because the level's XML file is malformed.", "avant-gardé engine", 12);
                return null; // TODO: Change to error message.
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. This is most likely because the Goal object attempted to trigger its interaction but the next level by ID doesn't exist yet.", "avant-gardé engine", 7);
                return null;
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading a level. This is most likely because its size was defined incorrectly.", "avant-gardé engine", 11);
                return null;
            }


        }

        public void BootNow_SetCurrentLevel(int LevelId)
        {
            Level level = null;

            // Get the level with this level id./
            foreach (Level Level in Levels)
            {
                // Set it
                if (Level.ID == LevelId)
                {
                    level = Level;
                }
            }

            // Error checking
            if (level == null) Error.Throw(null, ErrorSeverity.FatalError, "Fatal Error: Failed to set level!", "Fatal Error", 86);

            Game.DataContext = level;
            LevelBackground.DataContext = level;

            // read the level xml file
            // implement this code in the Level class? polymorphic addition.

            // 2020-06-05 FIX THIS SHIT CODE
            try
            {

                if (level.Size.X == 0 || level.Size.Y == 0) // we didn't put level size in the xml
                {
                    Error.Throw(new Exception("Level size not defined!"), ErrorSeverity.FatalError, "An error occurred while loading a level. This is most likely because its size was not defined.", "avant-gardé engine", 85);
                }

                BitmapImage LvBackNew = (BitmapImage)LevelBackground.Source;
                LvBackNew = new BitmapImage(); // a massive hack but w/e
                LvBackNew.BeginInit();
                LvBackNew.DecodePixelWidth = (int)level.BackgroundSize.X;
                LvBackNew.DecodePixelHeight = (int)level.BackgroundSize.Y;
                LvBackNew.UriSource = new Uri(level.BGPATH, UriKind.Relative);
                LevelBackground.Width = LvBackNew.DecodePixelWidth;
                LevelBackground.Height = LvBackNew.DecodePixelHeight; // something.getsomething function?????????
                LvBackNew.EndInit();
                UpdateLayout();

                LevelBackgroundImage = LvBackNew;


                if (LvBackNew.DecodePixelWidth > Width || LvBackNew.DecodePixelHeight > Height)
                {
                    LvBackNew.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                    LvBackNew.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.NearestNeighbor); // "pixelated" look -- IMPORTANT!! LEVEL SIZE MUST BE MULTIPLE OF BG IMAGE SIZE OR IT WILL LOOK LIKE SHIT!!!!

                }

                currentlevel = level;

                return;
            }

            catch (XmlException err)
            {
                // Uh oh
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. This is most likely because the level's XML file is malformed.", "avant-gardé engine", 12);
                return; // TODO: Change to error message.
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. This is most likely because a change level object attempted to trigger its interaction but the next level by ID doesn't exist (yet).", "avant-gardé engine", 7);
                return;
            }
            catch (PathTooLongException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level, or error loading a level. PathTooLongException: WINDOWS SUCKS! WINDOWS SUCKS! WINDOWS SUCKS!", "avant-gardé engine", 7);
                return;
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading a level. This is most likely because its size was defined incorrectly.", "avant-gardé engine", 11);
                return;
            }
        }
    }
}
