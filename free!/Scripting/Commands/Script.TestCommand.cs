using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    public class TestCommand : ICommandExecutor
    {
        public MainWindow MnWindow { get; set; }
        public string Name { get; set; }
        public List<SimpleESXParameter> Parameters { get; set; }

        public TestCommand()
        {
            Parameters = new List<SimpleESXParameter>();
        }

        public void GetParameters(List<SimpleESXParameter> Params)
        {
            
            Parameters = Params;
        }

        public void Verify()
        {
            if (Parameters.Count != 0)
            {
                ScriptError.Throw("TestCommand: Must have no parameters!!", 5, 0, "Temp");
            }
        }

        public void Execute()
        {
            MessageBox.Show("It works!"); 
        }
    }
}
