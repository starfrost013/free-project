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
    partial class FreeSDL
    {
        public void SetFullscreen()
        {

            if (currentlevel == null) return; 

            switch (FullScreen)
            {
                case false:
                    FullScreen = true;
                    //this.WindowStyle = WindowStyle.None;
                    //this.WindowState = WindowState.Maximized;


                    return;
                case true:
                    FullScreen = false;
                    //this.WindowStyle = WindowStyle.SingleBorderWindow;
                    //this.WindowState = WindowState.Normal;


                    return;
            }
        }
    }
}
