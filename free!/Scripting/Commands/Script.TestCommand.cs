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
        public TestCommand()
        {
            Parameters = new List<SimpleESXParameter>();
        }

        /// <summary>
        /// Special case: doesn't check for script referencing
        /// </summary>
        /// <returns></returns>
        public new bool CheckSatisfiesScriptReference() => true;

        public new void Verify()
        {
            if (Parameters.Count != 0)
            {
                ScriptError.Throw("TestCommand must have no parameters!", 12, 0, "Temp");
            }

        }

        public new ScriptReturnValue Execute()
        {
            if (!CheckSatisfiesScriptReference()) return new ScriptReturnValue { ReturnCode = 1, ReturnInformation = "Script reference requirements unsatisfied" }; 
            MessageBox.Show("It works!");
            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "Execution successful." };
        }
    }
}
