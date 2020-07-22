using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// 
/// SentientBeing.cs
/// 
/// Created: 2019-12-20
/// 
/// Modified: 2020-06-20
/// 
/// Version: 1.70
/// 
/// Purpose: Defines Sentient Beings, such as players and AI-enabled IGameObjects. GameObject/Being split (ver. 0.20)
/// 
/// </summary>

namespace Free
{
    public partial class SentientBeing : GameObject
    {
        public override bool GameObjectISPLAYER { get; set; } // new for compatibility purposes
        public override double GameObjectDAMAGE { get; set; }
        public override double GameObjectHEALTH { get; set;}
        public override double GameObjectLEVEL { get; set; }
        public override double GameObjectLEVELDAMAGE { get; set; }
        public override int GameObjectLIVES { get; set; }
        public override AI GameObjectAI { get; set; } // eww
        public SentientBeing(IGameObject GameObject, bool GameObjectIsPlayer, double PlayerDamage, double BeingHealth, double BeingLevel, double BeingLevelDamage, int BeingLives, int currentIntId)
        {
            this.GameObjectANIMATIONS = GameObject.GameObjectANIMATIONS;
            this.GameObjectINTERNALID = GameObject.GameObjectINTERNALID;
            this.GameObjectID = GameObject.GameObjectID;
            this.GameObjectIMAGE = GameObject.GameObjectIMAGE;
            this.GameObjectIMAGEPATH = GameObject.GameObjectIMAGEPATH;
            this.GameObjectNAME = GameObject.GameObjectNAME;
            this.GameObjectGRAV = GameObject.GameObjectGRAV;
            this.GameObjectACCELERATION = GameObject.GameObjectACCELERATION;
            this.GameObjectFORCE = GameObject.GameObjectFORCE;
            this.GameObjectPLAYERDAMAGE = GameObject.GameObjectPLAYERDAMAGE;
            this.GameObjectHITBOX = GameObject.GameObjectHITBOX;
            this.GameObjectMASS = GameObject.GameObjectMASS;
            this.GameObjectPRIORITY = GameObject.GameObjectPRIORITY;
            this.GameObjectCANCOLLIDE = GameObject.GameObjectCANCOLLIDE;
            this.GameObjectCANSNAP = GameObject.GameObjectCANSNAP;
            this.CollidedLevelObjects = new List<IGameObject>(); 
            //this.Position.X = GameObject.Position.X;
            //this.Position.Y = GameObject.Position.Y;
            this.GameObjectAI = GameObject.GameObjectAI;
            this.GameObjectPLAYER = GameObject.GameObjectPLAYER;
            this.GameObjectINTERNALID = currentIntId;
            this.GameObjectISPLAYER = GameObjectIsPlayer;
            this.GameObjectDAMAGE = PlayerDamage;
            this.GameObjectHEALTH = BeingHealth;
            this.GameObjectLEVEL = BeingLevel;
            this.GameObjectDAMAGE = BeingLevelDamage;
            this.GameObjectLIVES = BeingLives;
            this.GameObjectCANMOVELEFT = true;
            this.GameObjectCANMOVERIGHT = true;
        }

        public SentientBeing()
        {
            AssociatedScriptPaths = new List<ScriptReference>();
            CollidedLevelObjects = new List<IGameObject>();
            GameObjectHITBOX = new List<Point>();
            GameObjectANIMATIONS = new List<Animation>();
        }

        public override void MoveLeft()
        {
            this.ChgAcceleration(-Physics.Acceleration, 0);
        }

        public override void MoveRight()
        {
            this.ChgAcceleration(Physics.Acceleration, 0);
        }

        public override void Jump()
        {
            this.ChgAcceleration(0, -Physics.JumpForce);
            this.GameObjectISJUMPING = true;
        }
        
    }

    public partial class FreeSDL
    {
        public static bool IsSentientBeing(IGameObject GameObject)
        {
            return GameObject is SentientBeing;
        }
    }
}
