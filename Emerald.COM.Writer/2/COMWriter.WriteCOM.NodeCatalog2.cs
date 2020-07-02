using Emerald.COM2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Emerald.COM2.Writer
{
    public partial class COMWriter2
    {
       internal COMNodeCatalog2 WriteNodeCatalog2(string ComFileName, COMCatalog2 ComCat2)
        {
            try
            {
                // something bad happened and we fucked up
                if (!File.Exists(ComFileName)) return null;

                COMNodeCatalog2 COMNodeCatalog2 = ComBuildNodeCatalog2(ComCat2);

                using (BinaryWriter BW = new BinaryWriter(new FileStream(ComFileName, FileMode.Open)))
                {

                    // Seek to the end of the file catalog. 
                    BW.Seek(ComCat2.Endpoint, SeekOrigin.Begin);
                    COMNodeCatalog2.Endpoint = ComCat2.Endpoint;

                    BW.Write(COMNodeCatalog2.NodeCatalogBegin);
                    COMNodeCatalog2.Endpoint += 3;

                    // Write the node catalog

                    foreach (COMNode2 ComNode in COMNodeCatalog2.Nodes)
                    {
                       
                        // Increment the endpoint. This is so we actually know where to put the files.
                        COMNodeCatalog2.Endpoint += 2;

                        // V2.0.104.0 enhancement: Do this shortening stuff if node name length < 4 bytes and there is already a node with this name. Long-node XML files are improved here.

                        foreach (COMNode2 ComNode2 in COMNodeCatalog2.Nodes)
                        {
                            // no point doing this optimisation if it's less than five bytes - 4 bytes = no difference, <4 bytes = wasted space
                            if (ComNode.NodeName.Length < 3) break;

                            if (ComNode.NodeName == ComNode2.NodeName && ComNode.NodeID > ComNode2.NodeID)
                            {
                                // set this to a very particular string - the reader will pick this up
                                ComNode.NodeID = ComNode2.NodeID;
                                ComNode.NodeName = null;
                                COMNodeCatalog2.Endpoint -= 2; 
                                break; 
                            }
                        }

                        // Skip nodes we don't need
                        if (ComNode.NodeName != null)
                        {
                            // Write the node ID and name
                            BW.Write(ComNode.NodeID);
                            BW.Write(ComNode.NodeName);

                            // Increment the endpoint.
                            COMNodeCatalog2.Endpoint += ComNode.NodeName.Length + 1;

                            // Write the node parent ID
                            BW.Write(ComNode.NodeParentID);

                            COMNodeCatalog2.Endpoint += 2;
                        } 


                    }

                    BW.Write(COMNodeCatalog2.NodeCatalogEnd);

                    COMNodeCatalog2.Endpoint += 3;

                }

                return COMNodeCatalog2;
            }
            catch (FileNotFoundException err)
            {
                MessageBox.Show($"An error occurred. SOMEHOW we didn't find the file we just wrote to.\n\n{err}", "Emerald Game Engine Error 47", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (PathTooLongException err)
            {
                MessageBox.Show($"An error occurred. The path is too long - please shorten it to less than 260 characters. To fix this problem, uninstall Windows.\n\n{err}", "Emerald Game Engine Error 48", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (IOException err)
            {
                MessageBox.Show($"An error occurred. An IOException occurred while writing a Com2 node catalog.\n\n{err}", "Emerald Game Engine Error 49", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }

    }
}
