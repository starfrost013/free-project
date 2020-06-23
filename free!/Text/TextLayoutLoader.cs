using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Free
{
    partial class FreeSDL
    {
        public void LoadLevelText()
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(currentlevel.TEXTPATH);
            XmlNode XmlRootNode = XmlDocument.FirstChild;
            AGTextBlock TxtBlock = new AGTextBlock();

            while (XmlRootNode.Name != "Text")
            {
                XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception. Still todo (2019-12-08).
            }

            XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // yeah

            foreach (XmlNode XmlNode in XmlNodes)
            {
                if (XmlNode.Attributes.Count > 0)
                {
                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes;
                    
                    foreach (XmlAttribute XmlAttribute in XmlAttributes)
                    {
                        switch (XmlAttribute.Name)
                        {
                            case "Name":
                            case "name":
                                foreach (AGTextBlock TextBlock in TextList)
                                {
                                    if (TextBlock.TextName == XmlAttribute.Value)
                                    {
                                        TxtBlock = TextBlock;
                                    }
                                }
                                continue;
                            case "Position":
                            case "position":
                            case "Pos":
                            case "pos":
                                double[] position = Array.ConvertAll<string, double>(XmlAttribute.Value.Split(','), Double.Parse);
                                MoveText(TxtBlock, position[0], position[1]);
                                SetTextVisibility(TxtBlock, true);
                                continue; //yeah

                        }
                    }
                }
            }
        }

    }
}
