using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;

namespace GeometryStuff
{
    public partial class Rectangle
    {
        /// <summary>
        /// Initialises a new instance of the Rectangle class using two corner points
        /// </summary>
        /// <param name="point1">One corner point</param>
        /// <param name="point2">Corner point diagonally opposite to point1</param>
        /// <param name="corner">Which corner is point1</param>
        public Rectangle(Coords point1, Coords point2, Corners corner = Corners.TopLeft)
        {
            Debug.WriteLine(string.Format("Creating rectangle from {0} to {1}. First corner is {2}.", point1, point2, corner.ToString()));
            Initialise(point1, point2, corner);
        }

        /// <summary>
        /// Initialises a new instance of the Rectangle class using a corner point and a diagonal vector
        /// </summary>
        /// <param name="point1">One corner point</param>
        /// <param name="diagonal">The diagonal vector going from point1</param>
        /// <param name="corner">Which corner is point1</param>
        public Rectangle(Coords point1, Vector diagonal, Corners corner = Corners.TopLeft)
        {
            Coords point2 = diagonal.MovePoint(point1);
            Debug.WriteLine(string.Format("Creating rectangle from {0} to {1} using vector {2}. First corner is {3}.", point1, point2, diagonal, corner.ToString()));
            Initialise(point1, point2, corner);
        }

        /// <summary>
        /// Generates a Border WPF control from this Rectangle
        /// </summary>
        /// <param name="parentDimension">Dimension vector of the parent container going from top left to bottom right corner</param>
        /// <returns>A Border control</returns>
        public Border GenerateBorder(Vector parentDimension)
        {
            Border border = new Border();
            Vector dimension = Vector.FromPointPair(TopLeft, BottomRight);
            border.Margin = new Thickness(TopLeft.X, TopLeft.Y, parentDimension.X - (TopLeft.X + dimension.X), parentDimension.Y - (TopLeft.Y + dimension.Y));
            return border;
        }

        /// <summary>
        /// Resizes this Rectangle
        /// </summary>
        /// <param name="corner">Which corner to stretch</param>
        /// <param name="resize">The vector used to move the corner point</param>
        public void ResizeRectangle(Corners corner, Vector resize)
        {
            switch (corner)
            {
                case Corners.TopLeft:
                    TopLeft = resize.MovePoint(TopLeft);
                    break;
                case Corners.BottomRight:
                    BottomRight = resize.MovePoint(BottomRight);
                    break;
                case Corners.TopRight:
                    TopRight = resize.MovePoint(TopRight);
                    break;
                case Corners.BottomLeft:
                    BottomRight = resize.MovePoint(BottomRight);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Checks if point lies inside the rectangle
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>True if point lies inside</returns>
        public bool IsPointInside(Coords point)
        {
            Debug.Write(string.Format("    Checking if {0} is in rectangle {1}...", point, this));
            bool ret = ((TopLeft.X < point.X && point.X < BottomRight.X) && (TopLeft.Y < point.Y && point.Y < BottomRight.Y));
            Debug.WriteLine((ret) ? "It is." : "It isn't.");
            return ret;
        }

        /// <summary>
        /// Checks if two rectangles are overlapping
        /// </summary>
        /// <param name="rectangle">The other rectangle to compare to this one</param>
        /// <returns>True if the two rectangles overlap</returns>
        public bool RectanglesOverlap(Rectangle rectangle)
        {
            Debug.WriteLine(string.Format("Checking if rectangles {0} and {1} overlap:", this, rectangle));
            return IsPointInside(rectangle.TopLeft) || IsPointInside(rectangle.TopRight) || IsPointInside(rectangle.BottomLeft) || IsPointInside(rectangle.BottomRight);
        }

        public override string ToString()
        {
            return string.Format("[{0}:{1}]", TopLeft, BottomRight);
        }
    }
}
