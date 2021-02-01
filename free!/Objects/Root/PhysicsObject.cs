using Emerald.Utilities.Wpf2Sdl;
using SDLX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; 

namespace Free
{
    public class PhysicsObject : RootObject
    {
        public Animation CurrentAnimation { get; set; }


        /// <summary>
        /// The default sprite that is used if there is no current animation frame.
        /// </summary>
        public string DefaultSpritePath { get; set; }

        /// <summary>
        /// ObjectType of this object
        /// </summary>
        public static new ObjectTypes OType = ObjectTypes.NoPhysicsObject;

        [XmlElement("PhysicsDefinition")]
        public PhysicsDefinition PhysicsDefinition { get; set; }
        public SDLPoint Position { get; set; }

        /// <summary>
        /// Temporary until we have an SDLVector class.
        /// </summary>
        public SDLPoint Size { get; set; }
        public new void SetName(string NewName) => base.SetName(NewName);

        public SDLPoint GetPosition() => Position;

        public PhysicsObject()
        {
            PhysicsDefinition = new PhysicsDefinition();
        }

        public void SetPosition(SDLPoint NewPosition)
        {
            // temp
            if (NewPosition.X >= 0 && NewPosition.X <= 9999999 && NewPosition.Y >= 0 && NewPosition.Y <= 9999999)
            {
                Position = NewPosition;
            }
        }

        public void SetPosition(double X, double Y)
        {
            // temp
            if (X >= 0 && X <= 9999999 && Y >= 0 && Y <= 9999999)
            {
                Position = new SDLPoint(X, Y); 
            }
        }
    }
}
