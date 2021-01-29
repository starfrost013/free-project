using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    /// <summary>
    /// Object root class.
    /// 
    /// © 2021 starfrost.
    /// 
    /// Engine Version 4.0.1506.0
    /// </summary>
    public class RootObject
    {

        /// <summary>
        /// Is this object archivable? (can it be unloaded?)
        /// </summary>
        public bool Archivable { get; set; }


        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ID of this object.
        /// </summary>
        public int Id { get; set; }
        
        public void SetName(string NewName)
        {
            Name = NewName; // should use a backing field but whatever 
        }
    }
}
