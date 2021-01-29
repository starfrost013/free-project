using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// 
/// /Settings/Settings.cs
/// 
/// Created: 2019-12-22
/// 
/// Modified: 2020-07-11
/// 
/// Version: 2.00
/// 
/// Purpose: Handles settings
/// 
/// </summary>

namespace Emerald.Core
{
    public enum WindowType { Windowed, Maximized, FullScreen }
    public static class Settings
    {
        public static bool DebugMode { get; set; }
        public static bool DemoMode { get; set; }
        public static int DemoModeMaxLevel { get; set; }
        public static bool FeatureControl_DisableWPF { get; set; }
        public static bool FeatureControl_DumpConsoleMessagesToW32Console_WindowsOnly { get; set; }
        public static string GameDeveloper { get; set; }
        public static string GameName { get; set; }
        public static SDLPoint Resolution { get; set; }
        public static string TitleScreenPath { get; set; }
        public static WindowType WindowType { get; set; }
        
    }
}
