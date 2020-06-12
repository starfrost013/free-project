using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// SentientBeing.cs
/// 
/// Created: 2019-12-20
/// 
/// Modified: 2020-05-27
/// 
/// Version: 1.60
/// 
/// Purpose: Defines Sentient Beings, such as players and AI-enabled objects. Obj/Being split (ver. 0.20)
/// 
/// </summary>

namespace Free
{
    public partial class SentientBeing : Obj
    {
        public override bool OBJISPLAYER { get; set; } // new for compatibility purposes
        public override double OBJDAMAGE { get; set; }
        public override double OBJHEALTH { get; set;}
        public override double OBJLEVEL { get; set; }
        public override double OBJLEVELDAMAGE { get; set; }
        public override int OBJLIVES { get; set; }
        public override AI OBJAI { get; set; } // eww
        public SentientBeing(IGameObject obj, bool ObjIsPlayer, double PlayerDamage, double BeingHealth, double BeingLevel, double BeingLevelDamage, int BeingLives, int currentIntId)
        {
            this.OBJANIMATIONS = obj.OBJANIMATIONS;
            this.OBJINTERNALID = obj.OBJINTERNALID;
            this.OBJID = obj.OBJID;
            this.OBJIMAGE = obj.OBJIMAGE;
            this.OBJIMAGEPATH = obj.OBJIMAGEPATH;
            this.OBJNAME = obj.OBJNAME;
            this.OBJGRAV = obj.OBJGRAV;
            this.OBJACCELERATION = obj.OBJACCELERATION;
            this.OBJFORCE = obj.OBJFORCE;
            this.OBJPLAYERDAMAGE = obj.OBJPLAYERDAMAGE;
            this.OBJHITBOX = obj.OBJHITBOX;
            this.OBJMASS = obj.OBJMASS;
            this.OBJPRIORITY = obj.OBJPRIORITY;
            this.OBJCANCOLLIDE = obj.OBJCANCOLLIDE;
            this.OBJCANSNAP = obj.OBJCANSNAP;
            this.OBJCOLLIDEDOBJECTS = new List<IGameObject>(); 
            this.OBJX = obj.OBJX;
            this.OBJY = obj.OBJY;
            this.OBJAI = obj.OBJAI;
            this.OBJPLAYER = obj.OBJPLAYER;
            this.OBJINTERNALID = currentIntId;
            this.OBJISPLAYER = ObjIsPlayer;
            this.OBJDAMAGE = PlayerDamage;
            this.OBJHEALTH = BeingHealth;
            this.OBJLEVEL = BeingLevel;
            this.OBJDAMAGE = BeingLevelDamage;
            this.OBJLIVES = BeingLives;
            this.OBJCANMOVELEFT = true;
            this.OBJCANMOVERIGHT = true;
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
            this.OBJISJUMPING = true;
        }

        public SentientBeing()
        {
            AssociatedScriptPaths = new List<ScriptReference>(); 
        }
        
    }

    public partial class MainWindow
    {
        public static bool IsSentientBeing(object obj)
        {
            return obj is SentientBeing;
        }
    }
}
