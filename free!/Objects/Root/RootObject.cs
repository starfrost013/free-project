using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; 

namespace Free
{
    /// <summary>
    /// DataModel Object root class.
    /// 
    /// © 2021 starfrost.
    /// 
    /// Engine Version 4.0.1506.0
    /// </summary>
    public class RootObject
    {
        [XmlElement("Archivable")]
        /// <summary>
        /// Is this object archivable? (can it be unloaded?)
        /// </summary>
        public bool Archivable { get; set; }

        /// <summary>
        /// Is this object loaded?
        /// </summary>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Is the objdef of this object loaded?
        /// </summary>
        public bool IsXmlLoaded { get; set; }

        [XmlElement("Type")]
        /// <summary>
        /// ObjectType of this object
        /// </summary>
        public static ObjectTypes OType = ObjectTypes.RootObject;

        [XmlElement("Name")]
        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        [XmlElement("ID")]
        /// <summary>
        /// The ID of this object.
        /// </summary>
        public int Id { get; set; }
        
        [XmlElement("ScriptDefinitionPath")]
        public string ScriptDefinitionPath { get; set; }
        public List<ScriptReference> ScriptReferences { get; set; }
        public void SetName(string NewName)
        {
            Name = NewName; // should use a backing field but whatever 
        }

        public string GetName() => Name;

        public RootObject()
        {
            ScriptReferences = new List<ScriptReference>();
        }

    }
}
