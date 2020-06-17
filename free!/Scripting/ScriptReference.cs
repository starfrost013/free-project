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
        public List<ScriptReferenceRunOn> RunOnParameters { get; set; }

        public ScriptReference()
        {
            RunOnParameters = new List<ScriptReferenceRunOn>();
        }
    }

    public class ScriptReferenceRunOn
    {
        public EventClass EventClass { get; set; }
        public string Name { get; set; }
        public List<ScriptReferenceRunOnParameter> ReferenceRunOn { get; set; }

        public ScriptReferenceRunOn()
        {
            ReferenceRunOn = new List<ScriptReferenceRunOnParameter>(); 
        }
    }

    public class ScriptReferenceRunOnParameter
    {
        public string Name { get; set; } // Name string. 
        public List<object> Value { get; set; } // Contextually interpreted by the command on execution. ScriptReference is passed if not null. 

        public ScriptReferenceRunOnParameter()
        {
            Value = new List<object>();
        }

        
    }
}
