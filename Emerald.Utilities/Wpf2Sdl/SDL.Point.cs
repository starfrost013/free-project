using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
///
/// Emerald.Utilities/Wpf2Sdl/SDLPoint.cs
/// 
/// Created: 2020-06-16
/// 
/// Modified: 2020-06-16
/// 
/// Version: 1.00
/// 
/// Purpose: WPF compatibility services for SDL-based free! engine applications [enginecore-2.20.1379.56 v1.00]. Acts precisely like the WPF Point class to all other applications, just with a different name - also being a class
/// rather than a struct. It also only implements functionality used by free! at this time. 
/// 
/// </summary>

namespace Emerald.Utilities.Wpf2Sdl
{
    public class SDLPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public SDLPoint(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Offset(double OffsetX, double OffsetY)
        {
            X += OffsetX;
            Y += OffsetY;
        }

        // Custom equals implementation.
        public static bool operator ==(SDLPoint Point1, SDLPoint Point2)
        {
            return ((Point1.X == Point2.X) && (Point1.Y == Point2.Y));
        }

        // Custom inequality implementation.
        public static bool operator !=(SDLPoint Point1, SDLPoint Point2) 
        {
            return ((Point1.X != Point2.X) || (Point1.Y != Point2.Y)); 
        }

        // Custom addition implementation.
        public static SDLPoint operator +(SDLPoint Point1, SDLPoint Point2)
        {
            return new SDLPoint(Point1.X + Point2.X, Point1.Y + Point2.Y);
        }

        public static SDLPoint operator -(SDLPoint Point1, SDLPoint Point2)
        {
            return new SDLPoint(Point1.X - Point2.X, Point1.Y - Point2.Y);
        }

        public static SDLPoint operator *(SDLPoint Point1, SDLPoint Point2)
        {
            return new SDLPoint(Point1.X * Point2.X, Point1.Y * Point2.Y);
        }

        public static SDLPoint operator /(SDLPoint Point1, SDLPoint Point2)
        {
            return new SDLPoint(Point1.X / Point2.X, Point1.Y / Point2.Y);
        }
        public override bool Equals(IGameObject GameObject)
        {
            return base.Equals(GameObject);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
