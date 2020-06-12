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
using System.Windows.Shapes;

namespace Free
{
    /// <summary>
    /// Interaction logic for MoveObj.xaml
    /// </summary>
    public partial class MoveObj : Window
    {
        public MainWindow MnWindow { get; set; }
        public IGameObject objectToEdit { get; set; }
        public MoveObj(MainWindow MainWindow, IGameObject objToEdit)
        {
            InitializeComponent();
            MnWindow = MainWindow;
            objectToEdit = objToEdit;
            ObjSelect.Text = $"Moving Object: {objectToEdit.OBJNAME} @ X: {objectToEdit.OBJX} Y: {objectToEdit.OBJY}";
            //clear if its already full

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objectToEdit.OBJX = Convert.ToDouble(XPosBox.Text);
                objectToEdit.OBJY = Convert.ToDouble(YPosBox.Text);
                MnWindow.currentlevel.OBJLAYOUT[objectToEdit.OBJINTERNALID] = objectToEdit;
                ObjSelect.Text = $"Moving Object: {objectToEdit.OBJNAME} @ X: {objectToEdit.OBJX} Y: {objectToEdit.OBJY}";
                //this.Close(); V0.14.1035.0 remove
            }
            catch (FormatException)
            {
                Error.Throw(new Exception("DEBUG: The user did not select an object to move when using edit mode."), ErrorSeverity.Warning, "Please input X/Y positions in the correct format.", "avant-gardé engine ver 2.11", 22);
                return;
            }

        }

        private void PickButton_Click(object sender, RoutedEventArgs e)
        {
            XPosBox.Text = Convert.ToString(MnWindow.DbgMouseClickLevelX);
            YPosBox.Text = Convert.ToString(MnWindow.DbgMouseClickLevelY);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // close the window
        }
    }
}
