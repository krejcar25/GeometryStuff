using System;

namespace GeometryStuff
{
    public partial class Rectangle
    {
        private void Initialise(CartesianPoint point1, CartesianPoint point2, Corners corner)
        {
            switch (corner)
            {
                case Corners.TopLeft:
                    TopLeft = point1;
                    BottomRight = point2;
                    break;
                case Corners.BottomRight:
                    TopLeft = point2;
                    BottomRight = point1;
                    break;
                case Corners.TopRight:
                    TopLeft = new CartesianPoint(point2.X, point1.Y);
                    BottomRight = new CartesianPoint(point1.X, point2.Y);
                    break;
                case Corners.BottomLeft:
                    TopLeft = new CartesianPoint(point1.X, point2.Y);
                    BottomRight = new CartesianPoint(point2.X, point1.Y);
                    break;
                default:
                    throw new ArgumentException("Invalid parameter value", "corner");
            }
        }
    }
}
