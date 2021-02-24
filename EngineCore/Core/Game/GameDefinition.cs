using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        public new static bool Experimental = true; 
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

        [XmlElement("Author")]
        /// <summary>
        /// The author of this game.
        /// </summary>
        public string Author { get; set; }

        [XmlElement("Description")]
        /// <summary>
        /// The description of this game.
        /// </summary>
        public string Description { get; set; }

        [XmlElement("LastModifiedDate")]
        /// <summary>
        /// The last modified date of this game. 
        /// </summary>
        private string _lastmodifieddate { get; set; }

        public DateTime LastModifiedDate { get 
            {
                return DateTime.Parse(_lastmodifieddate);
            }
            set
            {
                _lastmodifieddate = value.ToString();
            }

        }

        [XmlElement("Name")]
        /// <summary>
        /// The name of this game.
        /// </summary>
        public string Name { get; set; }

        [XmlElement("Version")]
        /// <summary>
        /// The version of this game.
        /// </summary>
        public string Version { get; set; }

        [XmlElement("Version_Status")]
        /// <summary>
        /// The status of this game. (ex: Alpha), similar to AssemblyInformationalVersion
        /// </summary>
        public string Version_Status { get; set; }


    }
}
