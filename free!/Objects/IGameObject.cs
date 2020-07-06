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
/// /IGameObjects/IGameObject.cs
/// 
/// Created: 2019-12-26
/// 
/// Modified: 2019-12-26
/// 
/// Version: 1.00
/// 
/// Purpose: Allows multiple types of IGameObjects and IGameObject type-based logic by delineating an interface.
/// 
/// </summary>

namespace Free
{
    public interface IGameObject
    {
        double GameObjectACCELERATION { get; set; } // Newton would be ashamed.
        double GameObjectACCELERATIONY { get; set; }
        int GameObjectANIMNUMBER { get; set; } // Animation number for nonconstant animations
        int GameObjectCONSTANTANIMNUMBER { get; set; } // Animation number
        List<Animation> GameObjectANIMATIONS { get; set; }
        Animation AnimState { get; set; }
        bool? GameObjectCANCOLLIDE { get; set; }
        bool GameObjectCANMOVELEFT { get; set; }
        bool GameObjectCANMOVERIGHT { get; set; }
        bool GameObjectCANSNAP { get; set; }
        bool CollidesLeft { get; set; }
        bool CollidesRight { get; set; }
        bool CollidesTop { get; set; }
        bool CollidesBottom { get; set; }
        int GameObjectCOLLISIONS { get; set; } // if 0, fall.
        int CollisionsLeft { get; set; }
        int CollisionsRight { get; set; }
        int CollisionsTop { get; set; }
        int CollisionsBottom { get; set; }
        List<IGameObject> CollidedLevelObjects { get; set; } //a bad idea? maybe. 
        double GameObjectDECELERATION { get; set; }
        double GameObjectDECELERATIONY { get; set; }
        double GameObjectFORCE { get; set; }
        bool GameObjectGRAV { get; set; }
        Weapon GameObjectHELDWEAPON { get; set; }
        List<Point> GameObjectHITBOX { get; set; }
        int GameObjectID { get; set; }
        WriteableBitmap GameObjectIMAGE { get; set; }
        string GameObjectIMAGEPATH { get; set; }
        int GameObjectINTERNALID { get; set; }
        bool GameObjectISJUMPING { get; set; }
        double GameObjectMASS { get; set; }
        bool GameObjectMOVELEFT { get; set; }
        bool GameObjectMOVERIGHT { get; set; }
        bool GameObjectMOVEUP { get; set; }
        bool GameObjectMOVEDOWN { get; set; }
        LastCtrl LastControl { get; set; }
        string GameObjectNAME { get; set; }
        bool GameObjectPLAYER { get; set; }
        int GameObjectPLAYERDAMAGE { get; set; } // default -1;
        int GameObjectPLAYERHEALTH { get; set; }
        int GameObjectPLAYERLEVEL { get; set; }
        double GameObjectPLAYERLEVELDAMAGE { get; set; }
        int GameObjectPLAYERLIVES { get; set; }
        Priority GameObjectPRIORITY { get; set; }
        double GameObjectSPEED { get; set; }
        double GameObjectSPEEDY { get; set; }
        double GameObjectX { get; set; }
        double GameObjectY { get; set; }
        bool GameObjectISPLAYER { get; set; }
        double GameObjectDAMAGE { get; set; }
        double GameObjectHEALTH { get; set; }
        double GameObjectLEVEL { get; set; }
        double GameObjectLEVELDAMAGE { get; set; }
        int GameObjectLIVES { get; set; }
        AI GameObjectAI { get; set; }
        List<ScriptReference> AssociatedScriptPaths { get; set; }
        double JumpIntensity { get; set; }
        bool SpaceHeld { get; set; }
        void SetAcceleration(double x, double y); // sets the acceleration

        void ChgAcceleration(double x, double y); // changes the acceleration

        void SetDeceleration(double x, double y); // sets the deceleration

        void ChgDeceleration(double x, double y); // changes the deceleration

        void SetMass(double mass); // sets the mass

        void ChgMass(double mass); // changes the mass

        void MoveLeft();
        void MoveRight();
        void Jump();
        bool IsColliding();
        bool IsCollidingLeft();
        bool IsCollidingRight();
        bool IsCollidingTop();
        bool IsCollidingBottom();
    }
}
