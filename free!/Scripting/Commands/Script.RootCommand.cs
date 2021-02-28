using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    /// <summary>
    /// Provides functionality shared among all ESX commands; DO NOT EXECUTE!
    /// </summary>
    public class RootCommand : ICommandExecutor
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
            foreach (SimpleESXParameter SESXParam in Parameters)
            {
                if (SESXParam.Name == ParameterName)
                {
                    SimpleESXParameter ParamWeWant = SESXParam;

                    try
                    {
                        switch (ParamWeWant.ScParamType)
                        {
                            case ScriptParameterType.Bool:
                                return (bool)ParamWeWant.Value;
                            case ScriptParameterType.Double:
                                return (double)ParamWeWant.Value;
                            case ScriptParameterType.Int:
                                return (int)ParamWeWant.Value;
                            case ScriptParameterType.String:
                                // todo: will not work for all types
                                return ParamWeWant.Value.ToString();
                            default:
                                return ParamWeWant.Value;
                        }

                    }
                    catch (FormatException)
                    {
                        ScriptError.Throw($"Error: Required parameter {ParameterName} not found!", 11, 0, "Runtime Error");
                    }
                }

            }

            ScriptError.Throw($"Error: Required parameter {ParameterName} not found!", 11, 0, "Runtime Error");
            return null; 
        }

        /// <summary>
        /// this is idiotic design.
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public void GetParameters(List<SimpleESXParameter> Params) => Parameters = Params;;


        public ScriptReturnValue Execute()
        {
            throw new NotImplementedException();
        }
    }
}
