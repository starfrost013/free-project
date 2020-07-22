using Emerald.Core;
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
    public partial class FreeSDL // Potentially have a renderer class
    {
        public void DrawScene() // draws the scene, starting with the lowest priority IGameObjects and then going up to the highest priority ones, then draws any text IGameObjects and the debug display if it is on. 
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
                        for (int i = 0; i < currentlevel.LevelIGameObjects.Count; i++)
                        {
                            IGameObject CurGameObject = currentlevel.LevelIGameObjects[i];

                            if (CurGameObject.Position.X - Scrollbar.HorizontalOffset > 0 - CurGameObject.GameObjectIMAGE.PixelWidth 
                                && CurGameObject.Position.X - Scrollbar.HorizontalOffset < this.Width + CurGameObject.GameObjectIMAGE.PixelWidth 
                                && CurGameObject.Position.Y - Scrollbar.VerticalOffset > 0 - CurGameObject.GameObjectIMAGE.PixelHeight 
                                && CurGameObject.Position.Y - Scrollbar.VerticalOffset < this.Height + CurGameObject.GameObjectIMAGE.PixelHeight 
                                || IsSentientBeing(CurGameObject))
                            { // optimization (bld 1037-41) 
                                if (CurGameObject.GameObjectPRIORITY == currentPriority)
                                {
                                    if (CurGameObject.GameObjectPLAYER && Gamestate == GameState.Game)
                                    {
                                        Scrollbar.ScrollToHorizontalOffset(CurGameObject.Position.X - this.Width / 6); // level scrolling
                                        Scrollbar.ScrollToVerticalOffset(CurGameObject.Position.Y - this.Width / 6); // vertical level scrolling
                                    }

                                    Rectangle rectangle = new Rectangle();
                                    rectangle.Height = CurGameObject.GameObjectIMAGE.Height;
                                    rectangle.Width = CurGameObject.GameObjectIMAGE.Width;
                                    rectangle.StrokeThickness = 0;
                                    HandleAnimations(CurGameObject);

                                    if (Gamestate == GameState.Game & CurGameObject.GameObjectGRAV)
                                    {
                                        if (!Settings.FeatureControl_UsePhysicsV2)
                                        {
                                            HandlePhys(CurGameObject);
                                        }
                                        else
                                        {
                                            RunPhysicsV2(CurGameObject); 
                                        }
                                        

                                        // Just a test. Onto the physics thread this goes
                                        if (CurGameObject.GameObjectCANCOLLIDE != false)
                                        { // only handle collisions if cancollide is true or null

                                            if (!Settings.FeatureControl_UseCollisionV2)
                                            {
                                                HandleCollision(CurGameObject);
                                            }
                                            else
                                            {
                                                HandleCollisionV2(CurGameObject);
                                            }
                                            
                                        }
                                    }

                                    HandleAI(CurGameObject);
                                    Canvas.SetLeft(rectangle, CurGameObject.Position.X);
                                    Canvas.SetTop(rectangle, CurGameObject.Position.Y);
                                    rectangle.Fill = new ImageBrush(CurGameObject.GameObjectIMAGE);
                                    Game.Children.Add(rectangle);

                                    RenderWeapons(CurGameObject); 
                                }
                            }
                        }

                        if (currentPriority == Priority.High && !Settings.FeatureControl_DisableGTDebug)
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

                        currentPriority++; // increment the IGameObject priority we are currently drawing. this allows us to put IGameObjects in front of each other etc (currently 5 layers)
                    }
                }
            }

            //draw text

            UpdateLayout();
        }

        public void RenderWeapons(IGameObject CurGameObject)
        {
            if (CurGameObject.GameObjectHELDWEAPON != null) // weapon drawing
            {
                Rectangle WeaponRectangle = new Rectangle();
                WeaponRectangle.Height = CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGE.Height;
                WeaponRectangle.Width = CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGE.Width;
                WeaponRectangle.StrokeThickness = 0;
                CurGameObject.GameObjectHELDWEAPON.WEAPONPOSITIONX = CurGameObject.Position.X + CurGameObject.GameObjectIMAGE.PixelWidth / 1.33;
                CurGameObject.GameObjectHELDWEAPON.WEAPONPOSITIONY = CurGameObject.Position.Y + CurGameObject.GameObjectIMAGE.PixelHeight / 2;
                Canvas.SetLeft(WeaponRectangle, CurGameObject.GameObjectHELDWEAPON.WEAPONPOSITIONX);
                Canvas.SetTop(WeaponRectangle, CurGameObject.GameObjectHELDWEAPON.WEAPONPOSITIONY);
                WeaponRectangle.Fill = new ImageBrush(CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGE); // the image.
                Game.Children.Add(WeaponRectangle);

                if (CurGameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.Count > 0) // ammo
                {
                    for (int j = 0; j < CurGameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.Count; j++)
                    {
                        Ammo ammo = CurGameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST[j];
                        if (ammo.X - Scrollbar.HorizontalOffset > 0 - CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.PixelWidth && ammo.X - Scrollbar.HorizontalOffset < this.Width + CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.PixelWidth && ammo.Y - Scrollbar.VerticalOffset > 0 - CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.PixelHeight & ammo.Y - Scrollbar.VerticalOffset < this.Width + CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.PixelHeight)
                        {
                            Rectangle AmmoRectangle = new Rectangle();
                            AmmoRectangle.Width = CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.Width;
                            AmmoRectangle.Height = CurGameObject.GameObjectHELDWEAPON.WEAPONIMAGEAMMO.Height;
                            AmmoRectangle.StrokeThickness = 0;
                            Canvas.SetLeft(AmmoRectangle, ammo.X);
                            Canvas.SetTop(AmmoRectangle, ammo.Y);
                            AmmoRectangle.Fill = new ImageBrush(ammo.AMMOIMAGE);
                            Game.Children.Add(AmmoRectangle);
                        }
                        else
                        {
                            CurGameObject.GameObjectHELDWEAPON.WEAPONAMMOLIST.RemoveAt(j);
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

            PhysThread.RunWorkerAsync(currentlevel.LevelIGameObjects);
        }
    }
}
