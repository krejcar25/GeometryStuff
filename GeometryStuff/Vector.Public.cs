using System;

namespace GeometryStuff
{
    public partial class Vector : Coords
    {
        /// <summary>
        /// Initialises a new 2D vector
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector(double x, double y) : base(x, y)
        {

        }

        /// <summary>
        /// Initialises a new 3D vector
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Vector(double x, double y, double z) : base(x, y, z)
        {

        }

        
        /// <summary>
        /// Initialises a new vector and sets it to 2D or 3D based on point inputs
        /// </summary>
        /// <param name="point1">Origin point of vector</param>
        /// <param name="point2">End point of vector</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when one supplied point is 2D and the other 3D</exception>
        public Vector(Coords point1, Coords point2) : base(point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z)
        {
            point1.CheckSpace(point2, string.Format("Vector constructor requires both points to be same dimensions. point1 is {0} and point2 is {1}", point1.Space.ToString(), point2.Space.ToString()));
            To2D(point1.Space);
        }

        /// <summary>
        /// Moves the specified point by this vector
        /// </summary>
        /// <param name="point">The point to be moved</param>
        /// <returns>The point after moving</returns>
        public Coords MovePoint(Coords point)
        {
            CheckSpace(point, string.Format("Vector.MovePoint requires point to be same dimensions as this Vector. This vector is {0} and supplied point is {1}", Space.ToString(), point.Space.ToString()));
            Coords ret = new Coords(point.X + X, point.Y + Y, point.Z + Z);
            ret.To2D(Space);
            return ret;
        }

        /// <summary>
        /// Generates vector that has same angle and size but goes opposite way
        /// </summary>
        /// <returns>Inverted vector</returns>
        public Vector GetOppositeVector()
        {
            Vector ret = new Vector(-X, -Y, -Z);
            ret.To2D(Space);
            return ret;
        }

        /// <summary>
        /// Generates a Tuple of two vectors perpendicular to this vector
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when this is a 3D vector</exception>
        /// <returns>Tuple of vectors</returns>
        public Tuple<Vector, Vector> GetPerpendicularVectorsPair()
        {
            if (Space == SpaceType.ThreeDim) throw new ArgumentException("Perpendicular vectors can be calculated only for 2D vectors");
            Vector perp = new Vector(Y, -X);
            return new Tuple<Vector, Vector>(perp, perp.GetOppositeVector());
        }

        /// <summary>
        /// Adds a vector to this Vector
        /// </summary>
        /// <param name="add">Vector to add to this one</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when the supplied vector is 2D and this one is 3D or vice versa</exception>
        /// <returns>New Vector</returns>
        public Vector AddVectors(Vector add)
        {
            CheckSpace(add, string.Format("Vector.AddVectors requires both vectors to be same dimensions. This vector is {0} and the supplied vector is {1}", Space.ToString(), add.Space.ToString()));
            Vector ret = new Vector(X + add.X, Y + add.Y, Z + add.Z);
            ret.To2D(Space);
            return ret;
        }

        /// <summary>
        /// Adds a vector specified by two coordinates to this vector
        /// </summary>
        /// <param name="x">X coordinate of the vector to be added to this Vector</param>
        /// <param name="y">Y coordinate of the vector to be added to this Vector</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when the supplied vector is 2D and this one is 3D or vice versa</exception>
        /// <returns>New Vector</returns>
        public Vector AddVectors(double x, double y)
        {
            return AddVectors(new Vector(x, y));
        }

        /// <summary>
        /// Adds a vector specified by three coordinates to this vector
        /// </summary>
        /// <param name="x">X coordinate of the vector to be added to this Vector</param>
        /// <param name="y">Y coordinate of the vector to be added to this Vector</param>
        /// <param name="z">Z coordinate of the vector to be added to this Vector</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when the supplied vector is 2D and this one is 3D or vice versa</exception>
        /// <returns>New Vector</returns>
        public Vector AddVectors(double x, double y, double z)
        {
            return AddVectors(new Vector(x, y, z));
        }

        /// <summary>
        /// Calculates the dot product of this vector and the vector specified
        /// </summary>
        /// <param name="multiplier">The other vector</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when the supplied vector is 2D and this one is 3D or vice versa</exception>
        /// <returns>Double dot product</returns>
        public double DotProduct(Vector multiplier)
        {
            CheckSpace(multiplier, string.Format("Vector.DotProduct requires both vectors to be same dimensions. This vector is {0} and the supplied vector is {1}", Space.ToString(), multiplier.Space.ToString()));
            return X * multiplier.X + Y * multiplier.Y + Z * multiplier.Z;
        }

        /// <summary>
        /// Calculates the angle between the two vectors
        /// </summary>
        /// <param name="otherVector">The Vector with which this vector has angle</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when the supplied vector is 2D and this one is 3D or vice versa</exception>
        /// <returns>New Angle</returns>
        public Angle VectorAngle(Vector otherVector)
        {
            CheckSpace(otherVector, string.Format("Vector.VectorAngle requires both vectors to be same dimensions. This vector is {0} and the supplied vector is {1}", Space.ToString(), otherVector.Space.ToString()));
            return new Angle(Angle.AngleUnit.Rad, Math.Acos(Math.Abs(DotProduct(otherVector)) / (Length * otherVector.Length)));
        }

        /// <summary>
        /// Returns the coordinates as (X;Y) or (X;Y;Z)
        /// </summary>
        /// <returns>String description of this Vector</returns>
        public override string ToString()
        {
            switch (Space)
            {
                case SpaceType.TwoDim:
                    return string.Format("({0};{1})", X, Y);
                case SpaceType.ThreeDim:
                    return string.Format("({0};{1};{2})", X, Y, Z);
                default:
                    return "Incorrectly initialised Vector";
            }
        }
    }
}
