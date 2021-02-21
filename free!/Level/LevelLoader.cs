using Emerald.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Free
{
    public partial class FreeSDL
    {
        public void BootNow_SetCurrentLevel(int LevelId)
        {
            // Clear all loaded scripts.
            ScriptingCore.ClearLoadedScripts();

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

            // temp
            SDL_Stage0_Init.SDLGame.CurrentScene.SDL_LoadLevel(level.BGPATH);

            // Error checking
            if (level == null) Error.Throw(null, ErrorSeverity.FatalError, "Fatal Error: Failed to set level!", "Fatal Error", 86);

            // read the level xml file
            // implement this code in the Level class? polymorphic addition.

            // 2020-06-05 FIX THIS SHIT CODE
            try
            {

                if (level.Size.X == 0 || level.Size.Y == 0) // we didn't put level size in the xml
                {
                    Error.Throw(new Exception("Level size not defined!"), ErrorSeverity.FatalError, "An error occurred while loading a level. This is most likely because its size was not defined.", "avant-gardé engine", 85);
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
                Error.Throw(err, ErrorSeverity.FatalError, "Attempted to load a non-existent level. This is most likely because a change level object attempted to trigger its interaction but the next level by ID doesn't exist (yet).", "avant-gardé engine", 7);
                return;
            }
            catch (PathTooLongException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Error loading level; PathTooLongException: WINDOWS SUCKS! WINDOWS SUCKS! WINDOWS SUCKS!", "avant-gardé engine", 7);
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
