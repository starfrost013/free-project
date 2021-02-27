using Emerald.Core;
using Emerald.Utilities.Wpf2Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{

    /// <summary>
    /// Created 2020-0x 
    /// 
    /// Modified 2020-02-27
    /// 
    /// Emerald ESX Scripting Subsystem
    /// 
    /// Handles runtime errors in ESX scripts
    /// </summary>
    public class ScriptError : EmeraldComponent
    {
        public new static int API_VERSION_MAJOR = 0;
        public new static int API_VERSION_MINOR = 2;
        public new static int API_VERSION_REVISION = 1;

        public new static string DEBUG_COMPONENT_NAME = "Script Error Handler";

        public string ErrorStr { get; set; } // Any desc
        public ushort Id { get; set; } // 0-65535
        public int Line { get; set; } // The line the error occurred in
        public string ScriptPath { get; set; } // The script path.

        public static void Throw(string ErrorStr, ushort Id, int Line, string ScriptPath)
        {
            // DON'T USE ERROR.THROW THIS IS DIFFERENT
            MessageBox.Show($"Runtime Error!\n\nScript: {ScriptPath}\n\nLine: {Line}\n\nId: {Id}\n\nDescription: {ErrorStr}");
            Environment.Exit(0x1111 ^ Id); //XOR the error ID in the exit code with the key 0x1111


            /* remvoed 2020-02-27 (v1551)
            ScriptParseError SPE = new ScriptParseError(new ScriptError { Error = Error, Id = Id, Line = Line, ScriptPath = ScriptPath });
            SPE.ShowDialog();
            */
        }
    }
}
