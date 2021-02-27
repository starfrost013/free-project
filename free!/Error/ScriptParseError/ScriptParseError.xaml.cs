using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    /// <summary>
    /// Interaction logic for ScriptParseError.xaml.
    /// 
    /// This is pretty bad, but we could be in a pretty bad situation if we get here.
    /// </summary>
    public partial class ScriptParseError : Window
    {
        public ScriptError Err { get; set; }
        
        public ScriptParseError(ScriptError FatalError)
        {
            Err = FatalError; 
            InitializeComponent();
            SEXScriptError_ErroredScript.DataContext = Err;
            SEXScriptError_ErroredScriptError.DataContext = Err;
            SEXScriptError_ErroredScriptLine.DataContext = Err;
            UpdateLayout(); 
        }

        private void SEXScriptError_ExitButton_Click(object sender, RoutedEventArgs e)
        {
            int ExitCode = ((Err.Id << 16) | (0xDEAD));
            Application.Current.Shutdown(ExitCode); /// Exit Code 0x(errID)DEAD = fatal script error

            // If that doesn't work
            Environment.Exit(ExitCode); 
        }

    }
}
