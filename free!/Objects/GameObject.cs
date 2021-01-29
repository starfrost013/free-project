using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Shapes;
using Emerald.Utilities.Wpf2Sdl;

/// <summary>
/// 
/// GameObject.cs
/// 
/// Created: 2019-10-30, iirc
/// 
/// Modified: 2020-07-22
/// 
/// Version: 3.00
///
/// Purpose: Holds the IGameObject class and the Priority enum.
/// 
/// </summary>

namespace Free
{
    //Non-sentient IGameObject.

    public enum Priority { Invisible, Background1, Background2, Low, Medium, High} //tiles/IGameObjects use Medium priority by default.

    public partial class GameObject : IGameObject
    {
        public Animation AnimState { get; set; }
        public double GameObjectACCELERATION { get; set; } // Newton would be ashamed.
        public double GameObjectACCELERATIONY { get; set; }
        public int GameObjectANIMNUMBER { get; set; } // Animation number for nonconstant animations
        public int GameObjectCONSTANTANIMNUMBER { get; set; } // Animation number
        public List<Animation> GameObjectANIMATIONS { get; set; }
        public bool? GameObjectCANCOLLIDE { get; set; }
        public bool GameObjectCANMOVELEFT { get; set; }
        public bool GameObjectCANMOVERIGHT { get; set; }
        public bool GameObjectCANSNAP { get; set; }

        /* Deprecated */
        public bool CollidesLeft { get; set; }
        public bool CollidesRight { get; set; }
        public bool CollidesTop { get; set; }
        public bool CollidesBottom { get; set; }

        /* End deprecated */
        public int GameObjectCOLLISIONS { get; set; } // if 0, fall.
        public int CollisionsLeft { get; set; }
        public int CollisionsRight { get; set; }
        public int CollisionsTop { get; set; }
        public int CollisionsBottom { get; set; }

        public List<IGameObject> CollidedLevelObjects { get; set; } //a bad idea? maybe. 
        public double GameObjectDECELERATION { get; set; }
        public double GameObjectDECELERATIONY { get; set; }
        public double GameObjectFORCE { get; set; }
        public bool GameObjectGRAV { get; set; }
        public Weapon GameObjectHELDWEAPON { get; set; }
        public List<Point> GameObjectHITBOX { get; set; }
        public int GameObjectID { get; set; }
        public WriteableBitmap GameObjectIMAGE { get; set; }
        public string GameObjectIMAGEPATH { get; set; }
        public int GameObjectINTERNALID { get; set; }
        public bool GameObjectISJUMPING { get; set; }
        public LastCtrl LastControl { get; set; }
        public double GameObjectMASS { get; set; }
        public bool GameObjectMOVELEFT { get; set; }
        public bool GameObjectMOVERIGHT { get; set; }
        public bool GameObjectMOVEUP { get; set; }
        public bool GameObjectMOVEDOWN { get; set; }
        public string GameObjectNAME { get; set; }
        public bool SpaceHeld { get; set; }
        public bool GameObjectPLAYER { get; set; }
        public int GameObjectPLAYERDAMAGE { get; set; } // default -1;
        public int GameObjectPLAYERHEALTH { get; set; }
        public int GameObjectPLAYERLEVEL { get; set; }
        public double GameObjectPLAYERLEVELDAMAGE { get; set; }
        public int GameObjectPLAYERLIVES { get; set; }
        public Priority GameObjectPRIORITY { get; set; }
        public double GameObjectSPEED { get; set; }
        public double GameObjectSPEEDY { get; set; }
        //public double Position.X { get; set; }
        //public double Position.Y { get; set; }
        public List<ScriptReference> AssociatedScriptPaths { get; set; }
        public double JumpIntensity { get; set; } // Jumping intensity.
        public SDLPoint Position { get; set; }
        public PhysicsState PhysState { get; set; }
        public virtual bool GameObjectISPLAYER { get; set; }// new for compatibility purposes
        public virtual double GameObjectDAMAGE { get; set; }
        public virtual double GameObjectHEALTH { get; set; }
        public virtual double GameObjectLEVEL { get; set; }
        public virtual double GameObjectLEVELDAMAGE { get; set; }
        public virtual int GameObjectLIVES { get; set; }
        public virtual AI GameObjectAI { get; set; }
        public SDLPoint Size { get; set; }
        public PhysicsFlags PhysFlags { get; set; }
        public GameObject()
        {
            AssociatedScriptPaths = new List<ScriptReference>();
            CollidedLevelObjects = new List<IGameObject>();
            GameObjectHITBOX = new List<Point>();
            GameObjectANIMATIONS = new List<Animation>();
            Position = new SDLPoint(); // default is 0,0
            Size = new SDLPoint();
        }

        // Core ObjectAPI
        public void SetAcceleration(double x, double y) // sets the acceleration
        {
            GameObjectACCELERATION = x;
            GameObjectACCELERATIONY = y;
            return;
        }

        public void ChgAcceleration(double x, double y) // changes the acceleration
        {
            GameObjectACCELERATION += x;
            GameObjectACCELERATIONY += y;
        }

        public void SetDeceleration(double x, double y) // sets the deceleration
        {
            GameObjectDECELERATION = x;
            GameObjectDECELERATIONY = y;
            return;
        }

        public void ChgDeceleration(double x, double y) // changes the deceleration
        {
            GameObjectDECELERATION += x;
            GameObjectDECELERATIONY += y;
        }

        public void SetMass(double mass) //sets the mass
        {
            GameObjectMASS = mass;
            return;
        }

        public void ChgMass(double mass) //changes the mass
        {
            GameObjectMASS += mass;
            return;
        }

        public virtual void MoveLeft()
        {
            return;
        }

        public virtual void MoveRight()
        {
            return;
        }

        public virtual void Jump()
        {
            return;
        }

        public void MoveObject(SDLPoint PointSDL)
        {
            Position.X = PointSDL.X;
            Position.Y = PointSDL.Y;
        }

        public void SetObjectPriority(Priority SDLPri)
        {
            GameObjectPRIORITY = SDLPri;
        }
    }
}
