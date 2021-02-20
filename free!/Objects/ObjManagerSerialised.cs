using Emerald.Core;
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

namespace Free
{

    /// <summary>
    /// © 2019-2021 avant-gardé eyes
    /// 
    /// ObjManagerSerialised.cs
    /// 
    /// Serialised object manager for K19
    /// 
    /// February 17, 2021
    /// 
    /// Engine Version 4.0
    /// </summary>
    public class ObjManagerSerialised
    {
        public static string DEBUG_COMPONENT_NAME = "Schema-based Object Loader";

        
        public RootObjectCollection NewObjectList { get; set; }
        
        public bool LoadObjects()
        {
            if (!ObjectLoader_ValidateObjects()) return false;
            return ObjectLoader_LoadObjectList(); 
        }

        /// <summary>
        /// Validates the RootObject list; under construction - need to create a ValidationResult class
        /// </summary>
        /// <returns></returns>
        private bool ObjectLoader_ValidateObjects()
        {
            string SerialisationPath = @"Data\Objects.xml";

            if (!File.Exists(SerialisationPath)) return false;
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, "Validating global object list...");
            XmlReaderSettings ReaderSettings = new XmlReaderSettings();

            XmlSchemaSet SchemaCollection = new XmlSchemaSet();
            SchemaCollection.Add(null, @"Content\Schema\Objects.xsd");
            ReaderSettings.ValidationType = ValidationType.Schema;
            ReaderSettings.ValidationEventHandler += LoadObjects_OnFail;
            ReaderSettings.Schemas.Add(SchemaCollection);

            XmlReader ObjectReader = XmlReader.Create(SerialisationPath, ReaderSettings);

            while (ObjectReader.Read())
            {
                // yes, you actually have to do this
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, "Reading and validating node: ");
            }


            return true;
        }

        private bool ObjectLoader_LoadObjectList()
        {
            XmlSerializer Deserialiser = new XmlSerializer(typeof(RootObjectCollection));

            try
            {
                FileStream FS = new FileStream(@"Content\Objects.xml", FileMode.Open);

                XmlReader XR = XmlReader.Create(FS);

                Deserialiser.Deserialize(XR);

            }
            catch (DirectoryNotFoundException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Fatal error deserialising object list. Cannot find Objects.xml. This game is invalid.")
            }

            return true; 
        }

        private void LoadObjects_OnFail(object sender, ValidationEventArgs EventArgs)
        {
            switch (EventArgs.Severity)
            {
                case XmlSeverityType.Warning:
                    SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Global object definition schema validation threw a warning: {EventArgs.Exception.Message} - Line {EventArgs.Exception.LinePosition} in {EventArgs.Exception.SourceUri}");
                    return; 
                case XmlSeverityType.Error:
                    Error.Throw(EventArgs.Exception, ErrorSeverity.FatalError, "Schema validation failure!", "Fatal", 500);
                    return; // does not return because exit
            }

        }
    }
}
