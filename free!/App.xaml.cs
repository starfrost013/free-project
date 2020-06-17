using SDLX;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Free
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Activated(object sender, EventArgs e)
        {

            /*
            if (Settings.UseSDLX)
            {
                SDL_Init();
            }*/

            MainWindow MnWindow = new MainWindow();
            MnWindow.Show();
        }
    }
}
