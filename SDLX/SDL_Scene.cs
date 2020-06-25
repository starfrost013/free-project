using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>

namespace SDLX
{
    public class GameScene
    {
        public List<SDLSprite> GameObjects { get; set; }
        public GameCamera GameCamera { get; set; } 

        public GameScene()
        {
            GameCamera = new GameCamera();
            GameObjects = new List<SDLSprite>();
        }

        public void OnChangeScene()
        {
            GameObjects.Clear(); 
        }
    }
}
