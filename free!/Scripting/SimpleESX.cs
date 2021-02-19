using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;

namespace Free
{
    // Trigger IGameObjects?
    public enum EventClass { OnLoad, OnDestroy, OnChangeLevel, OnPlayerSpawn, OnBeingDead, OnGameOver, OnSkillObtained, OnSkillLost, OnLevelChange, OnHurt, OnAIChange, OnAICreate, OnAIDie, OnSpecificIGameObjectSpawn, OnSpecificIGameObject, Routine, EveryFrame, OnAnimationPlayed, OnAnimationPlayed_SpecificFrame, OnIGameObjectInteraction, OnSoundPlayed, OnMusicPlayed, OnVFXSpawn, OnVFXParticleNum, OnVFXDespawn, OnPlayerReachCertainPosition, OnCollide, OnGameClose }
    public partial class SimpleESX
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
            LoadedScripts = new List<SimpleESXScript>(); 
            ReflectionMetadata = new List<SimpleESXCommand>();
            Stack = new List<SimpleESXScript>();
        }

        public void LoadReflection()
        {
            SDLDebug.LogDebug_C("SimpleESX Init Reflection Loader", "Now loading ReflectionMetadata..."); 

            try
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.Load(@"Content\Game\ReflectionMetadata.xml");

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
                                            switch (XGreatGrandchildNode.Name)
                                            {
                                                case "Parameter":
                                                    // Create a new SimpleEsX parameter.
                                                    SimpleESXParameter SEXParameter = new SimpleESXParameter();
                                                    XmlNodeList XGGGrandchildNodes = XGreatGrandchildNode.ChildNodes;

                                                    foreach (XmlNode XGGGrandchildNode in XGGGrandchildNodes)
                                                    {
                                                        // Read the parameter information 
                                                        switch (XGGGrandchildNode.Name)
                                                        {
                                                            case "Description": // The description of this parameter.
                                                                SEXParameter.Description = XGGGrandchildNode.ChildNodes[0].Value;
                                                                continue;
                                                            case "Name": // The name of this parameter.
                                                                SEXParameter.Name = XGGGrandchildNode.ChildNodes[0].Value;
                                                                continue;
                                                            case "ParameterType": // The type of this parameter.
                                                                SEXParameter.ScParamType = (ScriptParameterType)Enum.Parse(typeof(ScriptParameterType), XGGGrandchildNode.ChildNodes[0].Value);
                                                                
                                                                continue;
                                                        }
                                                        continue;
                                                    }

                                                    if (SEXParameter.Name == null) Error.Throw(null, ErrorSeverity.FatalError, "Attempted to load a parameter with no name!", "Error!", 76);

                                                    SEXCommand.CommandParameters.Add(SEXParameter);
                                                    continue; 
                                            }

                                            
                                            
                                        }
                                        continue; 
                                }
                            
                            }

                            if (SEXCommand.CommandName == null) Error.Throw(null, ErrorSeverity.FatalError, "Attempted to load a command with no name!", "Error!", 75);
                            SEXCommand.GetExecutor();
                            ReflectionMetadata.Add(SEXCommand);

#if DEBUG
                            SDLDebug.LogDebug_C("Emerald SimpleESX Reflection Metadata Loader", $"Successfully loaded SimpleESXCommand with name {SEXCommand.CommandName} and class {SEXCommand.CommandExecutor}");
#endif
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

        public void LoadAllLevelScripts(List<IGameObject> LevelIGameObjects)
        {
            foreach (IGameObject LevelIGameObject in LevelIGameObjects)
            {
                // Check that there's stuff to load
                if (LevelIGameObject.AssociatedScriptPaths == null) continue;
                if (LevelIGameObject.AssociatedScriptPaths.Count == 0) continue; 

                foreach (ScriptReference SR in LevelIGameObject.AssociatedScriptPaths)
                {
                    SimpleESXScript SESX = LoadScript(SR.Path);
                    SESX.SetScriptClass(SR.RunOnParameters[0]); // Thank you have a nice day
                }
            }
        }

        public void ClearLoadedScripts()
        {
            SDLDebug.LogDebug_C($"SimpleESXScript Loader", $"Clearing all scripts...");
            LoadedScripts.Clear();
            Stack.Clear();
            ScriptContext = null; // we are not running a script.
        }

        public SimpleESXScript LoadScript(string ScriptPath)
        {

            SDLDebug.LogDebug_C($"SimpleESXScript Loader", $"Now loading script at {ScriptPath}..."); 

            SimpleESXScript SEXScript = new SimpleESXScript();
            SEXScript.LoadScript(ScriptPath);

            SDLDebug.LogDebug_C($"SimpleESXScript Loader", $"Now parsing script at {ScriptPath}...");
            SEXScript.ParseScript();
            ScriptContext = SEXScript;
            LoadedScripts.Add(SEXScript);
            return SEXScript;
        }

        /// <summary>
        /// Executes all SimpleESX (SEX/SESX) scripts of a specific ESX/Lua CommandClass.
        /// </summary>
        /// <param name="CmdClass">The command class to execute the scripts of.</param>
        public void ExecuteAllScriptsOfType(EventClass EVTClass)
        {
            foreach (SimpleESXScript SESXScript in LoadedScripts)
            {
                // Is the script loaded and parsed?
                if (SESXScript.Loaded)
                {
                    if (SESXScript.RunOnEvent.EventClass == EVTClass)
                    {
                        SESXScript.ExecuteScript(); 
                    } 
                }
            }
        }

        /// <summary>
        /// Executes a loaded script.
        /// </summary>
        /// <param name="ScriptName">The script name to execute.</param>
        public void ExecuteScriptWithName(string ScriptName)
        {
            foreach (SimpleESXScript SESXScript in LoadedScripts)
            {
                if (SESXScript.Name == ScriptName && SESXScript.Loaded)
                {
                    ScriptContext = SESXScript;
                    ExecuteCurrentScript();
                }
            }
        }

        /// <summary>
        /// Executes the current ScriptContext.
        /// </summary>
        public void ExecuteCurrentScript()
        {
            ScriptContext.ExecuteScript();
        }

        /// <summary>
        /// Sets the current script. It must be loaded first.
        /// </summary>
        /// <param name="ScriptName"></param>
        public void SetCurrentScript(string ScriptName)
        {
            foreach (SimpleESXScript SESXScript in LoadedScripts)
            {
                if (SESXScript.Name == ScriptName)
                {
                    ScriptContext = SESXScript;
                    return;
                }
            }
        }

    }

    /// <summary>
    /// Command type class.
    /// </summary>
    public class CommandClass
    {
        public EventClass TriggerOnEvent { get; set; }
        public List<string> EventParameters { get; set; } // Created via callback. The originating script, what it was triggered by, etc.
    }
}
