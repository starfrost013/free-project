using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public interface ICommandExecutor
    {
        MainWindow MnWindow { get; set;  }// This is ugly, but it works. Eventually we'll have a Good2ShitCodeService redirector 
        string Name { get; set; }
        List<SimpleESXParameter> Parameters { get; set; }
        void GetParameters(List<SimpleESXParameter> Params);
        void Verify();
        void Execute();
    }
}
