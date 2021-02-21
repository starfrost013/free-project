using Emerald.Core;
using Emerald.Utilities;
using SDLX;
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
/// Modified: 2021-02-19 (v1519)
/// 
/// Purpose: Preloads the level list. 
/// </summary>

namespace Free
{
    public static class LevelPreloader
    {
        public static List<Level> LoadLevels()
        {

            try
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.Load(@"Content\LevelsV2.xml");

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

#if DEBUG
                            SDLDebug.LogDebug_C("Level Loader V2.0", "Currently loading level...");
#endif
                            foreach (XmlNode XLevelGrandchild in XLevelGrandchildren)
                            {
                                switch (XLevelGrandchild.Name)
                                {
                                    // Level properties.
                                    case "ID": // A numeric ID used to identify the level. 
                                        CurrentLevel.ID = Convert.ToInt32(XLevelGrandchild.InnerText);
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"ID = {CurrentLevel.ID}");
                                        continue;
                                    case "Name": // The name used to identify the level.
                                        CurrentLevel.Name = XLevelGrandchild.InnerText;
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Name = {CurrentLevel.Name}\n");
                                        continue;
                                    case "BGPath": // The path to the level's background.
                                        CurrentLevel.BGPATH = XLevelGrandchild.InnerText;
                                        // very temp
                                        SDL_Stage0_Init.SDLGame.CurrentScene.Background.Path = CurrentLevel.BGPATH;
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Background path = {CurrentLevel.BGPATH}\n");
                                        continue;
                                    case "ObjectLayoutPath": // The path to the IGameObject layout of the level.
                                        CurrentLevel.LevelIGameObjectsPATH = XLevelGrandchild.InnerText;
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Object layout path = {CurrentLevel.LevelIGameObjectsPATH}\n");
                                        continue;
                                    case "Music": // The path to the level's music.
                                        CurrentLevel.MUSICPATH = XLevelGrandchild.InnerText;
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Music is playing on this level. Path = {CurrentLevel.MUSICPATH}\n");
                                        continue;
                                    case "Size": // The size of the level.
                                        CurrentLevel.Size = XLevelGrandchild.InnerText.SplitXY();
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Size = {CurrentLevel.Size.X},{CurrentLevel.Size.Y}\n");
                                        continue;
                                    case "PlayerStartPos": // The start position of the level.
                                        CurrentLevel.PlayerStartPosition = XLevelGrandchild.InnerText.SplitXY();
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Player start position = {CurrentLevel.PlayerStartPosition.X},{CurrentLevel.PlayerStartPosition.Y}\n");
                                        continue;
                                    case "KillPlaneY": // The Y position of the level's kill plane.
                                        CurrentLevel.PLRKILLY = Convert.ToDouble(XLevelGrandchild.InnerText);
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Kill plane height = {CurrentLevel.PLRKILLY}\n");
                                        continue;
                                    case "TextLayout": // The layout of this levels' text.
                                        CurrentLevel.TEXTPATH = XLevelGrandchild.InnerText;
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Text layout (deprecated) path = {CurrentLevel.TEXTPATH}\n");
                                        continue;
                                    case "BackgroundSize": // The background size of this level.
                                        CurrentLevel.BackgroundSize = XLevelGrandchild.InnerText.SplitXY();
                                        SDLDebug.LogDebug_C("Level Loader V2.0", $"Background size = {CurrentLevel.BackgroundSize.X},{CurrentLevel.BackgroundSize.Y}\n");
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
                Error.Throw(null, ErrorSeverity.FatalError, $"Fatal level loading error - error converting between types!\n\n{err}", "BootNow! Fatal Error", 83);
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
