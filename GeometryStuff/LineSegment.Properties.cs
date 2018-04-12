using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public partial class LineSegment : Animatable
    {


        public CartesianPoint PointA
        {
            get { return (CartesianPoint)GetValue(PointAProperty); }
            set { SetValue(PointAProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointA.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointAProperty =
            DependencyProperty.Register("PointA", typeof(CartesianPoint), typeof(LineSegment), new PropertyMetadata(new CartesianPoint(0, 0)));



        public CartesianPoint PointB
        {
            get { return (CartesianPoint)GetValue(PointBProperty); }
            set { SetValue(PointBProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointB.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointBProperty =
            DependencyProperty.Register("PointB", typeof(CartesianPoint), typeof(LineSegment), new PropertyMetadata(new CartesianPoint(0,0)));

        public Vector Vector => PointB - PointA;

        public Line Line => new Line(PointA, Vector);

        public CartesianPoint Midpoint
        {
            get
            {
                CartesianPoint point = new CartesianPoint((PointA.X + PointB.X) / 2, (PointA.Y + PointB.Y) / 2, (PointA.Z + PointB.Z) / 2);
                point.To2D(PointA.Space);
                return point;
            }
        }

    }

    public static class LineSegmentExtensionMethods
    {
        public static System.Windows.Shapes.Polygon Polygon(this List<LineSegment> list)
        {
            System.Windows.Shapes.Polygon polygon = new System.Windows.Shapes.Polygon();
            if (list[0].PointA == list[list.Count - 1].PointB)
            {
                polygon.Points.Add(list[0].PointA);
                for (int i=1; i < list.Count; i++)
                {
                    if (list[i - 1].PointB == list[i].PointA) polygon.Points.Add(list[i].PointA);
                    else return null;
                }
                return polygon;
            }
            return null;
        }
    }
}
