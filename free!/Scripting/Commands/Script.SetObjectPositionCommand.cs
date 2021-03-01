using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class SetObjectPositionCommand : RootCommand
    {

        public SetObjectPositionCommand()
        {
            Parameters = new List<SimpleESXParameter>();
        }


        public new void Verify()
        {
            if (Parameters.Count != 0)
            {
                ScriptError.Throw("SetObjectPosition: Must have three parameters!!", 14, 0, "Runtime Error");
                return; 
            }

            // fix this but it's late
            if (Parameters[0].ScParamType != ScriptParameterType.Int) ScriptError.Throw("Parameter 1 must be valid object ID", 13, 0, "Runtime Error");

            if (Parameters[1].ScParamType != ScriptParameterType.Double) ScriptError.Throw("Parameter 2 must be valid X position", 14, 0, "Runtime Error");

            if (Parameters[2].ScParamType != ScriptParameterType.Double) ScriptError.Throw("Parameter 3 must be valid Y position", 15, 0, "Runtime Error");
        }

        public new ScriptReturnValue Execute()
        {
            if (!CheckSatisfiesScriptReference()) return new ScriptReturnValue { ReturnCode = 1, ReturnInformation = "Script reference requirements unsatisfied" };

            MnWindow.currentlevel.SetGameObjectPos((int)GetParameter("ID"), (int)GetParameter("X"), (int)GetParameter("Y")); 

            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "Execution successful." };
        }
    }
}
