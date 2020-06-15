using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class InsertObjectCommand : ICommandExecutor
    {
        public MainWindow MnWindow { get; set; }
        public string Name { get; set; }
        public List<SimpleESXParameter> Parameters { get; set; }
        public bool ScriptRan { get; set; }
        public bool ScriptRunOnce { get; set; }
        public ScriptReference SR { get; set; }

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
            // Verify that we are actaully inserting an object. 
            if (Parameters.Count != 3) ScriptError.Throw("InsertObject(ID, x, y): 3 parameters required", 10, 0, "Temp");
        }

        public ScriptReturnValue Execute()
        {
            //MnWindow.currentlevel.LevelObjects.Add()
            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "The operation completed successfully - an object has been added." };
        }
    }
}
