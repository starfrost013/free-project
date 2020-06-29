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
        /// Delete the IGameObject with internal ID internalID.
        /// </summary>
        /// <param name="InternalID">The Internal IGameObject ID you wish to remove.</param>
        public void DeleteIGameObject(int InternalID)
        {
            foreach (IGameObject GameIGameObject in LevelIGameObjects)
            {
                if (GameIGameObject.GameObjectINTERNALID == InternalID)
                {
                    LevelIGameObjects.RemoveAt(InternalID);
                    return;
                }
            }
        }
    }
}
