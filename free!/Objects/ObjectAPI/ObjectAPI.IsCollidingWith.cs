using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class GameObject : IGameObject
    {
        public bool IsCollidingWith(IGameObject Obj2)
        {
            return (CollidedLevelObjects.Contains(Obj2));
        }
    }
}
