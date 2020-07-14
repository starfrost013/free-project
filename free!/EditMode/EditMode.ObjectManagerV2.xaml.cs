using Emerald.Utilities.Wpf2Sdl;
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
    /// Interaction logic for EditMode.IGameObjectManagerV2.xaml
    /// </summary>

    public partial class IGameObjectManager : Window
    {
        public FreeSDL MnWindow { get; set; }
        public IGameObjectManager(FreeSDL FreeSDL)
        {
            InitializeComponent();
            MnWindow = FreeSDL;
            Refresh();
            //clear if its already full

        }

        public void Refresh()
        {
            if (GameObjectNameList.Items.Count > 0) // prevent it from getting filled multiple times
            {
                GameObjectNameList.Items.Clear();
            }

            foreach (GameObject GameObject in MnWindow.IGameObjectList)
            {
                // add the IGameObjects
                GameObjectNameList.Items.Add($"{GameObject.GameObjectNAME}");
            }

            if (GameObjectNameListLvl.Items.Count > 0)
            {
                GameObjectNameListLvl.Items.Clear();
            }

            foreach (GameObject GameObject in MnWindow.currentlevel.LevelIGameObjects)
            {
                GameObjectNameListLvl.Items.Add($"{GameObject.GameObjectNAME} (X: {GameObject.Position.X} Y: {GameObject.Position.Y})");
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GameObjectNameList.SelectedIndex == -1)
                {
                    // the user did not select anything
                    Error.Throw(new Exception("DEBUG: The user did not select an IGameObject to add when using edit mode."), ErrorSeverity.Warning, "Please select an IGameObject to add.", "avant-gardé engine ver 2.11", 14);
                }

                // EWW!! WHY DOES THIS CODE EXIST! IT IS VERY UGLY AND RETARDED! USE THE GameObject FOR GOD SAKE!
                foreach (IGameObject GameObject in MnWindow.IGameObjectList)
                {
                    if (GameObject.GameObjectID == GameObjectNameList.SelectedIndex)
                    {
                        double RoundedX = FreeSDL.RoundNearest(Convert.ToDouble(XPosBox.Text), GameObject.GameObjectIMAGE.PixelWidth / 2);
                        double RoundedY = FreeSDL.RoundNearest(Convert.ToDouble(YPosBox.Text), GameObject.GameObjectIMAGE.PixelHeight / 2);

                        MnWindow.AddIGameObject(GameObject.GameObjectID, new SDLPoint(RoundedX, RoundedY));
                    }
                }
                Refresh(); // lol lazy code
                //this.Close(); V0.14.1046.0 
            }
            catch (FormatException)
            {
                Error.Throw(new Exception("DEBUG: The user entered invalid input for the X/Y pos of the IGameObject to add when using edit mode."), ErrorSeverity.Warning, "Please enter valid X/Y input.", "avant-gardé engine ver 2.11", 15);
                return;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameObjectNameListLvl.SelectedIndex == -1)
            {
                // the user did not select anything
                Error.Throw(new Exception("DEBUG: The user did not select an IGameObject to remove when using edit mode."), ErrorSeverity.Warning, "Please select an IGameObject to remove.", "avant-gardé engine ver 2.11", 16);
            }

            MnWindow.currentlevel.LevelIGameObjects.RemoveAt(GameObjectNameListLvl.SelectedIndex);
            Refresh();
            //this.Close(); V0.14.1036.0 remove
        }

        // Restored old code. 
        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            MoveGameObject MGameObject = new MoveGameObject(MnWindow, MnWindow.currentlevel.LevelIGameObjects[GameObjectNameListLvl.SelectedIndex]); // EWW EWW EWW
            MGameObject.Owner = this;
            MGameObject.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PickButton_Click(object sender, RoutedEventArgs e)
        {
            XPosBox.Text = Convert.ToString(MnWindow.DbgMouseClickLevelX);
            YPosBox.Text = Convert.ToString(MnWindow.DbgMouseClickLevelY);
        }
    }
}
