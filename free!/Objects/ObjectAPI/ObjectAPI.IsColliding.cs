using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class GameObject : IGameObject
    {
        public bool IsColliding()
        {
            return (CollisionsLeft == 0
                && CollisionsRight == 0
                && CollisionsTop == 0
                && CollisionsBottom == 0); 
        }
    }
}
