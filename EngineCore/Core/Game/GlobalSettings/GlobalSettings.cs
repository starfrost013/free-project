using Emerald.Core.StaticSerialiser;
using Emerald.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema; 
using System.Xml.Serialization;

namespace Emerald.Core
{
    [XmlRoot("GlobalSettings")]
    /// <summary>
    /// Emerald V4.0.1518.0 (2021-02-18)
    /// 
    /// Engine Global Settings class
    /// 
    /// Holds global settings for Emerald version 4.x
    /// 
    /// 2021-02-18
    /// 
    /// make not static?
    /// </summary>
    public class GlobalSettings : EmeraldComponent
    {
        public new static int API_VERSION_MAJOR = 0;
        public new static int API_VERSION_MINOR = 3;
        public new static int API_VERSION_REVISION = 0;

        public new static string DEBUG_COMPONENT_NAME = "Global Settings Loader";

        [XmlElement("_currentgamedefinitionpath")]
        private static string _currentgamedefinitionpath { get; set; }
        
        public static string CurrentGameDefinitionPath
        {
            get
            {
                return _currentgamedefinitionpath;
            }
            set
            {
                if (value.Length == 0 || !value.Contains('\\'))
                {
                    // TEMP
                    SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, "Attempted to set invalid CurrentGameDefinitionPath!");
                }
                else
                {
                    _currentgamedefinitionpath = value; 
                }
            }
        }
        /// <summary>
        /// The current game.
        /// </summary>
        public static GameDefinition CurrentGame { get; set; }

        /// <summary>
        /// Are the GlobalSettings loaded?
        /// </summary>
        public static bool IsLoaded { get; set; }

        public static string GetContentFolderPath() => CurrentGame.ContentFolderLocation;
        public static void SetContentFolderPath(string ContentFolderPathName) => CurrentGame.ContentFolderLocation = ContentFolderPathName;

        public static void Load()
        {
            try
            {
                Load_Validate();
                Load_Serialise();
                Load_GameDefinition_Validate();
                Load_GameDefinition_Serialise();
                IsLoaded = true;
            }
            catch (ArgumentNullException err)
            {
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"[TEMP - UNTIL ERROR PORTED TO ENGINECORE] - Fatal Error\n{err}");
            }
            catch (FileNotFoundException err)
            {
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"[TEMP - UNTIL ERROR PORTED TO ENGINECORE] - Fatal Error {err}.\n\nCouldn't find a critical file!");
            }
            catch (InvalidOperationException err)
            {
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"[TEMP - UNTIL ERROR PORTED TO ENGINECORE - Fatal Error\n{err}: XML serialisation error!");
                Environment.Exit(0x300FDEAD);
            }
            catch (XmlSchemaException err)
            {
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"[TEMP - UNTIL ERROR PORTED TO ENGINECORE] - Fatal global settings serialisation error [GlobalSettings XML schema invalid - engine bug]:\n{err.Message}");
                Environment.Exit(0x400FDEAD);
            }
        }

        private static void Load_Validate()
        {
            // loading code goes here...lol.

            string GlobalSettingsPath = @"Content\GlobalSettings.xml";
            string SchemaPath = @"Content\Schema\GlobalSettings.xsd";

            SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Validating GlobalSettings XML {GlobalSettingsPath} using schema {SchemaPath}...");
            // Create a schema set for the global settings schema.
            XmlSchemaSet XRS = new XmlSchemaSet();
            XRS.Add(null, SchemaPath);

            // Create and set up the reader settings.
            XmlReaderSettings ReaderSettings = new XmlReaderSettings();
            ReaderSettings.ValidationType = ValidationType.Schema;
            ReaderSettings.Schemas.Add(XRS);
            ReaderSettings.ValidationEventHandler += Load_OnFail;

            // create an xml reader
            XmlReader XR = XmlReader.Create(GlobalSettingsPath, ReaderSettings);

            // yes, we need to do this
            // this is even in the MSDN code example
            // it's dumb
            while (XR.Read())
            {

            }

            XR.Close(); 
        }

        /// <summary>
        /// Loads a gamedefinition.
        /// 
        /// Is this the right place?
        /// </summary>
        private static void Load_GameDefinition_Validate() // USE RESULT CLASSES/INPUT VALIDATION
        {
            
            string GDSchemaFileName = @"Content\Schema\GameDefinition.xsd";

            SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Validating GameDefinition XML @ {CurrentGameDefinitionPath}  ");
            XmlSchemaSet GameDefinitionSchemaSet = new XmlSchemaSet();

            XmlReaderSettings ReaderSettings = new XmlReaderSettings();
            ReaderSettings.ValidationType = ValidationType.Schema;
            ReaderSettings.Schemas.Add(null, GDSchemaFileName);
            ReaderSettings.ValidationEventHandler += GDLoad_OnFail;

            XmlReader SchemaReader = XmlReader.Create(CurrentGameDefinitionPath, ReaderSettings);

            while (SchemaReader.Read())
            {
                // still dumb
            }

            SchemaReader.Close();

        }

        private static void Load_GameDefinition_Serialise()
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(GameDefinition));

            using (StreamReader SerialisedReader = new StreamReader(new FileStream(CurrentGameDefinitionPath, FileMode.Open)))
            {
                GameDefinition Temp = (GameDefinition)Serializer.Deserialize(SerialisedReader);
                if (Temp != null) CurrentGame = Temp;

                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME,"Loaded current GameDefinition\n");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Author: {CurrentGame.Author}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Content Folder Path: {CurrentGame.ContentFolderLocation}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Debug Enabled: {CurrentGame.DebugEnabled}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Description: {CurrentGame.Description}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Last Modified: {CurrentGame.LastModifiedDate}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Name: {CurrentGame.Name}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Version: {CurrentGame.Version}");
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Version status: {CurrentGame.Version_Status}");
            }
  
        }

        private static void Load_Serialise()
        {
            // Serialise and load the Global Settings XML

            string GlobalSettingsPath = @"Content\GlobalSettings.xml";

            // TODO: SUPPORT RECURSION
            StaticSerialisationResult SR = StaticSerialiser.StaticSerialiser.Deserialize(typeof(GlobalSettings), GlobalSettingsPath);

            if (!SR.Successful)
            {
                // TEMP
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"[TEMP - UNTIL ERROR PORTED TO ENGINECORE] - Fatal global settings serialisation error:\n{SR.Message}");
                Environment.Exit(0x100FDEAD);
            }
        }

        /// <summary>
        /// GlobalSettings schema validation failed. Do stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Load_OnFail(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Warning:
                    SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Warning validating global settings: {e.Exception} ");
                    return;
                case XmlSeverityType.Error:
                    SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Temp (error is not ported to EngineCore.dll yet: {e.Exception}) ");

                    // oof, dead. Throw a proper fatal error when the error system is ported.
                    Environment.Exit(0x000FDEAD);
                    return;
            }
        }

        private static void GDLoad_OnFail(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Warning:
                    SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Warning validating global definition: {e.Exception}");
                    return;
                case XmlSeverityType.Error:
                    SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Error [TEMP - ERROR NOT PORTED TO ENGINECORE.DLL] {e.Exception}");
                    Environment.Exit(0x300FDEAD);
                    return; 
            }
        }
    }
}
