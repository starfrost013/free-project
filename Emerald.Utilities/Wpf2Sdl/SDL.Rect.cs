using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Emerald.Utilities/Wpf2Sdl/SDL.Rect.cs 
/// 
/// Created: 2020-06-21
/// 
/// Modified: 2020-06-22
/// 
/// Version: 1.50
/// 
/// Purpose: Reimplementation of the Windows Presentation Foundation (WPF) Rect class.
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
            FromPoints(P1, P2); 
        }

        private void FromPoints(SDLPoint P1, SDLPoint P2)
        {
            TopLeft = P1;
            Left = P1.X;
            Top = P1.Y;

            BottomRight = P2;
            Right = P2.X;
            Bottom = P2.Y;

            Width = P2.X - P1.X;
            Height = P1.Y - P1.Y;

            if (Width < 0 || Height < 0)
            {
                // temp code
                Debug.WriteLine("Width/Height less than 0!");

                // VERY TEMPORARY
                Environment.Exit(0x0010DEAD);
            }
        }
    }
}
