using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public partial class Angle : Animatable
    {
        /// <summary>
        /// Initialises a new Angle
        /// </summary>
        /// <param name="radians">Angle size in Radians</param>
        public Angle(double radians)
        {
            Rad = radians;
        }

        /// <summary>
        /// Initialises a new Angle
        /// </summary>
        /// <param name="unit">Angle unit</param>
        /// <param name="value">Angle size</param>
        public Angle(AngleUnit unit, double value)
        {
            switch (unit)
            {
                case AngleUnit.Deg:
                    Deg = value;
                    break;
                case AngleUnit.Rad:
                    Rad = value;
                    break;
                case AngleUnit.Grad:
                    Grad = value;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter value", "unit");
            }
        }

        /// <summary>
        /// Adds an angle to this angle
        /// </summary>
        /// <param name="a">Angle to be added</param>
        public void Add(Angle a)
        {
            Rad += a.Rad;
        }

        /// <summary>
        /// Subtracts an angle from this angle
        /// </summary>
        /// <param name="a">Angle to be subtracted</param>
        public void Subtract(Angle a)
        {
            Rad -= a.Rad;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new Angle(0);
        }

        /// <summary>
        /// Creates new Angle from this radians double
        /// </summary>
        /// <param name="rad"></param>
        public static implicit operator Angle(double rad)
        {
            return new Angle(rad);
        }

        public static double RadToDeg(double rad)
        {
            return new Angle(rad).Deg;
        }

        public static double DegToRad(double deg)
        {
            return new Angle(AngleUnit.Deg, deg).Rad;
        }

        public static Angle Atan2(double x, double y)
        {
            if (x > 0) return new Angle(Math.Atan(y / x));
            else if (x < 0 && y >= 0) return new Angle(Math.Atan(y - x) + Math.PI);
            else if (x < 0 && y < 0) return new Angle(Math.Atan(y / x) - Math.PI);
            else if (x == 0 && y > 0) return new Angle(Math.PI / 2);
            else if (x == 0 && y < 0) return new Angle(-Math.PI / 2);
            else if (x == 0 && y == 0) throw new ArgumentOutOfRangeException("X and Y cannot be zero at the same time");
            else throw new NotImplementedException();
        }

        public static Angle Atan2(Coords coords)
        {
            coords.CheckSpace(Coords.SpaceType.TwoDim, "Vector can be created only from 2D coords", "coords");
            return Atan2(coords.X, coords.Y);
        }
    }
}
