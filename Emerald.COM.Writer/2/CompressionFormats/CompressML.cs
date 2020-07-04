using Emerald.COM2; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Emerald.COM2.Writer
{
    public class CompressML : ICompressionFormat
    {
        public string Name { get; set; }

        public bool Compress(string COMFileName, COMCatalog2 COMCat2, COMNodeCatalog2 NodeCatalog2)
        {
            // Compress the Com2 XML file using CompressML 

            foreach (COMCatalogEntry2 ComCatEntry in COMCat2.COMCatalogEntries)
            {
                // write more shit
                using (BinaryWriter BW = new BinaryWriter(new FileStream(COMFileName, FileMode.Open)))
                {
                    BW.BaseStream.Seek(NodeCatalog2.Endpoint, SeekOrigin.Begin);

                    foreach (COMNode2 ComNode in NodeCatalog2.Nodes)
                    {

                        if (ComNode.NodeInnerText.Length == 0) continue;

                        // Write the inner text. Remove spaces here.

                        int NID = -1; // we need this for further optimisations

                        foreach (COMNode2 ComNode2 in NodeCatalog2.Nodes)
                        {
                            // no point doing this optimisation if it's less than five bytes - 4 bytes = no difference, <4 bytes = wasted space
                            if (ComNode.NodeInnerText.Length < 4) break;

                            if (ComNode.NodeInnerText == ComNode2.NodeInnerText)
                            {
                                // set this to a very particular string - the reader will pick this up
                                ComNode.NodeID = ComNode2.NodeID;

                                //ComNode.NodeInnerText = $"|{ComNode2.NodeID}";
                                NID = ComNode2.NodeID;
                                break;
                            }
                        }

                        // Write the node ID and text
                        BW.Write(ComNode.NodeID);

                        if (NID == -1)
                        {
                            BW.Write(ComNode.NodeInnerText);
                        }
                        else
                        {
                            BW.Write(0xB0); // 0xB0 signifies optimised node. Yes we will read 160 bytes. No I don't care. 
                            BW.Write(NID);
                        }

                    }
                }
            }

            return true;

        }

        public void Decompress(string COMFileName, COMCatalog2 COMCat2, COMNodeCatalog2 NodeCatalog2)
        {

        }
    }
}
