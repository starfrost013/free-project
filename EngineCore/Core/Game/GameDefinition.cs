using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Emerald.Core
{

    /// <summary>
    /// Defines a game made using Emerald.
    /// 
    /// Motto: "Always more stuff to add!"
    /// </summary>
    public class GameDefinition : EmeraldComponent
    {
        public new static string DEBUG_COMPONENT_NAME = "Game Service"; 

        [XmlElement("ContentFolderLocation")]
        /// <summary>
        /// The path to the content folder of this game.
        /// </summary>
        public string ContentFolderLocation { get; set; }

        [XmlElement("DebugEnabled")]
        /// <summary>
        /// Is debug mode enabled for this game?
        /// </summary>
        public bool DebugEnabled { get; set; }

        [XmlElement("GameAuthor")]
        /// <summary>
        /// The author of this game.
        /// </summary>
        public string GameAuthor { get; set; }

        [XmlElement("GameName")]
        /// <summary>
        /// The name of this game.
        /// </summary>
        public string GameName { get; set; }

        [XmlElement("GameVersion")]
        /// <summary>
        /// The version of this game.
        /// </summary>
        public string GameVersion { get; set; }
    }
}
