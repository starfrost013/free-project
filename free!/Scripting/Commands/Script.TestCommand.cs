using Emerald.Utilities.Wpf2Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class TestCommand : RootCommand
    {
        public FreeSDL MnWindow { get; set; }
        public string Name { get; set; }
        public List<SimpleESXParameter> Parameters { get; set; }
        public ScriptReference SR { get; set; }
        public bool ScriptRan { get; set; }
        public bool ScriptRunOnce { get; set; }
        public TestCommand()
        {
            Parameters = new List<SimpleESXParameter>();
        }

        public object GetParameter(string ParameterName)
        {
            throw new NotImplementedException();
        }

        public void GetParameters(List<SimpleESXParameter> Params)
        {
            Parameters = Params;
        }

        public void SetScriptReference(ScriptReference ScriptRef)
        {
            SR = ScriptRef;
        }

        public bool CheckSatisfiesScriptReference()
        {
            if (SR == null) return true; 

            foreach (ScriptReferenceRunOn SRX in SR.RunOnParameters)
            {
                // Check if we satisfy the script reference. 
                if (ScriptReferenceResolver.Resolve(SRX))
                {
                    return true;
                }

            }
            return false;
        }

        public void Verify()
        {
            if (Parameters.Count != 0)
            {
                ScriptError.Throw("TestCommand must have no parameters!", 12, 0, "Temp");
            }

        }

        public ScriptReturnValue Execute()
        {
            if (!CheckSatisfiesScriptReference()) return new ScriptReturnValue { ReturnCode = 1, ReturnInformation = "Script reference requirements unsatisfied" }; 
            MessageBox.Show("It works!");
            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "Execution successful." };
        }
    }
}
