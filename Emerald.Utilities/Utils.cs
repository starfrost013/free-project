using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection; 
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

/// <summary>
/// TODO: MERGE THIS DLL AND EMERALD.CORE.EVENTMANAGER INTO EMERALD.CORE
/// </summary>
namespace Emerald.Utilities
{
    public static class Utils
    {
        internal static void LogFile(string Component, string Text)
        {
            // open a binary writer to the emerald log and then write stuff to it (todo delete stuff)
            using (BinaryWriter BW = new BinaryWriter(new FileStream("Emerald.log", FileMode.OpenOrCreate)))
            {
                // Temporary code.
                BW.Seek(0, SeekOrigin.End);
                BW.Write($"[Emerald {Component} @ {DateTime.Now}]: {Text}\n");
            }

            return; 
        }

        /// <summary>
        /// Logs to the Win32-allocated console and/or the .NET output and the Emerald.log file.
        /// </summary>
        /// <param name="Component">The component of Emerald that is producing the log file.</param>
        /// <param name="Text">The text to log.</param>
        public static void LogDebug(string Component, string Text)
        {
#if DEBUG
            Console.WriteLine($"[Emerald {Component} @ {DateTime.Now}]: {Text}"); // console will be allocated for this process by EngineCore
#endif
            LogFile(Component, Text);


        }

        

        public static Point SplitXY(this String SplitString)
        {
            try
            {
                string[] Split = SplitString.Split(',');

                if (Split.Length != 2) MessageBox.Show("Error converting string to position - must be 2 positions supplied", "Error 19", MessageBoxButton.OK, MessageBoxImage.Error);

                     // return -1, -1 if failed. 

                Point XY = new Point();

                // convert the string parts to a Point
                XY.X = Convert.ToDouble(Split[0]);
                XY.Y = Convert.ToDouble(Split[1]);
                return XY;
            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to position - invalid position\n\n{err}", "Error 20", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Point { X = -1, Y = -1 };
            }
        }

        public static SDLPoint SplitXYV2(this String SplitString)
        {
            try
            {
                string[] Split = SplitString.Split(',');

                if (Split.Length != 2) MessageBox.Show("Error converting string to position - must be 2 positions supplied", "Error 19", MessageBoxButton.OK, MessageBoxImage.Error);

                // return -1, -1 if failed. 

                SDLPoint XY = new SDLPoint(Convert.ToDouble(Split[0]), Convert.ToDouble(Split[1]));

                return XY;
            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to position - invalid position\n\n{err}", "Error 20", MessageBoxButton.OK, MessageBoxImage.Error);
                return new SDLPoint(-1, -1);
            }
        }

        public static string ToStringEmerald(this Point XPoint)
        {
            return $"{XPoint.X},{XPoint.Y}";
        }

        public static Color SplitRGB(this String SplitString)
        {
            try
            {
                // Split the string by comma
                string[] Split = SplitString.Split(',');

                // RGB has three components - error out if we have less than three
                if (Split.Length != 3) MessageBox.Show("Error converting string to RGB colour - must be 2 positions supplied", "Emerald Game Engine Error 40", MessageBoxButton.OK, MessageBoxImage.Error);

                Color RGB = new Color();

                // For the track maker we don't need to set alpha. For free! and Tiralen we may need to
                RGB.A = 0xFF;

                // Convert to RGB
                RGB.R = Convert.ToByte(Split[0]);
                RGB.G = Convert.ToByte(Split[1]);
                RGB.B = Convert.ToByte(Split[2]);

                // Return our generated colour.
                return RGB;


            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to position - invalid position\n\n{err}", "Emerald Game Engine Error 41", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Color { A = 1, R = 1, B = 0, G = 2 };
            }

        }

        /// <summary>
        /// Splits a string into an Emerald GameDLL version. Modified 2020-04-30 for scriptdomains.
        /// </summary>
        /// <param name="SplitString">The string to split.</param>
        /// <returns></returns>
        public static List<int> SplitVersion(this String SplitString)
        {
            try
            {
                string[] _1 = SplitString.Split('.');

                // If we don't have 3 versions then error out
                if (_1.Length != 4)
                {
                    MessageBox.Show($"Error converting string to version - must be 4 version components supplied", "Emerald Game Engine Error 42", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                List<int> Version = new List<int>();

                // Build the version

                foreach (string _2 in _1)
                {
                    Version.Add(Convert.ToInt32(_2)); 
                }

                return Version;
            }

            // Error condition: Attempted to convert an invalid portion of a string. 
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to version - invalid version information\n\n{err}", "Emerald Game Engine Error 56", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// Convert from string to version. 
        /// </summary>
        /// <param name="SplitString"></param>
        /// <returns></returns>
        public static EngineVersion ToVersion(this String SplitString)
        {
            try
            {
                string[] _1 = SplitString.Split('.');

                // If we don't have 3 versions then error out
                if (_1.Length != 4)
                {
                    MessageBox.Show($"Error converting string to version - must be 4 version components supplied", "Emerald Game Engine Error 60", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                EngineVersion Version = new EngineVersion();

                // set the version information
                Version.Major = Convert.ToInt32(_1[0]);
                Version.Minor = Convert.ToInt32(_1[1]);
                Version.Build = Convert.ToInt32(_1[2]);
                Version.Revision = Convert.ToInt32(_1[3]);

                return Version;

            }
            catch (FormatException err)
            {
                MessageBox.Show($"Error converting string to version - invalid version component supplied!\n\n{err}", "Emerald Game Engine Error 61", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static List<string> InnerXml_Parse(this String InnerXml)
        {
            // xml preprocessing
            string[] _1 = InnerXml.Split('<');

            List<string> _2 = new List<string>();

            foreach (string _3 in _1)
            {
                string[] _4 = _3.Split('>');

                foreach (string _5 in _4)
                {
                    if (_5 == "" || _5.Contains(@"/")) continue; // skip the strings that are not like the other 
                    _2.Add(_5);
                }
            }

            return _2;

        }
        
        /// <summary>
        /// Returns the first XML node of this document.
        /// </summary>
        /// <param name="XmlDoc">UNUSED - EXTENSION METHOD</param>
        /// <returns></returns>
        public static XmlNode GetFirstNode(this XmlDocument XmlDoc)
        {
            // There are no child nodes, so throw an error.
            if (!XmlDoc.HasChildNodes)
            {
                MessageBox.Show("Internal Temporary Error: No child nodes when attempting to load the first node of an XmlDocument", "Emerald Game Engine Error 57", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }

            // Since we have ascertained that the XML does indeed have child nodes, get them. 

            return XmlDoc.FirstChild;// We can safely do this - if there are child nodes has already been checked, 
        }

        /// <summary>
        /// Returns the first XML node of this document and verifies that the node's name is VerifyString. If not, it loops through every first-level child to find it. If it does not find it, it throws an error.
        /// </summary>
        /// <param name="XmlDoc">UNUSED - EXTENSION METHOD</param>
        /// <param name="VerifyName">The name to verify</param>
        /// <returns></returns>
        public static XmlNode GetFirstNode(this XmlDocument XmlDoc, string VerifyName)
        {
            // There are no child nodes, so throw an error.
            if (!XmlDoc.HasChildNodes)
            {
                MessageBox.Show("Internal Temporary Error: No child nodes when attempting to load the first node of an XmlDocument", "Emerald Game Engine Error 58", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Since we have ascertained that the XML does indeed have child nodes, get them. 

            XmlNode XChild = XmlDoc.FirstChild;

            // Check that we have the node name specified in VerifyName

            while (XChild.Name != VerifyName)
            {
                // If the next sibling is null, this means that we have reached the last child without any matches, so error out
                if (XChild.NextSibling == null)
                {
                    MessageBox.Show($"No nodes with name {VerifyName} found!", "Emerald Game Engine Error 59", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                XChild = XChild.NextSibling;
            }

            return XChild; // We can safely do this - if there are child nodes has already been checked, 
        }

        public static string GetVersion()
        {
            FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location);
            return FVI.ProductVersion;
        }

        /// <summary>
        /// This may be required for SDL. Trying this.
        /// </summary>
        /// <returns></returns>
        public static string ConvertToSingleSlash(string Str) // make extension method?
        {
            return Str.Replace(@"\\", @"\");
        }
    }
}
