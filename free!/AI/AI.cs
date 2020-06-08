using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// /AI/AI.cs
/// 
/// Created: 2019-12-22
/// 
/// Modified: 2019-12-30
/// 
/// Version: 1.00
/// 
/// Purpose: Handles AI core
/// 
/// </summary>

namespace Free
{
    public enum AIType { Fear, Friendly, Hatred, Nervousness, Love, Rage, Neutral }
    public class AI
    {
        public double AIIntensity { get; set; } // TEST
        public AIType AIType { get; set; }
        public int Obj1Id { get; set; }
        public int Obj2Id { get; set; }

    } 

    public partial class MainWindow 
    {
        public void HandleAI(IObject obj)
        {
            foreach (IObject obj2 in currentlevel.OBJLAYOUT)
            {
                //HandleAI
                if (IsSentientBeing(obj) & obj.OBJAI != null)
                {
                    if (obj.OBJAI.Obj2Id == obj2.OBJID)
                    {
                        switch (obj.OBJAI.AIType) //AIHandler files?
                        {
                            case AIType.Fear:
                                //FIRST PASS.  
                                AI_Fear(obj, obj2);
                                continue;
                        }
                    }
                }
            }
        }
    }
}
