﻿using Emerald.Core; 
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
/// </summary>

namespace Free
{
    public enum ErrorSeverity { Warning, FatalError, NonDebugError }

    public class Error
    {
        // Totally rewrote this function. It was crap
        public static void Throw(Exception err, ErrorSeverity severity, string text, string caption = "avant-gardé engine", int id = 0)
        {
            App AppCurrent = (App)Application.Current;

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

                    AppCurrent.SDLGame.Game_Shutdown(); 

                    Environment.Exit(id);
                    return;
                case ErrorSeverity.NonDebugError: // Game crash while not in debug
                    MessageBox.Show($"An error has occurred.\n\nPlease note down or screenshot the details of this error for support purposes and then exit {Settings.GameName}.\n\nError ID {id}: {text}\nDetailed Error Information: {err}", caption, MessageBoxButton.OK, MessageBoxImage.Error);

                    AppCurrent.SDLGame.Game_Shutdown();

                    Environment.Exit(id);
                    return; 
            }
        }
    }
}
