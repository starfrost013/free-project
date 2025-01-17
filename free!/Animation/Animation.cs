﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

/// <summary>
/// 
/// File Name: Animation.cs
/// 
/// Created: 2019-11-22
/// 
/// Modified: 2021-01-29
/// 
/// Version: 2.00
/// 
/// Engine Version 2.21.1284+ 
/// 
/// Purpose: Holds the animation class
/// 
/// </summary>

namespace Free
{
    public enum AnimationType { Constant, Drilldashing, Dying, FacingLeft, FacingRight, Explode, General, Hit, Suffocating, Running, Walking }
    public class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        public AnimationType animationType { get; set; }
        public List<WriteableBitmap> animImages { get; set; }
        public List<int> numMs { get; set; } // this corresponds to animImage, so animImage[0] will play for numMs[0] milliseconds
    }
}
