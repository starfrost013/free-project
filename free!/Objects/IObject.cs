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
/// /Objects/IObject.cs
/// 
/// Created: 2019-12-26
/// 
/// Modified: 2019-12-26
/// 
/// Version: 1.00
/// 
/// Purpose: Allows multiple types of objects and object type-based logic by delineating an interface.
/// 
/// </summary>

namespace Free
{
    public interface IObject
    {
        double OBJACCELERATION { get; set; } // Newton would be ashamed.
        double OBJACCELERATIONY { get; set; }
        int OBJANIMNUMBER { get; set; } // Animation number for nonconstant animations
        int OBJCONSTANTANIMNUMBER { get; set; } // Animation number
        List<Animation> OBJANIMATIONS { get; set; }
        bool? OBJCANCOLLIDE { get; set; }
        bool OBJCANMOVELEFT { get; set; }
        bool OBJCANMOVERIGHT { get; set; }
        bool OBJCANSNAP { get; set; }
        bool ObjCollidesLeft { get; set; }
        bool ObjCollidesRight { get; set; }
        bool ObjCollidesTop { get; set; }
        bool ObjCollidesBottom { get; set; }
        int OBJCOLLISIONS { get; set; } // if 0, fall.
        List<IObject> OBJCOLLIDEDOBJECTS { get; set; } //a bad idea? maybe. 
        double OBJDECELERATION { get; set; }
        double OBJDECELERATIONY { get; set; }
        double OBJFORCE { get; set; }
        bool OBJGRAV { get; set; }
        Weapon OBJHELDWEAPON { get; set; }
        List<Point> OBJHITBOX { get; set; }
        int OBJID { get; set; }
        WriteableBitmap OBJIMAGE { get; set; }
        string OBJIMAGEPATH { get; set; }
        int OBJINTERNALID { get; set; }
        bool OBJISJUMPING { get; set; }
        double OBJMASS { get; set; }
        bool OBJMOVELEFT { get; set; }
        bool OBJMOVERIGHT { get; set; }
        bool OBJMOVEUP { get; set; }
        bool OBJMOVEDOWN { get; set; }
        LastCtrl LastControl { get; set; }
        string OBJNAME { get; set; }
        bool OBJPLAYER { get; set; }
        int OBJPLAYERDAMAGE { get; set; } // default -1;
        int OBJPLAYERHEALTH { get; set; }
        int OBJPLAYERLEVEL { get; set; }
        double OBJPLAYERLEVELDAMAGE { get; set; }
        int OBJPLAYERLIVES { get; set; }
        Priority OBJPRIORITY { get; set; }
        double OBJSPEED { get; set; }
        double OBJSPEEDY { get; set; }
        double OBJX { get; set; }
        double OBJY { get; set; }
        bool OBJISPLAYER { get; set; }
        double OBJDAMAGE { get; set; }
        double OBJHEALTH { get; set; }
        double OBJLEVEL { get; set; }
        double OBJLEVELDAMAGE { get; set; }
        int OBJLIVES { get; set; }
        AI OBJAI { get; set; }

        
        void SetAcceleration(double x, double y); // sets the acceleration

        void ChgAcceleration(double x, double y); // changes the acceleration

        void SetDeceleration(double x, double y); // sets the deceleration

        void ChgDeceleration(double x, double y); // changes the deceleration

        void SetMass(double mass); // sets the mass

        void ChgMass(double mass); // changes the mass

        void MoveLeft();
        void MoveRight();
        void Jump();

    }
}
