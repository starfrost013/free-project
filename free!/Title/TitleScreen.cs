using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Shapes;

namespace Free
{
    partial class FreeSDL
    {
        public void InitTitle()
        {
            try
            {
                Game.DataContext = this; // will be changed. 

                BitmapImage XSource = (BitmapImage)LevelBackground.Source;
                XSource = new BitmapImage();
                XSource.BeginInit();

                if (Settings.TitleScreenPath == null)
                {
                    XSource.UriSource = new Uri(@".\Game\Title.png", UriKind.RelativeOrAbsolute);
                }
                else
                {
                    XSource.UriSource = new Uri(Settings.TitleScreenPath, UriKind.RelativeOrAbsolute);
                }

                XSource.DecodePixelWidth = (int)Width;
                XSource.DecodePixelHeight = (int)Height;
                LevelBackground.Width = XSource.DecodePixelWidth;
                LevelBackground.Height = XSource.DecodePixelHeight; // something.getsomething function?????????
                
                XSource.EndInit();
                UpdateLayout();

                LevelBackground.Source = XSource;

                TitleInitialized = true;
            }
            catch (InvalidOperationException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "Error loading title screen. Must exit.", "free! avant-gardé engine fatal error", 37);
            }
        }
    }
}
