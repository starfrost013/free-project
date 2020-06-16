using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public static class CommandFactory
    {
        /// <summary>
        /// Convert command factory to command
        /// </summary>
        public static ICommandExecutor String2CommandExecutor(string Command)
        {
            switch (Command)
            {
                case "InsertObject":
                    return new InsertObjectCommand();
                case "Test":
                case "TestCommand":
                    return new TestCommand();

            }

            // Error
            ScriptError.Throw($"Internal Error ESX0005: Attempted to instantiate invalid command with name {Command}!", 5, 0, "Engine Bug!");
            return null;
        }
    }
}
