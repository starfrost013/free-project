using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Free
{ 
    partial class MainWindow
    {
        public void SetFullscreen()
        {
            switch (FullScreen)
            {
                case false:
                    FullScreen = true;
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    Scrollbar.Width = this.Width;
                    Scrollbar.Height = this.Height;

                    if (Gamestate != GameState.Game && Gamestate != GameState.Pause)
                    {
                        Game.Width = currentlevel.Size.X;
                        Game.Height = currentlevel.Size.Y;
                    }

                    return;
                case true:
                    FullScreen = false;
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;
                    Scrollbar.Width = this.Width;
                    Scrollbar.Height = this.Height;

                    if (Gamestate != GameState.Game && Gamestate != GameState.Pause)
                    {
                        Game.Width = currentlevel.Size.X;
                        Game.Height = currentlevel.Size.Y;
                    }

                    return;
            }
        }
    }
}
