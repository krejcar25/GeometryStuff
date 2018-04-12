using System;
using System.Windows;

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
        public Vector(CartesianPoint point1, CartesianPoint point2) : base(point2.X - point1.X, point2.Y- point1.Y, point2.Z - point1.Z)
        {
            point1.CheckSpace(point2, string.Format("Vector constructor requires both points to be same dimensions. point1 is {0} and point2 is {1}", point1.Space.ToString(), point2.Space.ToString()));
            To2D(point1.Space);
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
            return new Tuple<Vector, Vector>(perp, -perp);
        }

        public Vector WithLength(double length)
        {
            return this * (length / Length);
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
            return this + new Vector(x, y);
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
            return this + new Vector(x, y, z);
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
            return X * multiplier.X + Y* multiplier.Y+ Z * multiplier.Z;
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

        public Arrow Arrow(CartesianPoint origin)
        {
            return new Arrow(origin, this);
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
        
        public static Vector WithAngle(double length, Angle angle)
        {
            CartesianPoint point = (CartesianPoint)new PolarPoint(length, angle);
            return new Vector(point.X, point.Y);
            /*
            Vector v;
            if (angle.Deg % 90 == 0)
            {
                if ((angle.Deg / 90) % 4 == 0)
                {
                    v = new Vector(1, 0);
                }
                else if ((angle.Deg / 90) % 2 == 0)
                {
                    v = new Vector(-1, 0);
                }
                else
                {
                    if ((Math.Abs(angle.Deg) - 270) % 360 == 0)
                    {
                        if (angle.Deg > 0)
                        {
                            v = new Vector(0, -1);
                        }
                        else
                        {
                            v = new Vector(0, 1);
                        }
                    }
                    else
                    {
                        if (angle.Deg > 0)
                        {
                            v = new Vector(0, 1);
                        }
                        else
                        {
                            v = new Vector(0, -1);
                        }
                    }
                }
            }
            else
            {
                int x = 1;
                int y = 1;
                if (angle.Cos< 0)
                {
                    x = -1;
                    y = -1;
                }
                v = new Vector(x, y * angle.Tan);
                
            }
            return v.WithLength(length);*/
        }

        public void Normalise()
        {
            X /= Length;
            Y /= Length;
            Z /= Length;
        }

        public static Vector WithAngle(double length, Angle angle1, Vector zeroAngle1, Angle angle2, Vector zeroAngle2)
        {
            throw new NotImplementedException();
        }

        protected override Freezable CreateInstanceCore() => new Vector(0, 0);

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            vector1.CheckSpace(vector2, string.Format("Vector.AddVectors requires both vectors to be same dimensions. This vector is {0} and the supplied vector is {1}", vector1.Space.ToString(), vector2.Space.ToString()));
            Vector ret = new Vector(vector1.X + vector2.X, vector1.Y+ vector2.Y, vector1.Z + vector2.Z);
            ret.To2D(vector1.Space);
            return ret;
        }

        public static Vector operator -(Vector vector1, Vector vector2) => vector1 + (-vector2);

        public static Vector operator -(Vector vector)
        {
            Vector ret = new Vector(-vector.X, -vector.Y, -vector.Z);
            ret.To2D(vector.Space);
            return ret;
        }

        public static Vector operator *(Vector vector, double coefficient)
        {
            Vector ret = new Vector(vector.X * coefficient, vector.Y* coefficient, vector.Z * coefficient);
            ret.To2D(vector.Space);
            return ret;
        }
    }
}
