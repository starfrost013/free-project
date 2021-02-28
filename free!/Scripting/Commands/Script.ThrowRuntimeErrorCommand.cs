using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class ThrowRuntimeErrorCommand : RootCommand
    {
        public void Verify()
        {
            throw new NotImplementedException();
        }

        public bool CheckSatisfiesScriptReference()
        {
            throw new NotImplementedException();
        }

        public void SetScriptReference(ScriptReference SR)
        {
            throw new NotImplementedException(); 
        }

        public object GetParameter(string ParameterName) => base.GetParameter(ParameterName);

        /// <summary>
        /// this is idiotic design.
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public void GetParameters(List<SimpleESXParameter> Parameters) => base.GetParameters(Parameters);
        {
            throw new NotImplementedException();
        }

        public ScriptReturnValue Execute()
        {
            throw new NotImplementedException();
        }
    }
}
