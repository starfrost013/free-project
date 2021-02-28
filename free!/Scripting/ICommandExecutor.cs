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
        FreeSDL MnWindow { get; set; } // This is ugly, but it works. Eventually we'll have a Good2ShitCodeService redirector 
        string Name { get; set; }
        List<SimpleESXParameter> Parameters { get; set; }
        ScriptReference SR { get; set; }
        bool CheckSatisfiesScriptReference();
        object GetParameter(string GetParam); 

        /// <summary>
        /// This is bad. Why does this exist? Why is this implemented on a command level?
        /// 
        /// Remove the requirement to implement this. Static class? SimpleESXScript level functionality?
        /// </summary>
        /// <param name="Params"></param>
        void GetParameters(List<SimpleESXParameter> Params);
        void SetScriptReference(ScriptReference SR);
        void Verify();
        ScriptReturnValue Execute();
    }
}
