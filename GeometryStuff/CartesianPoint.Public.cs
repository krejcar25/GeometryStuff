using System.Collections.Generic;
using System.Windows;

namespace GeometryStuff
{
    public partial class CartesianPoint : Coords
    {
        public CartesianPoint(IEnumerable<Coords> list) : base(list)
        {

        }

        public CartesianPoint(double x, double y) : base(x, y)
        {

        }

        public CartesianPoint(double x, double y, double z) : base(x, y, z)
        {

        }

        protected override Freezable CreateInstanceCore()
        {
            return new CartesianPoint(0, 0);
        }

        /// <summary>Converts this Coordinates to System.Windows.Point</summary>
        /// <exception cref="ArgumentException">Thrown when this Coords' space type is different from the supplied one</exception>
        /// <returns>New System.Windows.Point</returns>
        public static implicit operator Point(CartesianPoint coords)
        {
            coords.CheckSpace(SpaceType.TwoDim, "System.Windows.Point may only be created from a 2D coords", "coords");
            return new Point(coords.X, coords.Y);
        }

        public static CartesianPoint operator +(CartesianPoint point, Vector vector)
        {
            vector.CheckSpace(point, string.Format("Vector.MovePoint requires point to be same dimensions as this Vector. This vector is {0} and supplied point is {1}", vector.Space.ToString(), point.Space.ToString()));
            CartesianPoint ret = new CartesianPoint(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
            ret.To2D(vector.Space);
            return ret;
        }

        public static CartesianPoint operator -(CartesianPoint point, Vector vector)
        {
            vector.CheckSpace(point, string.Format("Vector.MovePoint requires point to be same dimensions as this Vector. This vector is {0} and supplied point is {1}", vector.Space.ToString(), point.Space.ToString()));
            CartesianPoint ret = new CartesianPoint(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
            ret.To2D(vector.Space);
            return ret;
        }

        public static Vector operator -(CartesianPoint end, CartesianPoint start) => new Vector(start, end);

        public static explicit operator CartesianPoint(PolarPoint polarPoint)
        {
            return new CartesianPoint(polarPoint.Radius * polarPoint.Angle.Cos, polarPoint.Radius * polarPoint.Angle.Sin);
        }
    }
}
