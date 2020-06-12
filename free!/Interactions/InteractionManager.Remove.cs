using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public partial class MainWindow
    {
        public void Interaction_Remove(IGameObject obj, Interaction interaction)
        {
            obj.OBJCOLLISIONS--;
            obj.OBJCOLLIDEDOBJECTS.Remove(obj);
            DeleteObj(obj, interaction.OBJ2ID);
            return;
        }
    }
}
