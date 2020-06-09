using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
    public partial class Level
    {
        public BitmapImage BG { get; set; }
        public string BGPATH { get; set; }
        public Point BackgroundSize { get; set; }
        public int ID { get; set; }
        public BitmapImage LOADINGSCREEN { get; set; }
        public string MUSICPATH { get; set; }
        public MediaPlayer MUSICPLAYER { get; set; }
        public string NAME { get; set; }
        public List<IObject> OBJLAYOUT { get; set; } // 2do: load this in this class. (0.01X)
        public string OBJLAYOUTPATH { get; set; }
        public double? PLRKILLY { get; set; } 
        public Point PlayerStartPosition { get; set; }
        public double? PLRSTARTX { get; set; }
        public double? PLRSTARTY { get; set; }
        public string TEXTPATH { get; set; }
        public Point Size { get; set; }
        public Level()
        {
            this.OBJLAYOUT = new List<IObject>();
        }
    }
}
