using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    /// <summary>
    /// Holds a reference to a SESX script. 
    /// </summary>
    public class ScriptReference
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public EventClass EventClass { get; set; }
        
    }
}
