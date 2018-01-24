using System;

namespace GeometryStuff
{
    public partial class Vector : Coords 
    {
        /// <summary>
        /// Length of the vector
        /// </summary>
        public double Length { get => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); }

        #region Static properties
        /// <summary>
        /// A vector with coordinates (0;0)
        /// </summary>
        public static Vector Null { get => new Vector(0, 0); }
        /// <summary>
        /// A vector with coordinates (1;1)
        /// </summary>
        public static Vector Unit { get => new Vector(1, 1); }
        #endregion
    }
}
