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

/// <summary>
/// 
/// Obj.cs
/// 
/// Created: 2019-10-30, iirc
/// 
/// Modified: 2020-05-26
/// 
/// Version: 2.10
///
/// Purpose: Holds the Object class and the Priority enum.
/// 
/// </summary>

namespace Free
{
    //Non-sentient Object.

    public enum Priority { Invisible=0, Background1, Background2, Low, Medium, High} //tiles/objects use Medium priority by default.

    public class Obj : IObject
    {
        public double OBJACCELERATION { get; set; } // Newton would be ashamed.
        public double OBJACCELERATIONY { get; set; }
        public int OBJANIMNUMBER { get; set; } // Animation number for nonconstant animations
        public int OBJCONSTANTANIMNUMBER { get; set; } // Animation number
        public List<Animation> OBJANIMATIONS { get; set; }
        public bool? OBJCANCOLLIDE { get; set; }
        public bool OBJCANMOVELEFT { get; set; }
        public bool OBJCANMOVERIGHT { get; set; }
        public bool OBJCANSNAP { get; set; }
        public bool ObjCollidesLeft { get; set; }
        public bool ObjCollidesRight { get; set; }
        public bool ObjCollidesTop { get; set; }
        public bool ObjCollidesBottom { get; set; }
        public int OBJCOLLISIONS { get; set; } // if 0, fall.
        public List<IObject> OBJCOLLIDEDOBJECTS { get; set; } //a bad idea? maybe. 
        public double OBJDECELERATION { get; set; }
        public double OBJDECELERATIONY { get; set; }
        public double OBJFORCE { get; set; }
        public bool OBJGRAV { get; set; }
        public Weapon OBJHELDWEAPON { get; set; }
        public List<Point> OBJHITBOX { get; set; }
        public int OBJID { get; set; }
        public WriteableBitmap OBJIMAGE { get; set; }
        public string OBJIMAGEPATH { get; set; }
        public int OBJINTERNALID { get; set; }
        public bool OBJISJUMPING { get; set; }
        public LastCtrl LastControl { get; set; }
        public double OBJMASS { get; set; }
        public bool OBJMOVELEFT { get; set; }
        public bool OBJMOVERIGHT { get; set; }
        public bool OBJMOVEUP { get; set; }
        public bool OBJMOVEDOWN { get; set; }
        public string OBJNAME { get; set; }
        public bool OBJJUMP { get; set; }
        public bool OBJPLAYER { get; set; }
        public int OBJPLAYERDAMAGE { get; set; } // default -1;
        public int OBJPLAYERHEALTH { get; set; }
        public int OBJPLAYERLEVEL { get; set; }
        public double OBJPLAYERLEVELDAMAGE { get; set; }
        public int OBJPLAYERLIVES { get; set; }
        public Priority OBJPRIORITY { get; set; }
        public double OBJSPEED { get; set; }
        public double OBJSPEEDY { get; set; }
        public double OBJX { get; set; }
        public double OBJY { get; set; }
        public List<ScriptReference> AssociatedScriptPaths { get; set; }
        public double JumpIntensity { get; set; } // Jumping intensity.
        public virtual bool OBJISPLAYER { get; set; }// new for compatibility purposes
        public virtual double OBJDAMAGE { get; set; }
        public virtual double OBJHEALTH { get; set; }
        public virtual double OBJLEVEL { get; set; }
        public virtual double OBJLEVELDAMAGE { get; set; }
        public virtual int OBJLIVES { get; set; }
        public virtual AI OBJAI { get; set; }

        public Obj()
        {
            AssociatedScriptPaths = new List<ScriptReference>(); 
        }

        public void SetAcceleration(double x, double y) // sets the acceleration
        {
            OBJACCELERATION = x;
            OBJACCELERATIONY = y;
            return;
        }

        public void ChgAcceleration(double x, double y) // changes the acceleration
        {
            OBJACCELERATION += x;
            OBJACCELERATIONY += y;
        }

        public void SetDeceleration(double x, double y) // sets the deceleration
        {
            OBJDECELERATION = x;
            OBJDECELERATIONY = y;
            return;
        }

        public void ChgDeceleration(double x, double y) // changes the deceleration
        {
            OBJDECELERATION += x;
            OBJDECELERATIONY += y;
        }

        public void SetMass(double mass) //sets the mass
        {
            OBJMASS = mass;
            return;
        }

        public void ChgMass(double mass) //changes the mass
        {
            OBJMASS += mass;
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
    }
}
