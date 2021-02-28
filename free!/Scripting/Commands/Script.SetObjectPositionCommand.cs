using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class SetObjectPositionCommand : RootCommand
    {
        public FreeSDL MnWindow { get; set; }
        public string Name { get; set; }
        public List<SimpleESXParameter> Parameters { get; set; }
        public ScriptReference SR { get; set; }
        public bool ScriptRan { get; set; }
        public bool ScriptRunOnce { get; set; }
        public SetObjectPositionCommand()
        {
            Parameters = new List<SimpleESXParameter>();
        }

        public object GetParameter(string ParameterName)
        {
            try
            {
                // Loop through all of the script parameters. 
                foreach (SimpleESXParameter SESXParameter in Parameters)
                {
                    // If we find the parameter the caller is asking for...
                    if (SESXParameter.Name == ParameterName)
                    {
                        switch (SESXParameter.ScParamType)
                        {
                            // Convert to the type stored in ScParamType and return. 
                            case ScriptParameterType.Bool:
                                return Convert.ToBoolean(SESXParameter.Value);
                            case ScriptParameterType.Double:
                                return Convert.ToDouble(SESXParameter.Value);
                            case ScriptParameterType.Int:
                                return Convert.ToInt32(SESXParameter.Value);
                            case ScriptParameterType.String:
                                return SESXParameter.ToString();
                        }

                        return SESXParameter.Value;
                    }
                }

                ScriptError.Throw($"Error: Required parameter {ParameterName} not found!", 11, 0, "Runtime Error");
            }
            catch (FormatException err)
            {
                ScriptError.Throw($"Error converting parameters: {err}", 12, 0, "Runtime Error");
            }

            return null;
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
