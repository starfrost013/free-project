using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// PhysicsConstants.cs
/// 
/// Created: 2019-11-18
/// 
/// Modified: 2020-06-03
/// 
/// Version: 1.20
/// 
/// Purpose: Handles constants for physics handling.
/// 
/// </summary>

namespace Free
{
    public static class Physics
    {
        public static double Acceleration = 0.4;
        public static double Friction = 0.275;
        public static double Gravity = 0.225;

        /// <summary>
        /// deprecated
        /// </summary>
        public static double JumpForce = 7.25; 
        public static double MaxSpeed = 8.5;
        public static double SpeedConst = 13;

        /// <summary>
        /// deprecated
        /// </summary>
        public static double MaxJumpIntensity = 1.5; 
    }
}
