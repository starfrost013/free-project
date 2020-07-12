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
        // FeatureControl for demos
        public static bool FeatureControl_DisableAI { get; set; }
        public static bool FeatureControl_DisableAnimation { get; set; }
        public static bool FeatureControl_DisableCollision { get; set; }
        public static bool FeatureControl_DisableGTDebug { get; set; }
        public static bool FeatureControl_DisablePhysics { get; set; }
        public static bool FeatureControl_DisableScripting { get; set; }
        public static bool FeatureControl_DisableSkills { get; set; }
        public static bool FeatureControl_DisableSDL_PublicDemosOnly { get; set; }
        public static bool FeatureControl_DisableWPF { get; set; }
        public static bool FeatureControl_DumpConsoleMessagesToW32Console_WindowsOnly { get; set; }
        public static bool FeatureControl_UseCollisionV2 { get; set; }
        public static bool FeatureControl_UseEngineCore { get; set; }
        public static bool FeatureControl_UseELS { get; set; }
        public static bool FeatureControl_UseFreeUI { get; set; }
        public static bool FeatureControl_UsePhysicsV2 { get; set; }
        public static string GameDeveloper { get; set; }
        public static string GameName { get; set; }
        public static SDLPoint Resolution { get; set; }
        public static string TitleScreenPath { get; set; }
        public static WindowType WindowType { get; set; }
        
    }
}
