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
        public void SetGameObjectSize(int ID, SDLPoint Size)
        {
            foreach (IGameObject GameObject in LevelIGameObjects)
            {
                if (GameObject.GameObjectINTERNALID == ID)
                {
                    //GameObject. = Size; 
                }
            }
        }
    }
}
