using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// IGameObject API - Get Players (IGameObjects\IGameObjectAPI\IGameObjectAPI.GetPlayers.cs)
/// 
/// Created: 2020-06-13
/// 
/// Modified: 2020-06-13
/// 
/// Version: 1.00
/// 
/// </summary>
namespace Free
{
    public partial class Level
    {
        /// <summary>
        /// Gets all of the players inside this level.
        /// </summary>
        /// <returns></returns>
        public List<IGameObject> GetPlayers()
        {
            List<IGameObject> Players = new List<IGameObject>();

            foreach (IGameObject LevelIGameObject in LevelIGameObjects)
            {
                if (LevelIGameObject.GameObjectISPLAYER) Players.Add(LevelIGameObject);
            }

            return Players; 
        }
    }
}
