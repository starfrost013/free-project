using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public interface ICommandExecutor
    {

        bool ScriptRan { get; set; } // RunOnce
        bool ScriptRunOnce { get; set; } // Does the script run once?
        MainWindow MnWindow { get; set; }// This is ugly, but it works. Eventually we'll have a Good2ShitCodeService redirector 
        string Name { get; set; }
        List<SimpleESXParameter> Parameters { get; set; }
        ScriptReference SR { get; set; }
        bool CheckSatisfiesScriptReference();
        object GetParameter(string GetParam); 
        void GetParameters(List<SimpleESXParameter> Params);
        void SetScriptReference(ScriptReference SR);
        void Verify();
        ScriptReturnValue Execute();
    }
}
