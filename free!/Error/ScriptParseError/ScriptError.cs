using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class ScriptError
    {
        public string Error { get; set; } // Any desc
        public ushort Id { get; set; } // 0-65535
        public int Line { get; set; } // The line the error occurred in
        public string ScriptPath { get; set; } // The script path.

        public static void Throw(string Error, ushort Id, int Line, string ScriptPath)
        {
            ScriptParseError SPE = new ScriptParseError(new ScriptError { Error = Error, Id = Id, Line = Line, ScriptPath = ScriptPath });
            SPE.ShowDialog();

        }
    }
}
