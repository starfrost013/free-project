using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class ThrowRuntimeErrorCommand : RootCommand
    {
        public new void Verify()
        {
            throw new NotImplementedException();
        }

        public new bool CheckSatisfiesScriptReference()
        {
            throw new NotImplementedException();
        }

        public new void SetScriptReference(ScriptReference SR)
        {
            throw new NotImplementedException(); 
        }


        public new ScriptReturnValue Execute()
        {
            throw new NotImplementedException();
        }
    }
}
