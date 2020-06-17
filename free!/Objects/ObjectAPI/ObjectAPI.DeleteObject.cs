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
        /// Delete the object with internal ID internalID.
        /// </summary>
        /// <param name="InternalID">The Internal Object ID you wish to remove.</param>
        public void DeleteObject(int InternalID)
        {
            foreach (IGameObject GameObject in LevelObjects)
            {
                if (GameObject.OBJINTERNALID == InternalID)
                {
                    LevelObjects.RemoveAt(InternalID);
                    return;
                }
            }
        }
    }
}
