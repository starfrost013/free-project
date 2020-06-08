using Emerald.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/// <summary>
/// Level/LevelPreloader.cs
/// 
/// Created: 2020-06-07
/// 
/// Modified: 2020-06-08
/// 
/// Purpose: Preloads the level list. 
/// </summary>
/// 
namespace Free
{
    public static class LevelPreloader
    {
        public static List<Level> LoadLevels()
        {

            try
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.Load("LevelsV2.xml");

                XmlNode XRootNode = XDoc.FirstChild;

                while (XRootNode.Name != "Levels")
                {
                    if (XRootNode.NextSibling == null) Error.Throw(null, ErrorSeverity.FatalError, "Fatal error - Cannot find Levels node", "BootNow! Fatal Error", 80);
                    XRootNode = XRootNode.NextSibling;
                }

                if (!XRootNode.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "Fatal error - Levels list is empty", "BootNow! Fatal Error", 81);

                XmlNodeList XLevelChildren = XRootNode.ChildNodes;

                List<Level> LevelList = new List<Level>();

                // Parse the level properties
                foreach (XmlNode XLevelChild in XLevelChildren)
                {
                    Level CurrentLevel = new Level();

                    switch (XLevelChild.Name)
                    {
                        // The Level node.
                        case "Level":

                            if (!XLevelChild.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "Fatal error - Can't load empty level! (make this just continue?)", "BootNow! Fatal Error", 82);

                            XmlNodeList XLevelGrandchildren = XLevelChild.ChildNodes;

                            foreach (XmlNode XLevelGrandchild in XLevelGrandchildren)
                            {
                                switch (XLevelGrandchild.Name)
                                {
                                    // Level properties.
                                    case "ID": // A numeric ID used to identify the level. 
                                        CurrentLevel.ID = Convert.ToInt32(XLevelGrandchild.InnerText);
                                        continue;
                                    case "Name": // The name used to identify the level.
                                        CurrentLevel.NAME = XLevelGrandchild.InnerText;
                                        continue;
                                    case "BGPath": // The path to the level's background.
                                        CurrentLevel.BGPATH = XLevelGrandchild.InnerText;
                                        continue;
                                    case "ObjectLayoutPath": // The path to the object layout of the level.
                                        CurrentLevel.OBJLAYOUTPATH = XLevelGrandchild.InnerText;
                                        continue;
                                    case "Music": // The path to the level's music.
                                        CurrentLevel.MUSICPATH = XLevelGrandchild.InnerText;
                                        continue;
                                    case "Size": // The size of the level.
                                        CurrentLevel.Size = XLevelGrandchild.InnerText.SplitXY();
                                        continue;
                                    case "PlayerStartPos": // The start position of the level.
                                        CurrentLevel.PlayerStartPosition = XLevelGrandchild.InnerText.SplitXY();
                                        continue;
                                    case "KillPlaneY": // The Y position of the level's kill plane.
                                        CurrentLevel.PLRKILLY = Convert.ToDouble(XLevelGrandchild.InnerText);
                                        continue;
                                    case "TextLayout": // The layout of this levels' text.
                                        CurrentLevel.TEXTPATH = XLevelGrandchild.InnerText;
                                        continue;
                                    case "BackgroundSize": // The background size of this level.
                                        CurrentLevel.BackgroundSize = XLevelGrandchild.InnerText.SplitXY();
                                        continue;
                                }
                            }
                            LevelList.Add(CurrentLevel);
                            continue;
                    }


                }


                return LevelList;
            }
            catch (FormatException err)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"Fatal level loading error - error converting between types\n\n{err}", "BootNow! Fatal Error", 83);
                return null; 
            }
            catch (FileNotFoundException err)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"Fatal level loading error - LevelsV2.xml not found!\n{err}", "BootNow! Fatal Error", 84);
                return null;
            }
            catch (XmlException err)
            {
                Error.Throw(null, ErrorSeverity.FatalError, $"Fatal level loading error - LevelsV2.xml corrupted or malformed!\n{err}", "BootNow! Fatal Error", 84);
                return null;
            }
        }
    }
}
