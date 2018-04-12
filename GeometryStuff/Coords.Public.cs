using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    /// <summary>A set of coordinates class</summary>
    public abstract partial class Coords : Animatable
    {
        /// <summary>Initialises a new 2D point</summary>
        /// <param name = "x" > X coordinate</param>
        /// <param name = "y" > Y coordinate</param>
        public Coords(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
            Space = SpaceType.TwoDim;
        }

        /// <summary>Initialises a new 3D point</summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Coords(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Space = SpaceType.ThreeDim;
        }

        public Coords(IEnumerable<Coords> list)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            int count = 0;

            foreach (Coords coords in list)
            {
                count++;
                x += coords.X;
                y += coords.Y;
                z += coords.Z;
            }

            X = x / count;
            Y = y / count;
            Z = z / count;
        }

        /// <summary>If original is 2D, it will change current object's properties to match 2D mode</summary>
        /// <param name="original">The SpaceType of the original object</param>
        protected internal void To2D(SpaceType original)
        {
            if (original == SpaceType.TwoDim)
            {
                Z = 0;
                Space = SpaceType.TwoDim;
            }
        }

        /// <summary>Checks if supplied Coords is same space as this Coords</summary>
        /// <param name="coords">Other Coords instance</param>
        /// <param name="message">Exception message</param>
        /// <exception cref="SpaceTypeNotSameException">Thrown when this Coords' space is different from the supplied one's</exception>
        protected internal void CheckSpace(Coords coords, string message)
        {
            if (Space != coords.Space) throw new SpaceTypeNotSameException(this, coords, message);
        }

        /// <summary>Checks if this Coords has space supplied and throws exception if false</summary>
        /// <param name="desired">The desired Space type</param>
        /// <param name="message">Exception message</param>
        /// <param name="argumentName">The name of the original erroring argument</param>
        /// <exception cref="ArgumentException">Thrown when this Coords' space type is different from the supplied one</exception>
        protected internal void CheckSpace(SpaceType desired, string message, string argumentName)
        {
            if (Space != desired) throw new ArgumentException(message, argumentName);
        }

        /// <summary>Returns the coordinates [X;Y] or [X;Y;Z]</summary>
        /// <returns>String description of this Coords</returns>
        public override string ToString()
        {
            string ret = (Space == SpaceType.TwoDim) ? string.Format("[{0};{1}]", X, Y) : string.Format("[{0};{1};{2}]", X, Y, Z);
            return ret;
        }

        public static implicit operator SpaceType(Coords coords) => coords.Space;
    }
}
