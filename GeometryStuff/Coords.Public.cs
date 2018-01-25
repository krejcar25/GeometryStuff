using System;

namespace GeometryStuff
{
    public partial class Coords
    {
        /// <summary>
        /// Initialises a new 2D point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Coords(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
            Space = SpaceType.TwoDim;
        }

        /// <summary>
        /// Initialises a new 3D point
        /// </summary>
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

        protected internal void To2D(SpaceType original)
        {
            if (original == SpaceType.TwoDim)
            {
                Z = 0;
                Space = SpaceType.TwoDim;
            }
        }

        protected internal void CheckSpace(Coords item2, string message)
        {
            if (Space != item2.Space) throw new SpaceTypeNotSameException(this, item2, message);
        }

        protected internal void CheckSpace(SpaceType desired, string message, string argumentName)
        {
            if (Space != desired) throw new ArgumentException(message, argumentName);
        }

        /// <summary>
        /// Returns the coordinates [X;Y] or [X;Y;Z]
        /// </summary>
        /// <returns>String description of this Coords</returns>
        public override string ToString()
        {
            string ret = (Space == SpaceType.TwoDim) ? string.Format("[{0};{1}]", X, Y) : string.Format("[{0};{1};{2}]", X, Y, Z);
            return ret;
        }
    }
}
