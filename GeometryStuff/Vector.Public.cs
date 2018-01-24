using System;

namespace GeometryStuff
{
    public partial class Vector : Coords
    {
        /// <summary>
        /// Initialises a new instance of the Vector class
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector(double x, double y) : base(x, y)
        {

        }

        /// <summary>
        /// Moves the specified point by this vector
        /// </summary>
        /// <param name="point">The point to be moved</param>
        /// <returns>The point after moving</returns>
        public Coords MovePoint(Coords point)
        {
            return new Coords(point.X + X, point.Y + Y);
        }

        /// <summary>
        /// Generates vector that has same angle and size but goes opposite way
        /// </summary>
        /// <returns>Inverted vector</returns>
        public Vector GetOppositeVector()
        {
            return new Vector(-X, -Y);
        }

        /// <summary>
        /// Generates a Tuple of two vectors perpendicular to this vector
        /// </summary>
        /// <returns>Tuple of vectors</returns>
        public Tuple<Vector, Vector> GetPerpendicularVectorsPair()
        {
            Vector perp = new Vector(Y, -X);
            return new Tuple<Vector, Vector>(perp, perp.GetOppositeVector());
        }

        /// <summary>
        /// Generates new Vector that connects two points
        /// </summary>
        /// <param name="point1">Origin point of vector</param>
        /// <param name="point2">End point of vector</param>
        /// <returns>New Vector</returns>
        public static Vector FromPointPair(Coords point1, Coords point2)
        {
            return new Vector(point2.X - point1.X, point2.Y - point1.Y);
        }

        public override string ToString()
        {
            return string.Format("({0};{1})", X, Y);
        }
    }
}
