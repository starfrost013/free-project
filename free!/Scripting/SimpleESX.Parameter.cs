using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public enum ScriptParameterType { Boolean, String, Int };
    public class SimpleESXParameter
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public ScriptParameterType ScParamType { get; set; }
        public object Value { get; set; }
    }
}
