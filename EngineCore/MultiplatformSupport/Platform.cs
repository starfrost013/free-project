using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// EngineCore/Settings/Platform.cs
/// 
/// Created: 2020-06-15
/// 
/// Modified: 2020-07-15
/// 
/// Version: 1.00
/// 
/// Purpose: Holds the platforms that Emerald is designed to run on, as well as a static method to determine what the current platform is 
/// </summary>

namespace EngineCore
{
    public enum AvailablePlatforms
    {
        // Emerald Game Engine, Win32, .NET Framework (theoretically 4.7.1, practically 4.7.2+) (legacy systems)
        EmeraldPlatformWin32NetFX = 0,
        // Emerald Game Engine, Win32, .NET Core (3.1+) (Windows 7 SP1, Windows 8.x, Windows 10 Redstone 1 and later)
        EmeraldPlatformWin32NetCore = 1,
        // Emerald Game Engnie, Win64, .NET Framework (4.7.2/4.8)
        EmeraldPlatformWin64NetFX = 2,
        // Emerald Game Engine, Win64, .NET Core (3.1+)
        EmeraldPlatformWin64NetCore = 3,
        // Emerald Game Engine, Mac OS x64, 10.13+, .NET Core (3.1+)
        EmeraldPlatformMac64NetCore = 4,
        // Emerald Game Engine, Mac OS x64 AArch64, 11.0+, .NET Core 3.1 / .NET 5+ (futureproofing)
        EmeraldPlatformMacARMNetCore = 5,
        // Emerald Game Engine, Linux, Alpine 3.1 / Fedora 29 / OpenSUSE 15.0 / Ubuntu 16.04, etc+), x64, .NET Core (3.1+)
        EmeraldPlatformLinux64NetCore = 6

    }

    public static class Platforms
    {
        public static AvailablePlatforms CurrentPlatform { get; set; }
        
        /// <summary>
        /// Determines the current Emerald platform using .NET standard apis.
        /// </summary>
        public static void DetermineCurrentPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) // if we are running on windows
            {
                if (RuntimeInformation.FrameworkDescription.Contains(".NET Core"))
                {
                    if (RuntimeInformation.OSArchitecture == Architecture.X64)
                    {
                        CurrentPlatform = AvailablePlatforms.EmeraldPlatformWin32NetCore;
                    }
                    else // if we do not have x64 we are probably x86
                    {
                        CurrentPlatform = AvailablePlatforms.EmeraldPlatformWin32NetCore;
                    }
                }
                else // if not .net core, then .net framework (we do not support .net native)
                {
                    if (RuntimeInformation.OSArchitecture == Architecture.X64)
                    {
                        CurrentPlatform = AvailablePlatforms.EmeraldPlatformWin64NetFX;
                    }
                    else
                    {
                        CurrentPlatform = AvailablePlatforms.EmeraldPlatformWin32NetFX;
                    }
                }
            }
            else // if not windows, we are running on OSX/Linux and thus .NET Core
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) // if we are not running on osx
                {
                    if (RuntimeInformation.OSArchitecture == Architecture.X64)
                    {
                        CurrentPlatform = AvailablePlatforms.EmeraldPlatformMac64NetCore;
                    }
                    else // .NET Core 3.1 only supports 10.13+, and mac x86 is dead, so otherwise we would be running on ARM64 Mac OS 11.0+
                    {
                        CurrentPlatform = AvailablePlatforms.EmeraldPlatformMacARMNetCore;
                    }
                }
                else // then we are running Linux - .NET is only available as Core x64 for Linux, so we are using that
                {
                    CurrentPlatform = AvailablePlatforms.EmeraldPlatformLinux64NetCore;
                }
            }
        }
    }
}
