using Emerald.Utilities;
using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Media;

namespace Emerald.Core
{
    /// <summary>
    /// Emerald Game Engine Settings © 2020 avant-gardé eyes.
    /// 
    /// Ported from Emerald Lite/NetEmerald/Emerald Mini with enhancements 
    /// </summary>
    /// 
    public static class GameSettings 
    {


        public static class SettingsAPI
        {
            internal static XmlNode LoadApplicationSettingsXmlGetNode()
            {
                try
                {
                    // Priscilla 442 - simplify
                    XmlDocument XDoc = LoadApplicationSettingsXml();

                    XmlNode XRoot = GetFirstNode(XDoc);

                    return XRoot;
                }
                // can't load serversettings.xml because it doesn't exist
                catch (FileNotFoundException err)
                {
                    MessageBox.Show($"Uh oh, something bad happened!! GenerateGameApplicationSettings() failed. Error 10!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                // some error in parsing the xml
                catch (IOException err)
                {
                    //NetEmeraldCore.ThrowError("IOException while loading xml!", 6, err);
                    MessageBox.Show($"Temp error. IOException occurred while loading settings xml. Error 9!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null; // handle nicely.
                }
                catch (XmlException err)
                {
                    MessageBox.Show($"Temp error. ServerApplicationSettings.xml corrupted or maflormed! Error 18!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

            internal static XmlDocument LoadApplicationSettingsXml()
            {
                try
                {
                    XmlDocument XDoc = new XmlDocument();

                    string ApplicationSettingsFile = @"Data\Settings.xml";

                    if (!File.Exists(ApplicationSettingsFile))
                    {
                        Error.Throw("Fatal error", "Settings.xml not found!", ErrorSeverity.FatalError, 110);
                    }

                    XDoc.Load(ApplicationSettingsFile);

                    return XDoc;
                }
                // can't load serversettings.xml because it doesn't exist
                catch (FileNotFoundException err)
                {
                    MessageBox.Show($"Uh oh, something bad happened!! GenerateGameApplicationSettings() failed. Error 10!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                // some error in parsing the xml
                catch (IOException err)
                {
                    //NetEmeraldCore.ThrowError("IOException while loading xml!", 6, err);
                    MessageBox.Show($"Temp error. IOException occurred while loading settings xml. Error 9!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null; // handle nicely.
                }
                catch (XmlException err)
                {
                    MessageBox.Show($"Temp error. ServerApplicationSettings.xml corrupted or maflormed! Error 18!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

            /// <summary>
            /// This is used for write access to ApplicationSettings.xml.  
            /// </summary>
            /// <returns></returns>
            internal static XmlNode GetFirstNode(XmlDocument XDoc, string VerifyString = "ApplicationSettings")
            {
                XmlNode XRoot = XDoc.FirstChild;

                // Feature: if we pass null as our VerifyString, we don't verify and just return the first node
                if (VerifyString == null) return XRoot;

                while (XRoot.Name != VerifyString)
                {
                    //get the next sibling until it has the name we want
                    XRoot = XRoot.NextSibling;
                }

                return XRoot;
            }


            /// <summary>
            /// Internal Api for getting nodes from the root node obtained by using LoadApplicationSettingsXml()/
            /// </summary>
            /// <param name="XRoot">The root node of the ServerApplicationSettings Xml.</param>
            /// <param name="NodeName">The name of the setting to acquire.</param>
            /// <returns></returns>
            private static XmlNode GetNode(XmlNode XRoot, string NodeName)
            {
                //iterate through the metadata 
                XmlNodeList XMetadata = XRoot.ChildNodes;

                foreach (XmlNode XMetadataElement in XMetadata)
                {
                    // get the name. Switch statements don't support non-constant values
                    if (XMetadataElement.Name == NodeName)
                    {
                        return XMetadataElement;
                    }

                }
                return null; // node not found or incorrect node
            }

            /// <summary>
            /// Obtains a double setting.
            /// </summary>
            /// <param name="ApplicationSettingsElement">The name of the setting to acquire.</param>
            /// <returns></returns>
            public static bool GetBool(string ApplicationSettingsElement)
            {
                try
                {
                    XmlNode XRoot = LoadApplicationSettingsXmlGetNode();
                    XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                    bool Val = false;

                    //throw an error if xelement is null
                    if (XElement == null || XRoot == null) return Val;

                    Val = Convert.ToBoolean(XElement.InnerText);

                    XRoot = null;
                    XElement = null;

                    return Val;
                }
                catch (FormatException err)
                {
                    Error.Throw("An error has occurred.", $"Error converting string to boolean while loading xml!\n\n{err}", ErrorSeverity.Error, 11);
                    Application.Current.Shutdown(11);
                    return false;
                }
            }

            /// <summary>
            /// Obtains a double setting.
            /// </summary>
            /// <param name="ApplicationSettingsElement">The name of the setting to acquire.</param>
            /// <returns></returns>
            public static double GetDouble(string ApplicationSettingsElement)
            {
                try
                {
                    XmlNode XRoot = LoadApplicationSettingsXmlGetNode();
                    XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                    double Val = 0;

                    //throw an error if xelement is null
                    if (XElement == null || XRoot == null) return Val;

                    Val = Convert.ToDouble(XElement.InnerText);

                    return Val;
                }
                catch (FormatException err)
                {
                    MessageBox.Show($"Error converting string to double while loading xml! Error 14!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown(14);
                    return -1;
                }
            }

            /// <summary>
            /// Obtains an int setting.
            /// </summary>
            /// <param name="ApplicationSettingsElement">The name of the setting to acquire.</param>
            /// <returns></returns>
            public static int GetInt(string ApplicationSettingsElement)
            {
                try
                {
                    XmlNode XRoot = LoadApplicationSettingsXmlGetNode();
                    XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                    int Val = 0;

                    // throw an error if xelement is null
                    if (XElement == null || XRoot == null) return Val;

                    Val = Convert.ToInt32(XElement.InnerText);

                    return Val;
                }
                catch (FormatException err)
                {
                    Error.Throw("An error has occurred.", $"Error converting string to int while loading xml!\n\n{err}", ErrorSeverity.Error, 16);

                    Application.Current.Shutdown(16); // ApplicationSettings may be corrupted

                    return -1;
                }
            }

            /// <summary>
            /// Obtains a string setting.
            /// </summary>
            /// <param name="ApplicationSettingsElement">The name of the setting to acquire.</param>
            public static string GetString(string ApplicationSettingsElement)
            {
                XmlNode XRoot = LoadApplicationSettingsXmlGetNode();
                XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                string Val = "";

                // throw an error if xelement is null
                if (XElement == null || XRoot == null) return Val;

                Val = XElement.InnerText;

                return Val;

            }

            /// <summary>
            /// Obtains a point setting.
            /// </summary>
            /// <param name="ApplicationSettingsElement">The element name to grab.</param>
            /// <returns></returns>
            public static Point GetPoint(string ApplicationSettingsElement)
            {
                XmlNode XRoot = LoadApplicationSettingsXmlGetNode();
                XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                Point XY = new Point(0, 0);
                // throw an error if xelement is null
                if (XElement == null || XRoot == null) return XY;


                XY = XElement.InnerText.SplitXY();

                return XY;
            }

            public static Color GetColour(string ApplicationSettingsElement)
            {
                XmlNode XRoot = LoadApplicationSettingsXmlGetNode();
                XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                Color RGB = new Color { A = 255, R = 255, G = 255, B = 255 };

                if (XElement == null || XRoot == null) return RGB;

                RGB = XElement.InnerText.SplitRGB();

                return RGB;
            }


            /// <summary>
            /// Saves a setting to ApplicationSettings.xml.
            /// </summary>
            /// <param name="ApplicationSettingsElement">The name of the setting to change.</param>
            /// <param name="ApplicationSettingsValue">The value to change it to.</param>
            /// <returns></returns>
            public static bool SetSetting(string ApplicationSettingsElement, string ApplicationSettingsValue)
            {
                try
                {
                    Logging.Log($"[Saving settings] ApplicationSettings {ApplicationSettingsElement} to {ApplicationSettingsValue}...");
                    // Load settings and get the first node.
                    XmlDocument XDoc = LoadApplicationSettingsXml();
                    XmlNode XRoot = GetFirstNode(XDoc);

                    // Find the setting we need.
                    XmlNode XElement = GetNode(XRoot, ApplicationSettingsElement);

                    if (XElement == null || XRoot == null)
                    {
                        Error.Throw("Error!", "An error occurred while saving settings. Either settings.xml is empty or we attempted to modify an invalid setting.", ErrorSeverity.Error, 220);
                        return false;
                    }

                    // Update its inner text.
                    XElement.InnerText = ApplicationSettingsValue;

                    // Save it.
                    XDoc.Save($@"{Directory.GetCurrentDirectory()}\ApplicationSettings.xml");

                    return true;
                }
                catch (XmlException err)
                {
                    // An error occurred while saving - return false. 
                    Error.Throw("Error!", $"An error occurred while saving settings.\n\n{err}", ErrorSeverity.Error, 221);
                    return false;
                }
            }

        }

        internal static XmlNode LoadSettingsXml()
        {
            try
            {

                string SettingsFilePath = $@"{GlobalSettings.CurrentGame.ContentFolderLocation}\Settings.xml";

                // TEMP
                if (!File.Exists(SettingsFilePath)) return null;
                // END TEMP

                XmlDocument XDoc = new XmlDocument();

                if (!File.Exists(SettingsFilePath))
                {
                    // TEMP UNTIL PROPER RESULT/VALIDATION APIS
                    return null; // don't do that..
                    // END TEMP UNTIL PROPER RESULT/VALIDATION APIS
                }

                XDoc.Load(SettingsFilePath);

                // get the xmlnode
                XmlNode XRoot = XDoc.FirstChild;

                while (XRoot.Name != "Settings")
                {
                    //get the next sibling until it has the name we want
                    XRoot = XRoot.NextSibling;
                }

                
                return XRoot;
            }
            // can't load serversettings.xml because it doesn't exist
            catch (FileNotFoundException err)
            {
                MessageBox.Show($"Uh oh, something bad happened!! GenerateGameSettings() failed. Error 10!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            // some error in parsing the xml
            catch (IOException err)
            {
                //NetEmeraldCore.ThrowError("IOException while loading xml!", 6, err);
                MessageBox.Show($"Temp error. IOException occurred while loading settings xml. Error 9!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // handle nicely.
            }
            catch (XmlException err)
            {
                MessageBox.Show($"Temp error. Settings.xml corrupted or maflormed! Error 18!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// Internal Api for getting nodes from the root node obtained by using LoadSettingsXml()/
        /// </summary>
        /// <param name="XRoot">The root node of the ServerSettings Xml.</param>
        /// <param name="NodeName">The name of the setting to acquire.</param>
        /// <returns></returns>
        private static XmlNode GetNode(XmlNode XRoot, string NodeName)
        {
            //iterate through the metadata 
            XmlNodeList XMetadata = XRoot.ChildNodes;

            foreach (XmlNode XMetadataElement in XMetadata)
            {
                // get the name. Switch statements don't support non-constant values
                if (XMetadataElement.Name == NodeName)
                {
                    return XMetadataElement;
                }

            }
            return null; // node not found or incorrect node
        }

        /// <summary>
        /// Obtains a double setting.
        /// </summary>
        /// <param name="SettingsElement">The name of the setting to acquire.</param>
        /// <returns></returns>
        public static bool GetBool(string SettingsElement)
        {
            try
            {
                XmlNode XRoot = LoadSettingsXml();
                XmlNode XElement = GetNode(XRoot, SettingsElement);

                //throw an error if xelement is null
                if (XElement == null) MessageBox.Show($"Temp error. Attempted to load invalid setting boolean! Error 12!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error); 

                bool Val = Convert.ToBoolean(XElement.InnerText);

                XRoot = null;
                XElement = null;

                return Val;
            }
            catch (FormatException err)
            {
                MessageBox.Show($"Temp error. Error converting string to boolean while loading xml! Error 11!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Obtains a double setting.
        /// </summary>
        /// <param name="SettingsElement">The name of the setting to acquire.</param>
        /// <returns></returns>
        public static double GetDouble(string SettingsElement)
        {
            try
            {
                XmlNode XRoot = LoadSettingsXml();
                XmlNode XElement = GetNode(XRoot, SettingsElement);

                //throw an error if xelement is null
                if (XElement == null) MessageBox.Show($"Temp error. Attempted to load invalid setting double! Error 13!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);

                double Val = Convert.ToDouble(XElement.InnerText);

                XRoot = null;
                XElement = null;

                return Val;
            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to double while loading xml! Error 14!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        /// <summary>
        /// Obtains an int setting.
        /// </summary>
        /// <param name="SettingsElement">The name of the setting to acquire.</param>
        /// <returns></returns>
        public static int GetInt(string SettingsElement)
        {
            try
            {
                XmlNode XRoot = LoadSettingsXml();
                XmlNode XElement = GetNode(XRoot, SettingsElement);
                if (XElement == null) MessageBox.Show($"Temp error. Attempted to load invalid setting int! Error 15!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);

                int Val = Convert.ToInt32(XElement.InnerText);

                XRoot = null;
                XElement = null;

                return Val;
            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to int while loading xml! Error 16!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);

                return -1;
            }
        }

        /// <summary>
        /// Obtains a string setting.
        /// </summary>
        /// <param name="SettingsElement">The name of the setting to acquire.</param>
        public static string GetString(string SettingsElement)
        {
            XmlNode XRoot = LoadSettingsXml();
            XmlNode XElement = GetNode(XRoot, SettingsElement);

            if (XElement == null)
            {
                MessageBox.Show($"Temp error. Attempted to load invalid setting string! Error 17!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }
            

            string Val = XElement.InnerText;

            XRoot = null;
            XElement = null;

            return Val;

        }

        /// <summary>
        /// Obtains a point setting.
        /// </summary>
        /// <param name="SettingsElement">The element name to grab.</param>
        /// <returns></returns>
        public static SDLPoint GetPoint(string SettingsElement)
        {
            XmlNode XRoot = LoadSettingsXml();
            XmlNode XElement = GetNode(XRoot, SettingsElement);

            // throw an error if xelement is null
            if (XElement == null)
            {
                MessageBox.Show($"Temp error. Attempted to load invalid setting point! Error 18!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(18);
            }

            SDLPoint XY = XElement.InnerText.SplitXYV2();

            return XY;
        }

        public static Color GetColour(string SettingsElement)
        {
            XmlNode XRoot = LoadSettingsXml();
            XmlNode XElement = GetNode(XRoot, SettingsElement);

            if (XElement == null)
            {
                MessageBox.Show($"Temp error. Attempted to load invalid setting colour! Error 19!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(19);
            }


            Color RGB = XElement.InnerText.SplitRGB();

            return RGB;
        }

    }
}
