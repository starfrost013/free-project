using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;

namespace Free
{
    // Trigger objects?
    public enum EventClass { OnLoad, OnDestroy, OnChangeLevel, OnPlayerSpawn, OnBeingDead, OnGameOver, OnSkillObtained, OnSkillLost, OnLevelChange, OnHurt, OnAIChange, OnAICreate, OnAIDie, OnSpecificObjectSpawn, OnSpecificObject, Routine, EveryFrame, OnAnimationPlayed, OnAnimationPlayed_SpecificFrame, OnObjectInteraction, OnSoundPlayed, OnMusicPlayed, OnVFXSpawn, OnVFXParticleNum, OnVFXDespawn, OnPlayerReachCertainPosition, OnCollide, OnGameClose }
    public class SimpleESX
    {
        internal List<SimpleESXScript> LoadedScripts { get; set; }

        /// <summary>
        /// Static globally accesssible copy of the current reflection metadata.
        /// </summary>
        public static List<SimpleESXCommand> ReflectionMetadata { get; set; }
        internal SimpleESXScript ScriptContext { get; set; }
        internal List<SimpleESXScript> Stack { get; set; }

        public SimpleESX()
        {
            ReflectionMetadata = new List<SimpleESXCommand>();
            Stack = new List<SimpleESXScript>();
        }

        public void LoadReflection()
        {
            try
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.Load(@"Game\ReflectionMetadata.xml");

                if (!XDoc.HasChildNodes)
                {
                    Error.Throw(null, ErrorSeverity.FatalError, "An error occurred loading the script reflection metadata!", "Error", 72);
                    return;
                }

                XmlNode XRootNode = XDoc.FirstChild;

                // Get the first node
                while (XRootNode.Name != "ReflectionMetadata")
                {
                    if (XRootNode.NextSibling == null)
                    {
                        Error.Throw(null, ErrorSeverity.FatalError, "An error occurred loading the script reflection metadata!", "Error", 71);
                        return;
                    }

                    XRootNode = XRootNode.NextSibling;
                }


                if (!XRootNode.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "An error occurred - ReflectionMetadata.xml is empty!.", "Error", 73);

                XmlNodeList XChildNodes = XRootNode.ChildNodes;

                // Iterate through all of the command definition nodes.
                foreach (XmlNode XChild in XChildNodes)
                {
                    switch (XChild.Name)
                    {
                        case "Command":
                            // So we found a command node 

                            if (!XChild.HasChildNodes) Error.Throw(null, ErrorSeverity.FatalError, "An error occurred - Can't load an empty command", "Error", 74); 

                            XmlNodeList XGrandChildNodes = XChild.ChildNodes;
                            SimpleESXCommand SEXCommand = new SimpleESXCommand();

                            foreach (XmlNode XGrandChildNode in XGrandChildNodes)
                            {
                                switch (XGrandChildNode.Name)
                                {
                                    // The name of the command.
                                    case "Name":
                                        SEXCommand.CommandName = XGrandChildNode.InnerText;
                                        continue;
                                    // The class of the command.
                                    case "Class":
                                        SEXCommand.CmdClass.TriggerOnEvent = (EventClass)Enum.Parse(typeof(EventClass), XGrandChildNode.InnerText);
                                        continue;
                                    // The description of the command.
                                    case "Description":
                                        SEXCommand.Description = XGrandChildNode.InnerText;
                                        continue;
                                    // The parameter schema
                                    case "ParameterSchema":
                                        // Get the child nodes
                                        XmlNodeList XGreatGrandchildNodes = XGrandChildNode.ChildNodes;

                                        foreach (XmlNode XGreatGrandchildNode in XGreatGrandchildNodes)
                                        {
                                            // Create a new SimpleEsX parameter.
                                            SimpleESXParameter SEXParameter = new SimpleESXParameter();
                                            // Read the parameter information 
                                            switch (XGreatGrandchildNode.Name)
                                            {
                                                case "Description": // The description of this parameter.
                                                    SEXParameter.Description = XGreatGrandchildNode.InnerText;
                                                    continue; 
                                                case "Name": // The name of this parameter.
                                                    SEXParameter.Name = XGreatGrandchildNode.InnerText;
                                                    continue;
                                                case "ParameterType": // The type of this parameter.
                                                    SEXParameter.ScParamType = (ScriptParameterType)Enum.Parse(typeof(ScriptParameterType), XGreatGrandchildNode.InnerText);
                                                    continue;
                                            }

                                            if (SEXParameter.Name == null) Error.Throw(null, ErrorSeverity.FatalError, "Attempted to load a parameter with no name!", "Error!", 76);

                                            SEXCommand.CommandParameters.Add(SEXParameter); 
                                        }
                                        continue; 
                                }
                            }
                            if (SEXCommand.CommandName == null) Error.Throw(null, ErrorSeverity.FatalError, "Attempted to load a command with no name!", "Error!", 75);
                            SEXCommand.GetExecutor();
                            ReflectionMetadata.Add(SEXCommand);

                            continue; 
                    }
                }
            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred loading the script reflection metadata.", "Error", 77);
                return;
            }
            catch (ArgumentException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred loading a command type.", "Error", 78);
                return;
            }
        }

        public void LoadScript(string ScriptPath)
        {
            SimpleESXScript SEXScript = new SimpleESXScript();
            SEXScript.LoadScript(ScriptPath);
            SEXScript.ParseScript();
            ScriptContext = SEXScript;
            Stack.Add(SEXScript);
        }

        /// <summary>
        /// Executes all SimpleESX (SEX/SESX) scripts of a specific ESX/Lua CommandClass.
        /// </summary>
        /// <param name="CmdClass">The command class to execute the scripts of.</param>
        public void ExecuteAllScriptsOfType(CommandClass CmdClass)
        {
            
        }

        /// <summary>
        /// Executes a loaded script.
        /// </summary>
        /// <param name="ScriptName">The script name to execute.</param>
        public void ExecuteScriptWithName(string ScriptName)
        {

        }

        public void ExecuteCurrentScript()
        {
            ScriptContext.ExecuteScript();
        }

        /// <summary>
        /// Sets the current script. It must be loaded,.
        /// </summary>
        /// <param name="ScriptName"></param>
        public void SetCurrentScript(string ScriptName)
        {
            foreach (SimpleESXScript SESXScript in LoadedScripts)
            {
                if (SESXScript.Name == ScriptName)
                {
                    ScriptContext = SESXScript;
                }
            }
        }
    }

    public class CommandClass
    {
        public EventClass TriggerOnEvent { get; set; }
        public List<string> EventParameters { get; set; } // Created via callback. The originating script, what it was triggered by, etc.
    }
}
