using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows; 

namespace Emerald.COM2.Writer
{
    public partial class COMWriter2
    {
        internal COMNodeCatalog2 ComBuildNodeCatalog2(COMCatalog2 CompressFilePath)
        {
            try
            {
                COMNodeCatalog2 COMNodeCat = new COMNodeCatalog2();
                COMNodeCat.Nodes = new List<COMNode2>();

                foreach (COMCatalogEntry2 CatEntry in CompressFilePath.COMCatalogEntries)
                {
                    XmlDocument XDoc = new XmlDocument();

                    XDoc.Load(CatEntry.FileNameFull);

                    XmlNode XmlRootNode = XDoc.FirstChild;

                    while (XmlRootNode.Name.Contains("#")) // Used by .NET XML parser to show things like the document, comments, the end of the document...etc
                    {
                        // Check that there aren't just comments
                        if (XmlRootNode.NextSibling == null)
                        {
                            // Throw an error
                            MessageBox.Show($"An error occurred reading the XML during an attempt to build a CompressML node catalog\nThere are no valid XML nodes to parse.", "Emerald Game Engine Error 51", MessageBoxButton.OK, MessageBoxImage.Error);
                            return null;
                        }

                        XmlRootNode = XmlRootNode.NextSibling;
                    }

                    COMNodeCat = IterateNodes(COMNodeCat, XmlRootNode);

                }

                return COMNodeCat;
                
            }
            catch (FileNotFoundException err)
            {
                MessageBox.Show($"Cannot find an XML file used while trying to build a CompressML node catalog.\n\n{err}", "Emerald Game Engine Error 52", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (IOException err)
            {
                MessageBox.Show($"An I/O error occurred reading the XML during an attempt to build a CompressML node catalog.\n\n{err}", "Emerald Game Engine Error 52", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (XmlException err)
            {
                MessageBox.Show($"An error occurred reading the XML during an attempt to build a CompressML node catalog.\n\n{err}", "Emerald Game Engine Error 46", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }
        }

        internal COMNodeCatalog2 IterateNodes(COMNodeCatalog2 Com2_Catalog, XmlNode XCore)
        {
            if (!XCore.HasChildNodes)
            {
                return Com2_Catalog; 
            }
            else
            {
                foreach (XmlNode XChild in XCore.ChildNodes)
                {
                    COMNode2 COMNode2 = new COMNode2();
                    COMNode2.NodeXml = XChild;
                    COMNode2.NodeID = Convert.ToInt16(Com2_Catalog.Nodes.Count);
                    // Add the node name
                    COMNode2.NodeName = XChild.Name;

                    // bad terrible no good hack 
                    if (COMNode2.NodeName == "#text") continue;

                    // Add the inner text
                    COMNode2.NodeInnerText = XChild.InnerText;

                    foreach (COMNode2 COMParents in Com2_Catalog.Nodes)
                    {
                        if (COMParents.NodeXml == XCore)
                        {
                            // check that we actually do have the parent 
                            COMNode2.NodeParentID = COMParents.NodeID; 
                        }
                    }

                    Com2_Catalog.Nodes.Add(COMNode2);

                }

                // Create a second foreach loop to do this
                foreach (XmlNode XChild in XCore.ChildNodes)
                {
                    IterateNodes(Com2_Catalog, XChild);
                }
                

            }

            return Com2_Catalog;
        }
    }
}
