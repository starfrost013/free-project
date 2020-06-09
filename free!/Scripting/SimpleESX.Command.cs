using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class SimpleESXCommand
    {
        public CommandClass CmdClass { get; set; }
        public string CommandName { get; set; }
        public List<SimpleESXParameter> CommandParameters { get; set; }
        public string Description { get; set; }
        public ICommandExecutor CommandExecutor { get; set; }
        public List<SimpleESXParameter> UserParameters { get; set; } // These are compared by ICommandExecutor.

        public SimpleESXCommand()
        {
            CmdClass = new CommandClass();
            UserParameters = new List<SimpleESXParameter>();
        }

        public void GetExecutor()
        {
            CommandExecutor = CommandFactory.String2CommandExecutor(CommandName);
        }
    }

}
