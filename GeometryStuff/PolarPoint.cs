using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public partial class PolarPoint : Animatable
    {
        public PolarPoint(double radius, Angle angle)
        {
            Radius = radius;
            Angle = angle;
        }

        public double Radius { get; set; }
        public Angle Angle { get; set; }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        public static explicit operator PolarPoint(CartesianPoint cartesianPoint)
        {
            return new PolarPoint(Math.Sqrt(Math.Pow(cartesianPoint.X, 2) + Math.Pow(cartesianPoint.Y, 2)), Angle.Atan2(cartesianPoint));
        }
    }
}
