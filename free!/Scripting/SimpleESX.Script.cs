using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class SimpleESXScript
    {
        public ScriptReferenceRunOn RunOnEvent { get; set; } // Loaded from ScriptReference.
        public string Name { get; set; }
        public string Path { get; set; }
        public List<SimpleESXCommand> SEXCommands { get; set; }
        public List<string> SEXLines { get; set; }
        public bool Loaded { get; set; }
        public SimpleESXScript()
        {
            SEXCommands = new List<SimpleESXCommand>();
            SEXLines = new List<string>();
        }



        internal void LoadScript(string ScriptPath)
        {
            // Open the script
            using (StreamReader SR = new StreamReader(new FileStream(ScriptPath, FileMode.Open)))
            {
                Path = ScriptPath;
                // array?
                string _ = SR.ReadLine();
                // Script name

                if (_.Length == 0)
                {
                    ScriptError.Throw("No script name was found. The script may be corrupted.", 0, 1, ScriptPath); 
                }
                else
                {
                    Name = _;
                }

                while (!SR.EndOfStream)
                {
                    SEXLines.Add(SR.ReadLine());
                }
                
                Name = _;
            }
        }

        internal void ParseScript()
        {
            /* * Parse the script and turn it into commands that we can use.
            * Script commands are parsde in the format CCCC pn1:p1 pn2:p2 pn3:p3 ...
            * where CCCC is the command name, pn1, pn2, pn3, etc are the parameter names, and p1, p2, p3, etc are the parameters for this command.
            * For example:
            * Insert_Object TestObject1 x:650 y:400 
            * 
            * Keywords might be added later.
            */
            
            for (int i = 0; i < SEXLines.Count; i++) 
            {
                string SEXLine = SEXLines[i];

                // Split into the components required.
                string[] SEXLineComponents = SEXLine.Split(' ');

                if (SEXLineComponents.Length == 0) ScriptError.Throw("Command identifier expected.", 1, i + 1, Path);

                // Check the list of valid commands

                // Used for checking
                SimpleESXCommand CurCommand = null;

                foreach (SimpleESXCommand SEXCommand in SimpleESX.ReflectionMetadata)
                {
                    // If we find the right command do it.
                    if (SEXCommand.CommandName == SEXLineComponents[0]) CurCommand = SEXCommand;    
                }

                // Check that this is a valid command
                if (CurCommand == null)
                {
                    ScriptError.Throw($"Invalid command {SEXLine} found.", 2, i + 1, Path);
                }
                else
                {
                    // So CurCommand is null...parse the parameters
                    
                    // Skip the first one
                    for (int j = 1; j < SEXLineComponents.Length; j++)
                    {
                        string SEXLineComponent = SEXLineComponents[i];
                        // Verify parameters. More checking of this is done by the individual commands.
                        string[] SexLineComponentParameters = SEXLineComponent.Split(':');

                        // Check that we didn't do something stupid like x::400 or x:800:460
                        if (SexLineComponentParameters.Length != 2) ScriptError.Throw($"Invalid parameter format @ {SEXLine}.", 4, i + 1, Path);
                        SimpleESXParameter CurParameter = null;

                        foreach (SimpleESXParameter SEXParameter in CurCommand.CommandParameters)
                        {
                            // Command format is 
                            if (SexLineComponentParameters[0] == SEXParameter.Name)
                            {
                                CurParameter = SEXParameter;
                            }
                        }

                        // Error checking
                        if (CurParameter == null) ScriptError.Throw($"Invalid parameter {CurParameter.Name} found.", 3, i + 1, Path);

                        CurParameter.Value = SexLineComponentParameters[1];

                        CurCommand.UserParameters.Add(CurParameter);
                        
                    }

                    SEXCommands.Add(CurCommand);
                }
            }

            // THe script has now loaded
            Loaded = true;
            return; 
        }

        public void ExecuteScript()
        {
            foreach (SimpleESXCommand SEXCommand in SEXCommands)
            {
                SEXCommand.CommandExecutor.GetParameters(SEXCommand.UserParameters); 
                SEXCommand.CommandExecutor.Verify();
                SEXCommand.CommandExecutor.Execute();
            }
        }

        public void SetScriptClass(ScriptReferenceRunOn SR)
        {
            RunOnEvent = SR;
            return;
        }
    }
}
