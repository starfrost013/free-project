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
        public Game SDLGame { get; set; }
        private void Application_Activated(object sender, EventArgs e)
        {
            SDLGame = new Game();


            MainWindow MnWindow = new MainWindow();
            MnWindow.Show();
        }
    }
}
