﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace SDLX
{
    /// <summary>
    /// 2021-02-25 (v1536)
    /// 
    /// Contains information about the current SDL scene.
    /// </summary>
    public class SDL_SceneInfo
    {
        
        public int FPS { get; set; }
        public Timer FrameTimer { get; set; }
    }
}
