using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

/// <summary>
/// 
/// /Settings/Settings.cs
/// 
/// Created: 2019-12-22
/// 
/// Modified: 2019-12-22
/// 
/// Version: 1.00
/// 
/// Purpose: Handles settings
/// 
/// </summary>

namespace Free
{
    public enum WindowType { Windowed=0, Maximized, FullScreen }
    public static class Settings
    {
        public static bool DebugMode { get; set; }
        public static bool DemoMode { get; set; }
        public static int DemoModeMaxLevel { get; set; }
        public static string GameDeveloper { get; set; }
        public static string GameName { get; set; }
        public static Point Resolution { get; set; }
        public static string TitleScreenPath { get; set; }
        public static bool UseSDLX { get; set; }
        public static WindowType WindowType { get; set; }
        
    }
}
