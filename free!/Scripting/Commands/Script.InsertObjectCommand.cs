using Emerald.Utilities.Wpf2Sdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free
{
    public class InsertIGameObjectCommand : RootCommand
    {

      

        public InsertIGameObjectCommand()
        {
            MnWindow = SDL_Stage0_Init.SDLEngine;
        }

        public new void Verify()
        {
            // Verify that we are actaully inserting an IGameObject. 
            if (Parameters.Count != 3) ScriptError.Throw("InsertIGameObject(ID, x, y): 3 parameters required", 10, 0, "Temp");
        }

        public new ScriptReturnValue Execute()
        {
            //MnWindow.currentlevel.LevelIGameObjects.Add()
            MnWindow.AddIGameObject((int)GetParameter("ID"), new SDLPoint((double)GetParameter("X"), (double)GetParameter("Y")));

            return new ScriptReturnValue { ReturnCode = 0, ReturnInformation = "The operation completed successfully - an IGameObject has been added." };
        }
    }
}
