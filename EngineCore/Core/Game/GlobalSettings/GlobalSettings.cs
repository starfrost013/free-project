using Emerald.Core.StaticSerialiser;
using Emerald.Utilities;
using System;
using System.Collections.Generic;
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

        public new static string DEBUG_COMPONENT_NAME = "Global Settings Loader";

        [XmlElement("CurrentGameDefinitionPath")]
        public static string CurrentGameDefinitionPath { get; set; }
        /// <summary>
        /// The current game.
        /// </summary>
        public static GameDefinition CurrentGame { get; set; }

        public static string GetContentFolderPath() => CurrentGame.ContentFolderLocation;
        public static void SetContentFolderPath(string ContentFolderPathName) => CurrentGame.ContentFolderLocation = ContentFolderPathName;

        public static void Load()
        {
            Load_Validate();
            Load_Serialise();
            
        }

        private static void Load_Validate()
        {
            // loading code goes here...lol.

            string GlobalSettingsPath = @"Content\GlobalSettings.xml";

            SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, "Logging");
            // Create a schema set for the global settings schema.
            XmlSchemaSet XRS = new XmlSchemaSet();
            XRS.Add(null, @"Content\Schema\GlobalSettings.xsd");

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

    }
}
