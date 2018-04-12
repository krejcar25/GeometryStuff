using System.Windows;
using System.Windows.Media.Animation;
using Shapes = System.Windows.Shapes;

namespace GeometryStuff
{
    public partial class LineSegment : Animatable
    {
        public LineSegment(CartesianPoint pointA, CartesianPoint pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }

        public LineSegment(CartesianPoint pointA, Vector vector)
        {
            PointA = pointA;
            PointB = pointA + vector;
        }

        public Shapes.Line GetLine(Shapes.Line line)
        {
            line.X1 = PointA.X;
            line.Y1 = PointA.Y;
            line.X2 = PointB.X;
            line.Y2 = PointB.Y;
            return line;
        }

        public void GetLine(ref System.Windows.Controls.Canvas canvas, Shapes.Line line)
        {
            canvas.Children.Add(GetLine(line));
        }

        protected override Freezable CreateInstanceCore() => new LineSegment(new CartesianPoint(0, 0), new CartesianPoint(0, 0));

        public CartesianPoint Intersects(LineSegment segment)
        {
            CartesianPoint intersect = Line.Intersects(segment.Line);
            return (PointBelongsToLineSegment(intersect) && segment.PointBelongsToLineSegment(intersect)) ? intersect : null;
        }

        public CartesianPoint Intersects(Line line)
        {
            CartesianPoint intersect = Line.Intersects(line);
            return (PointBelongsToLineSegment(intersect) && line.PointBelongsToLine(intersect)) ? intersect : null;
        }

        public bool PointBelongsToLineSegment(CartesianPoint point)
        {
            bool line = Line.PointBelongsToLine(point);
            bool x = (PointA.X < PointB.X) ? 
                PointA.X < point.X && point.X < PointB.X : 
                PointB.X < point.X && point.X < PointA.X;
            bool y = (PointA.Y < PointB.Y) ? 
                PointA.Y < point.Y && point.Y < PointB.Y : 
                PointB.Y < point.Y && point.Y < PointA.Y;
            bool z = (PointA.Space == Coords.SpaceType.ThreeDim) ?
                (PointA.Z < PointB.Z) ?
                    PointA.Z < point.Z && point.Z < PointB.Z :
                    PointB.Z < point.Z && point.Z < PointA.Z :
                true;
            return line && x && y && z;
        }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", PointA, PointB);
        }
    }
}
