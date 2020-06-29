using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public enum ScriptParameterType { Bool, String, Int, Double };
    public class SimpleESXParameter
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public ScriptParameterType ScParamType { get; set; }
        public IGameObject Value { get; set; }
    }
}
