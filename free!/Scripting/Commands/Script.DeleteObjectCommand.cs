﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    public class DeleteObjectCommand : ICommandExecutor
    {
        public FreeSDL MnWindow { get; set; }
        public string Name { get; set; }
        public List<SimpleESXParameter> Parameters { get; set; }
        public bool ScriptRan { get; set; }
        public bool ScriptRunOnce { get; set; }
        public ScriptReference SR { get; set; }

        public object GetParameter(string ParameterName)
        {
            try
            {
                // Loop through all of the script parameters. 
                foreach (SimpleESXParameter SESXParameter in Parameters)
                {
                    // If we find the parameter the caller is asking for...
                    if (SESXParameter.Name == ParameterName)
                    {
                        switch (SESXParameter.ScParamType)
                        {
                            // Convert to the type stored in ScParamType and return. 
                            case ScriptParameterType.Bool:
                                return Convert.ToBoolean(SESXParameter.Value);
                            case ScriptParameterType.Double:
                                return Convert.ToDouble(SESXParameter.Value);
                            case ScriptParameterType.Int:
                                return Convert.ToInt32(SESXParameter.Value);
                            case ScriptParameterType.String:
                                return SESXParameter.ToString();
                        }

                        return SESXParameter.Value;
                    }
                }

                ScriptError.Throw($"Error: Required parameter {ParameterName} not found!", 11, 0, "Runtime Error");
            }
            catch (FormatException err)
            {
                ScriptError.Throw($"Error converting parameters: {err}", 12, 0, "Runtime Error"); 
            }

            return null; 
                
        }

        public void GetParameters(List<SimpleESXParameter> Params)
        {
            Parameters = Params;
        }

        public void SetScriptReference(ScriptReference ScriptRef)
        {
            SR = ScriptRef;
        }

        public bool CheckSatisfiesScriptReference()
        {
            if (SR == null) return true;

            foreach (ScriptReferenceRunOn SRX in SR.RunOnParameters)
            {
                // Check if we satisfy the script reference. 
                if (ScriptReferenceResolver.Resolve(SRX))
                {
                    return true;
                }

            }

            return false;

        }

        public DeleteObjectCommand()
        {
            MnWindow = (FreeSDL)Application.Current.MainWindow;
        }

        public void Verify()
        {
            // Verify that we are actaully inserting an object. 
            if (Parameters.Count != 1) ScriptError.Throw("DeleteObject(ID): 1 parameter required", 10, 0, "Temp");
        }

        public ScriptReturnValue Execute()
        {
            //MnWindow.currentlevel.LevelObjects.Add()
            MnWindow.currentlevel.DeleteObject((int)GetParameter("ID")); 

            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "The operation completed successfully - an object has been added." };
        }
    }
}