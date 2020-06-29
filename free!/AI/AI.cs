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
        public int GameObject1Id { get; set; }
        public int GameObject2Id { get; set; }

    } 

    public partial class FreeSDL 
    {
        public void HandleAI(IGameObject GameObject)
        {
            foreach (IGameObject GameObject2 in currentlevel.LevelIGameObjects)
            {
                //HandleAI
                if (IsSentientBeing(GameObject) & GameObject.GameObjectAI != null)
                {
                    if (GameObject.GameObjectAI.GameObject2Id == GameObject2.GameObjectID)
                    {
                        switch (GameObject.GameObjectAI.AIType) //AIHandler files?
                        {
                            case AIType.Fear:
                                //FIRST PASS.  
                                AI_Fear(GameObject, GameObject2);
                                continue;
                        }
                    }
                }
            }
        }
    }
}
