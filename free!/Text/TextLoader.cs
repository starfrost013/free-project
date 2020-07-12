using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Media;
/// <summary>
/// 
/// TextLoader.cs
/// 
/// Created: 2019-12-07
///
/// Modified: 2019-12-07
/// 
/// Version: 1.00
/// 
/// Purpose: Loads text from Text.xml
/// 
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        public void LoadTextXml()
        {
            //todo: loadxmlandverify?

            // Hack but this will die soon
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("[WARNING] Text XML is deprecated. No logging will be provided for this system.");

            Console.ForegroundColor = ConsoleColor.Gray;

            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load("Text.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Text")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // yeah

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    // load.

                    // defaults
                    AGTextBlock AGText = LoadText("Text", 0, 0);
                    SetTextColour(AGText, new Color { R = 0, G = 0, B = 0, A = 255 }); // rgba 255.
                    SetTextFontSize(AGText, 11);

                    if (XmlNode.Attributes.Count > 0) // dont parse attributes that dont exist
                    {
                        XmlAttributeCollection XmlAttributes = XmlNode.Attributes;

                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name)
                            {
                                case "name":
                                    SetTextInternalName(AGText, XmlAttribute.Value);
                                    continue;
                                case "text":
                                    SetText(AGText, XmlAttribute.Value);
                                    continue;
                                case "initpos":
                                    double[] initpos = Array.ConvertAll<string, double>(XmlAttribute.Value.Split(','), Double.Parse); // split and convert to double
                                    MoveText(AGText, initpos[0], initpos[1]);
                                    continue;
                                case "color": // No variant of english left out of the text loading party!
                                case "colour":
                                    string[] colourarray = XmlAttribute.Value.Split(',');
                                    SetTextColour(AGText, new Color { R = Convert.ToByte(colourarray[0]), G = Convert.ToByte(colourarray[1]), B = Convert.ToByte(colourarray[2]), A = Convert.ToByte(colourarray[3])}); // convert to byte.
                                    continue;
                                case "bgcolor":
                                case "bgcolour":
                                    string[] bgcolourarray = XmlAttribute.Value.Split(',');
                                    SetTextColourBg(AGText, new Color { R = Convert.ToByte(bgcolourarray[0]), G = Convert.ToByte(bgcolourarray[1]), B = Convert.ToByte(bgcolourarray[2]), A = Convert.ToByte(bgcolourarray[3]) }); // convert to byte.
                                    continue;
                                case "style":
                                    SetTextStyle(AGText, (FontStyle)new FontStyleConverter().ConvertFromString(XmlAttribute.Value)); // iterate through the fontstyle array from wpf and compare it against the string.
                                    continue;
                                case "size":
                                    SetTextFontSize(AGText, Convert.ToInt32(XmlAttribute.Value));
                                    continue;
                                case "font":
                                    SetTextFont(AGText, (FontFamily)new FontFamilyConverter().ConvertFromString(XmlAttribute.Value)); // aaaa
                                    continue;

                            }
                        }
                    }
                }
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading Text.xml: Invalid input was received when attempting to convert to an enum.", "avant-gardé engine", 30);
                return;
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading Text.xml: Invalid input was received", "avant-gardé engine", 29);
                return;
            }
            catch (OverflowException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred while loading Text.xml: A value for the colour or background colour below 0 or above 255 was found. Please rectify this issue.", "avant-gardé engine", 31);
                return;
            }
            
        }
    }
}
