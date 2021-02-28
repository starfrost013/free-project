using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class ThrowRuntimeErrorCommand : ICommandExecutor
    {
        public string Name { get; set; }
        public List<SimpleESXParameter> Parameters { get; set; }
        public FreeSDL MnWindow { get; set; }
        public bool ScriptRan { get; set; }
        public bool ScriptRunOnce { get; set; }
        public ScriptReference SR { get; set; }


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

        public object GetParameter(string ParameterName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// this is idiotic design.
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public void GetParameters(List<SimpleESXParameter> Parameters)
        {
            throw new NotImplementedException();
        }

        public ScriptReturnValue Execute()
        {
            throw new NotImplementedException();
        }
    }
}
