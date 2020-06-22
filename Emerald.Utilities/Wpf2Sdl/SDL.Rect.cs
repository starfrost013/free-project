using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// Emerald.Utilities/Wpf2Sdl/SDL.Rect.cs 
/// 
/// Created: 2020-06-21
/// 
/// Modified: 2020-06-21
/// 
/// Version: 1.00
/// 
/// Purpose: Reimplementation of the Windows Presentation Foundation (WPF) 
/// 
/// </summary>
namespace Emerald.Utilities.Wpf2Sdl
{
    public class SDLRect
    {
        public double Top { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        public SDLPoint TopLeft { get; set; }
        public SDLPoint TopRight { get; set; }
        public SDLPoint BottomLeft { get; set; }
        public SDLPoint BottomRight { get; set; }
        public SDLPoint Location { get => TopLeft; set => TopLeft = value; }
        public double Width
        {
            get
            {
                return Width;
            }
            set
            {
                OnWidthSet(Width); 
                Width = value;
            }
        }

        public double Height
        {
            get
            {
                return Height;
            }
            set
            {
                OnHeightSet(Height);
                Height = value;
            }
        }

        public double X
        {
            get
            {
                return Left;
            }
            set
            {
                Left = value;
                TopLeft = new SDLPoint(Left, Top);
                BottomLeft = new SDLPoint(Left, Bottom);
            }
        }

        private void OnWidthSet(double X)
        {
            Right = Left + X;
            TopRight = new SDLPoint(Left + X, Top);
            BottomRight = new SDLPoint(Left + X, Bottom); 
        }

        private void OnHeightSet(double Y)
        {
            Bottom = Left + Y;
            TopRight = new SDLPoint(Left, Top);
            BottomRight = new SDLPoint(Left, Bottom);
        }

        public SDLRect(SDLPoint P1, SDLPoint P2)
        {
            TopLeft = P1;
            BottomRight = P2;
                
        }
    }
}
