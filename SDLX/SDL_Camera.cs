﻿using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLX
{
    public class GameCamera
    {
        public SDLPoint CameraPosition { get; set; } // The center of the camera.
        public SDLSprite FocusedObject { get; set; }
        public bool IsFocusing { get; set; }

        public void UpdateCamera(SDLPoint Pos)
        {
            CameraPosition = Pos;
        }

        public void SetFocusedObject(SDLSprite SDLSprite)
        {
            FocusedObject = SDLSprite;
            CameraPosition = FocusedObject.Position;
            IsFocusing = true; 
        }

    }
}