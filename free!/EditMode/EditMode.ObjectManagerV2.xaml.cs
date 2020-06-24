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
    /// Interaction logic for EditMode.ObjectManagerV2.xaml
    /// </summary>

    public partial class ObjectManager : Window
    {
        public FreeSDL MnWindow { get; set; }
        public ObjectManager(FreeSDL FreeSDL)
        {
            InitializeComponent();
            MnWindow = FreeSDL;
            Refresh();
            //clear if its already full

        }

        public void Refresh()
        {
            if (ObjNameList.Items.Count > 0) // prevent it from getting filled multiple times
            {
                ObjNameList.Items.Clear();
            }

            foreach (Obj obj in MnWindow.ObjectList)
            {
                // add the objects
                ObjNameList.Items.Add($"{obj.OBJNAME}");
            }

            if (ObjNameListLvl.Items.Count > 0)
            {
                ObjNameListLvl.Items.Clear();
            }

            foreach (Obj obj in MnWindow.currentlevel.LevelObjects)
            {
                ObjNameListLvl.Items.Add($"{obj.OBJNAME} (X: {obj.OBJX} Y: {obj.OBJY})");
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ObjNameList.SelectedIndex == -1)
                {
                    // the user did not select anything
                    Error.Throw(new Exception("DEBUG: The user did not select an object to add when using edit mode."), ErrorSeverity.Warning, "Please select an object to add.", "avant-gardé engine ver 2.11", 14);
                }

                // EWW!! WHY DOES THIS CODE EXIST! IT IS VERY UGLY AND RETARDED! USE THE OBJ FOR GOD SAKE!
                foreach (IGameObject obj in MnWindow.ObjectList)
                {
                    if (obj.OBJID == ObjNameList.SelectedIndex)
                    {
                        Obj objx = new Obj();
                        objx.OBJANIMATIONS = obj.OBJANIMATIONS;
                        objx.OBJINTERNALID = MnWindow.currentlevel.LevelObjects.Count;
                        objx.OBJID = obj.OBJID;
                        objx.OBJIMAGE = obj.OBJIMAGE;
                        objx.OBJIMAGEPATH = obj.OBJIMAGEPATH;
                        objx.OBJNAME = obj.OBJNAME;
                        objx.OBJGRAV = obj.OBJGRAV;
                        objx.OBJACCELERATION = obj.OBJACCELERATION;
                        objx.OBJFORCE = obj.OBJFORCE;
                        objx.OBJPLAYER = obj.OBJPLAYER;
                        objx.OBJPLAYERDAMAGE = obj.OBJPLAYERDAMAGE;
                        objx.OBJCANCOLLIDE = obj.OBJCANCOLLIDE;
                        objx.OBJHITBOX = obj.OBJHITBOX;
                        objx.OBJMASS = obj.OBJMASS;
                        objx.OBJPRIORITY = obj.OBJPRIORITY;
                        objx.OBJCANSNAP = obj.OBJCANSNAP;
                        objx.OBJAI = obj.OBJAI;
                        objx.OBJCOLLIDEDOBJECTS = new List<IGameObject>(); // yeah.
                        objx.OBJX = FreeSDL.RoundNearest(Convert.ToDouble(XPosBox.Text), objx.OBJIMAGE.PixelWidth / 2);
                        objx.OBJY = FreeSDL.RoundNearest(Convert.ToDouble(YPosBox.Text), objx.OBJIMAGE.PixelHeight / 2);
                        XPosBox.Text = objx.OBJX.ToString();
                        YPosBox.Text = objx.OBJY.ToString();
                        objx.OBJX = Convert.ToDouble(XPosBox.Text);
                        objx.OBJY = Convert.ToDouble(YPosBox.Text);
                        MnWindow.currentlevel.LevelObjects.Add(objx);
                    }
                }
                Refresh(); // lol lazy code
                //this.Close(); V0.14.1046.0 
            }
            catch (FormatException)
            {
                Error.Throw(new Exception("DEBUG: The user entered invalid input for the X/Y pos of the object to add when using edit mode."), ErrorSeverity.Warning, "Please enter valid X/Y input.", "avant-gardé engine ver 2.11", 15);
                return;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ObjNameListLvl.SelectedIndex == -1)
            {
                // the user did not select anything
                Error.Throw(new Exception("DEBUG: The user did not select an object to remove when using edit mode."), ErrorSeverity.Warning, "Please select an object to remove.", "avant-gardé engine ver 2.11", 16);
            }

            MnWindow.currentlevel.LevelObjects.RemoveAt(ObjNameListLvl.SelectedIndex);
            Refresh();
            //this.Close(); V0.14.1036.0 remove
        }

        // Restored old code. 
        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            MoveObj MObj = new MoveObj(MnWindow, MnWindow.currentlevel.LevelObjects[ObjNameListLvl.SelectedIndex]); // EWW EWW EWW
            MObj.Owner = this;
            MObj.Show();
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
