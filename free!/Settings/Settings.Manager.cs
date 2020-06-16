using Emerald.Utilities; 
using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Media; 

namespace Free
{
    /// <summary>
    /// Emerald Game Engine Settings © 2020 avant-gardé eyes.
    /// 
    /// Ported from Emerald Lite/NetEmerald/Emerald Mini with enhancements 
    /// </summary>
    /// 
    public enum SettingFile { Engine, Game }; // engine/game level settings
    public static class GameSettings 
    {
        internal static XmlNode LoadSettingsXml()
        {
            try
            {
                XmlDocument XDoc = new XmlDocument();

                if (!File.Exists("Settings.xml"))
                {
                    GenerateSettings(); 
                }

                XDoc.Load("Settings.xml");

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
                MessageBox.Show($"Temp error. ServerSettings.xml corrupted or maflormed! Error 18!\n\n{err}", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private static void GenerateSettings()
        {
            // TEMP
            FileStream Fstream = File.Create("Settings.xml");
            Fstream.Close();
            // END TEMP
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
        public static Point GetPoint(string SettingsElement)
        {
            XmlNode XRoot = LoadSettingsXml();
            XmlNode XElement = GetNode(XRoot, SettingsElement);

            // throw an error if xelement is null
            if (XElement == null)
            {
                MessageBox.Show($"Temp error. Attempted to load invalid setting point! Error 18!", "An error has occurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(18);
            }

            Point XY = XElement.InnerText.SplitXY();

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
