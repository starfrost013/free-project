using Emerald.Core; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// File: Error.cs
/// 
/// Purpose: Static Error class
/// 
/// Ported from the track maker 2019-11-17
/// 
/// Ver 1.01 (2019-12-22)
/// 
/// 2021-02-25: Ported to Emerald.Core.dll
/// </summary>

namespace Emerald.Core
{
    public enum ErrorSeverity { Warning, FatalError, Error }

    public class Error : EmeraldComponent
    {
        public new static int API_VERSION_MAJOR = 2;
        public new static int API_VERSION_MINOR = 0;
        public new static int API_VERSION_REVISION = 0;
        public new static string DEBUG_COMPONENT_NAME = "Error Handler";
>
        public static void ThrowV2(string Text, string Caption, ErrorSeverity Severity, Exception CException = null, int ID = 0)
        {
            if (CException == null)
            {
                
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"An error has occurred of severity {Severity}:\n#{ID}: {Text} {Caption}");
            }
            else
            {
                SDLDebug.LogDebug_C(DEBUG_COMPONENT_NAME, $"Exception Handled: {CException}\n\nError ID {ID} with severity {Severity}: {Text} {Caption}");
            }
            

            switch (Severity)
            {
                case ErrorSeverity.Warning:
                    if (CException != null)
                    {
                        MessageBox.Show($"Warning: {Text}", $"Warning #{ID}: {Caption}", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"Warning: {Text}", $"Warning #{ID}: {Caption}\nException: {CException}", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return;
                case ErrorSeverity.Error:

                    if (CException != null)
                    {
                        MessageBox.Show($"Error: {Text}", $"Warning #{ID}: {Caption}", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {Text}", $"Warning #{ID}: {Caption}\nException: {CException}", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    return;
                case ErrorSeverity.FatalError:

                    if (CException != null)
                    {
                        MessageBox.Show($"Error: {Text}", $"Warning #{ID}: {Caption}", MessageBoxButton.OK, MessageBoxImage.Error);
                        // TEMP
                        Environment.Exit(ID);
                    }
                    else
                    {
                        MessageBox.Show($"Warning: {Text}", $"Warning #{ID}: {Caption}\nException: {CException}", MessageBoxButton.OK, MessageBoxImage.Error);
                        // TEMP
                        Environment.Exit(ID); 
                    }

                    return;
            }
        }

        /// <summary>
        /// Throws an error.
        /// 
        /// NEEDS REWRITE VERY BIG REWRITE
        /// </summary>
        /// <param name="err"></param>
        /// <param name="severity"></param>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="id"></param>
        public static void Throw(Exception err, ErrorSeverity severity, string text, string caption = "SDLEmerald", int id = 0)
        {

#if DEBUG
            SDLDebug.LogDebug_C("ERROR", $"An error has occurred with severity {severity}, ID {id}. Error information: {text}\n\n");
            
            if (err != null)
            {
                SDLDebug.LogDebug_C("ERROR", $"Detailed exception information is available.");
                SDLDebug.LogDebug_C("ERROR", $"{err.Message}");
            }
#endif
            switch (severity)
            {
                case ErrorSeverity.Warning: // Warning
                    MessageBox.Show($"Debug Warning: \n{text}", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                case ErrorSeverity.FatalError: // Fatal Error
                    MessageBox.Show($"An error has occurred.\n\nFatal Error #{id}: {text}\n\nDetailed Information: {err}", caption, MessageBoxButton.OK, MessageBoxImage.Error);

                    // TEMP
                    // Todo: Check for SDL init

                    // Todo: Port most engine code to EngineCore
                    //SDL_Stage0_Init.SDLGame.Game_Shutdown(); 

                    Environment.Exit(id);
                    return;
                case ErrorSeverity.Error: // Game crash while not in debug
                    MessageBox.Show($"An error has occurred.\n\nPlease note down or screenshot the details of this error for support purposes and then exit {Settings.GameName}.\n\nError ID {id}: {text}\nDetailed Error Information: {err}", caption, MessageBoxButton.OK, MessageBoxImage.Error);

                    // TEMP
                    //SDL_Stage0_Init.SDLGame.Game_Shutdown();

                    // Todo: Port most engine code to EngineCore
                    Environment.Exit(id);
                    return; 
            }
        }
    }
}
