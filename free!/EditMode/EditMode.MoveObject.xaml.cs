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
    /// Interaction logic for MoveGameObject.xaml
    /// </summary>
    public partial class MoveGameObject : Window
    {
        public FreeSDL MnWindow { get; set; }
        public IGameObject IGameObjectToEdit { get; set; }
        public MoveGameObject(FreeSDL FreeSDL, IGameObject GameObjectToEdit)
        {
            InitializeComponent();
            MnWindow = FreeSDL;
            IGameObjectToEdit = GameObjectToEdit;
            GameObjectSelect.Text = $"Moving IGameObject: {IGameObjectToEdit.GameObjectNAME} @ X: {IGameObjectToEdit.GameObjectX} Y: {IGameObjectToEdit.GameObjectY}";
            //clear if its already full

        }

        private void OKButton_Click(IGameObject sender, RoutedEventArgs e)
        {
            try
            {
                IGameObjectToEdit.GameObjectX = Convert.ToDouble(XPosBox.Text);
                IGameObjectToEdit.GameObjectY = Convert.ToDouble(YPosBox.Text);
                MnWindow.currentlevel.LevelIGameObjects[IGameObjectToEdit.GameObjectINTERNALID] = IGameObjectToEdit;
                GameObjectSelect.Text = $"Moving IGameObject: {IGameObjectToEdit.GameObjectNAME} @ X: {IGameObjectToEdit.GameObjectX} Y: {IGameObjectToEdit.GameObjectY}";
                //this.Close(); V0.14.1035.0 remove
            }
            catch (FormatException)
            {
                Error.Throw(new Exception("DEBUG: The user did not select an IGameObject to move when using edit mode."), ErrorSeverity.Warning, "Please input X/Y positions in the correct format.", "avant-gardé engine ver 2.11", 22);
                return;
            }

        }

        private void PickButton_Click(IGameObject sender, RoutedEventArgs e)
        {
            XPosBox.Text = Convert.ToString(MnWindow.DbgMouseClickLevelX);
            YPosBox.Text = Convert.ToString(MnWindow.DbgMouseClickLevelY);
        }

        private void CloseButton_Click(IGameObject sender, RoutedEventArgs e)
        {
            this.Close(); // close the window
        }
    }
}
