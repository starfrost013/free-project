using Emerald.Utilities.Wpf2Sdl;
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
        public SDLSprite FocusedIGameObject { get; set; }
        public bool IsFocusing { get; set; }

        public GameCamera()
        {
            CameraPosition = new SDLPoint(0, 0); 
        }

        public void UpdateCamera(SDLPoint Pos)
        {
            CameraPosition = Pos;
        }

        public void FocusOnObject(SDLSprite SDLSprite)
        {
            FocusedIGameObject = SDLSprite;
            CameraPosition = FocusedIGameObject.Position;
            IsFocusing = true; 
        }

    }
}
