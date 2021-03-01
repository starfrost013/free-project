using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class DeleteIGameObjectCommand : RootCommand
    {

        public DeleteIGameObjectCommand()
        {
            //MnWindow = (FreeSDL)Application.Current.MainWindow;
        }

        public new void Verify()
        {
            // Verify that we are actaully inserting an IGameObject. 
            if (Parameters.Count != 1) ScriptError.Throw("DeleteIGameObject(ID): 1 parameter required", 10, 0, "Temp");
        }

        public new ScriptReturnValue Execute()
        {
            //MnWindow.currentlevel.LevelIGameObjects.Add()
            MnWindow.currentlevel.DeleteIGameObject((int)GetParameter("ID")); 

            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "The operation completed successfully - an IGameObject has been added." };
        }
    }
}
