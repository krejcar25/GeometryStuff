using System;
using System.Windows;

namespace GeometryStuff
{
    public partial class Line : Coords
    {
        public Line(double xIntersection, double yIntersection) : base(xIntersection, 0)
        {
            if (xIntersection == 0 && yIntersection == 0) throw new ArgumentException("Ambiguous line going through origin");
            Vector = new CartesianPoint(0, yIntersection) - (CartesianPoint)this;
        }

        public Line(CartesianPoint point1, CartesianPoint point2) : base(point1.X, point1.Y, point1.Z)
        {
            point1.CheckSpace(point2, "Both points must be the same space type");
            To2D(point1);
            Vector = point2 - point1;
        }

        public Line(CartesianPoint point, Vector vector) : base(point.X, point.Y, point.Z)
        {
            point.CheckSpace(vector, "Point and Vector must be same space type");
            To2D(point);
            Vector = vector;
        }

        protected override Freezable CreateInstanceCore() => new Line(1, 1);

        public CartesianPoint Intersects(Line line)
        {
            double coefficient = (line.Vector.Y * (line.X - X) + line.Vector.X * (Y - line.Y)) / (Vector.X * line.Vector.Y - Vector.Y * line.Vector.X);
            Console.WriteLine(coefficient);
            CartesianPoint point = new CartesianPoint(X + Vector.X * coefficient, Y + Vector.Y * coefficient, Z + Vector.Z * coefficient);
            point.To2D(Space);
            return point;
        }

        public bool PointBelongsToLine(CartesianPoint point)
        {
            double coefficient = (point.X - X) / Vector.X;
            return (point.Y == Y + Vector.Y * coefficient) && (point.Z == Z + Vector.Z * coefficient || Space == SpaceType.TwoDim);
        }

        public override string ToString()
        {
            switch (Space)
            {
                case SpaceType.TwoDim:
                    return string.Format(
                        "x={0}+{1}t,\r\n" +
                        "y={2}+{3}t, t∈R", 
                        X, Vector.X, Y, Vector.Y);
                case SpaceType.ThreeDim:
                    return string.Format(
                        "x={0}+{1}t,\r\n" +
                        "y={2}+{3}t,\r\n" +
                        "z={4}+{5}t, t∈R", 
                        X, Vector.X, Y, Vector.Y, Z, Vector.Z);
                default:
                    return "Nope! Bad!";
            }
        }

        public static explicit operator CartesianPoint(Line line)
        {
            CartesianPoint point = new CartesianPoint(line.X, line.Y, line.Z);
            point.To2D(line.Space);
            return point;
        }

        public static explicit operator Vector(Line line) => line.Vector;
    }

    public static class BoolExtensions
    {
        public static bool AnyFromMany(int count, params bool[] conditions)
        {
            int _true = 0;
            foreach (bool cond in conditions)
            {
                if (cond) _true++;
            }
            return _true >= count;
        }
    }
}
