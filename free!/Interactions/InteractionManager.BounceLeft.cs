﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    partial class FreeSDL // move to obj in the obj/player split?
    {
        public void Interaction_BounceLeft(IGameObject obj)
        {
            obj.OBJACCELERATION = -28.5;
            obj.OBJSPEED = -Physics.MaxSpeed;
            return;
        }
    }
}
