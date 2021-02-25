using Emerald.Core;
using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class Level
    {
        /// <summary>
        /// Sets the acceleration for player #playerID to acceleration
        /// </summary>
        /// <param name="PlayerID"><Player ID to use/param>
        /// <param name="Acceleration">Acceleration (SDLPoint)</param>
        public void SetPlayerAcceleration(int PlayerID, SDLPoint Acceleration)
        {
            List<IGameObject> SDLPlayers = GetPlayers();

            if (SDLPlayers.Count - 1 < PlayerID || PlayerID < 0 || PlayerID > SDLPlayers.Count - 1)
            {
                Error.Throw(null, ErrorSeverity.Warning, $"Attempted to set acceleration for invalid player #{PlayerID}, maximum player ID {SDLPlayers.Count - 1}. Attempting to continue...");
                return; 
            }
            else
            {
                SDLPlayers[PlayerID].SetAcceleration(Acceleration.X, Acceleration.Y); 
            }
        }
    }
}
