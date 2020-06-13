using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

// Currently, the movement speed is locked to the game's framerate. This is very very very very very bad and should be avoided.

namespace Free
{
    public partial class MainWindow // Potentially have a renderer class
    {
        public void DrawScene() // draws the scene, starting with the lowest priority objects and then going up to the highest priority ones, then draws any text objects and the debug display if it is on. 
        {
            Priority currentPriority = Priority.Background1;
            Game.Children.Clear();
            while (currentPriority <= Priority.High)
            {
                if (currentPriority != Priority.Invisible)
                {
                    if (Gamestate == GameState.Game || Gamestate == GameState.EditMode)
                    {
                        // v2 for 0.06+
                        for (int i = 0; i < currentlevel.LevelObjects.Count; i++)
                        {
                            IGameObject CurObj = currentlevel.LevelObjects[i];

                            if (CurObj.OBJX - Scrollbar.HorizontalOffset > 0 - CurObj.OBJIMAGE.PixelWidth && CurObj.OBJX - Scrollbar.HorizontalOffset < this.Width + CurObj.OBJIMAGE.PixelWidth & CurObj.OBJY - Scrollbar.VerticalOffset > 0 - CurObj.OBJIMAGE.PixelHeight && CurObj.OBJY - Scrollbar.VerticalOffset < this.Height + CurObj.OBJIMAGE.PixelHeight || IsSentientBeing(CurObj))
                            { // optimization (bld 1037-41) 
                                if (CurObj.OBJPRIORITY == currentPriority)
                                {
                                    if (CurObj.OBJPLAYER == true & Gamestate == GameState.Game)
                                    {
                                        Scrollbar.ScrollToHorizontalOffset(CurObj.OBJX - this.Width / 6); // level scrolling
                                        Scrollbar.ScrollToVerticalOffset(CurObj.OBJY - this.Width / 6); // vertical level scrolling
                                    }

                                    Rectangle rectangle = new Rectangle();
                                    rectangle.Height = CurObj.OBJIMAGE.Height;
                                    rectangle.Width = CurObj.OBJIMAGE.Width;
                                    rectangle.StrokeThickness = 0;
                                    HandleAnimations(CurObj);

                                    if (Gamestate == GameState.Game & CurObj.OBJGRAV)
                                    {
                                        HandlePhys(CurObj);

                                        // Just a test. Onto the physics thread this goes
                                        if (CurObj.OBJCANCOLLIDE != false)
                                        { // only handle collisions if cancollide is true or null
                                            HandleCollision(CurObj);
                                        }
                                    }

                                    HandleAI(CurObj);
                                    Canvas.SetLeft(rectangle, CurObj.OBJX);
                                    Canvas.SetTop(rectangle, CurObj.OBJY);
                                    rectangle.Fill = new ImageBrush(CurObj.OBJIMAGE);
                                    Game.Children.Add(rectangle);

                                    RenderWeapons(CurObj); 
                                }
                            }
                        }

                        if (currentPriority == Priority.High)
                        {
                            if (TextList.Count > 0)
                            {
                                foreach (AGTextBlock Textblock in TextList)
                                {
                                    if (Textblock.IsDisplayed)
                                    {
                                        Canvas.SetLeft(Textblock, Textblock.GamePos.X);
                                        Canvas.SetTop(Textblock, Textblock.GamePos.Y);

                                        // DEBUG CODE // 
                                        if (Textblock.TextName == "GlobalTimerDebug" && Settings.DebugMode)
                                        {
                                            int minutes = GlobalTimer / 60 / 60;
                                            int seconds = (GlobalTimer / 60) % 60;
                                            double hundredths = RoundNearest(((GlobalTimer * 60) % 59) * 1.4, 1);

                                            if (seconds > 9)
                                            {
                                                SetText(Textblock, $"--DEBUG--\nFrame No: {GlobalTimer.ToString()}\nLevel Time: {minutes.ToString()}:{seconds.ToString()}.{hundredths.ToString()}\nCurrent Window Size: {this.Width},{this.Height}\n\n-Settings-\nDebug Mode: {Settings.DebugMode}\nDemo Mode: {Settings.DemoMode}\nDemo Mode Max Level: {Settings.DemoModeMaxLevel}\nGame Name: {Settings.GameName}\nResolution: {Settings.Resolution}\nTitle Screen Path: {Settings.TitleScreenPath} \nWindow Size: {Settings.WindowType}");
                                            }
                                            else
                                            {
                                                SetText(Textblock, $"--DEBUG--\nFrame No: {GlobalTimer.ToString()}\nLevel Time: {minutes.ToString()}:0{seconds.ToString()}.{hundredths.ToString()}\nCurrent Window Size: {this.Width},{this.Height}\n\n-Settings-\nDebug Mode: {Settings.DebugMode}\nDemo Mode: {Settings.DemoMode}\nDemo Mode Max Level: {Settings.DemoModeMaxLevel}\nGame Name: {Settings.GameName}\nResolution: {Settings.Resolution}\nTitle Screen Path: {Settings.TitleScreenPath} \nWindow Size: {Settings.WindowType}");
                                            }

                                            Canvas.SetLeft(Textblock, Scrollbar.HorizontalOffset);
                                            Canvas.SetTop(Textblock, Scrollbar.VerticalOffset);
                                        }
                                        // END DEBUG CODE //

                                        Game.Children.Add(Textblock);
                                    }
                                }
                            }
                        }
                        currentPriority++; // increment the object priority we are currently drawing. this allows us to put objects in front of each other etc (currently 5 layers)
                    }
                }
            }

            //draw text

            UpdateLayout();
        }

        public void RenderWeapons(IGameObject CurObj)
        {
            if (CurObj.OBJHELDWEAPON != null) // weapon drawing
            {
                Rectangle WeaponRectangle = new Rectangle();
                WeaponRectangle.Height = CurObj.OBJHELDWEAPON.WEAPONIMAGE.Height;
                WeaponRectangle.Width = CurObj.OBJHELDWEAPON.WEAPONIMAGE.Width;
                WeaponRectangle.StrokeThickness = 0;
                CurObj.OBJHELDWEAPON.WEAPONPOSITIONX = CurObj.OBJX + CurObj.OBJIMAGE.PixelWidth / 1.33;
                CurObj.OBJHELDWEAPON.WEAPONPOSITIONY = CurObj.OBJY + CurObj.OBJIMAGE.PixelHeight / 2;
                Canvas.SetLeft(WeaponRectangle, CurObj.OBJHELDWEAPON.WEAPONPOSITIONX);
                Canvas.SetTop(WeaponRectangle, CurObj.OBJHELDWEAPON.WEAPONPOSITIONY);
                WeaponRectangle.Fill = new ImageBrush(CurObj.OBJHELDWEAPON.WEAPONIMAGE); // the image.
                Game.Children.Add(WeaponRectangle);

                if (CurObj.OBJHELDWEAPON.WEAPONAMMOLIST.Count > 0) // ammo
                {
                    for (int j = 0; j < CurObj.OBJHELDWEAPON.WEAPONAMMOLIST.Count; j++)
                    {
                        Ammo ammo = CurObj.OBJHELDWEAPON.WEAPONAMMOLIST[j];
                        if (ammo.X - Scrollbar.HorizontalOffset > 0 - CurObj.OBJHELDWEAPON.WEAPONIMAGEAMMO.PixelWidth && ammo.X - Scrollbar.HorizontalOffset < this.Width + CurObj.OBJHELDWEAPON.WEAPONIMAGEAMMO.PixelWidth && ammo.Y - Scrollbar.VerticalOffset > 0 - CurObj.OBJHELDWEAPON.WEAPONIMAGEAMMO.PixelHeight & ammo.Y - Scrollbar.VerticalOffset < this.Width + CurObj.OBJHELDWEAPON.WEAPONIMAGEAMMO.PixelHeight)
                        {
                            Rectangle AmmoRectangle = new Rectangle();
                            AmmoRectangle.Width = CurObj.OBJHELDWEAPON.WEAPONIMAGEAMMO.Width;
                            AmmoRectangle.Height = CurObj.OBJHELDWEAPON.WEAPONIMAGEAMMO.Height;
                            AmmoRectangle.StrokeThickness = 0;
                            Canvas.SetLeft(AmmoRectangle, ammo.X);
                            Canvas.SetTop(AmmoRectangle, ammo.Y);
                            AmmoRectangle.Fill = new ImageBrush(ammo.AMMOIMAGE);
                            Game.Children.Add(AmmoRectangle);
                        }
                        else
                        {
                            CurObj.OBJHELDWEAPON.WEAPONAMMOLIST.RemoveAt(j);
                        }
                    }
                }
            }
        }


        // Engine Version 2.21.1288.2 [Physics code now on its own thread] 
        public void RunPhysics()
        {
            BackgroundWorker PhysThread = new BackgroundWorker();
            PhysThread.DoWork += PhysicsDoWork;
            PhysThread.RunWorkerCompleted += PhysicsDone;

            PhysThread.RunWorkerAsync(currentlevel.LevelObjects);
        }
    }
}
