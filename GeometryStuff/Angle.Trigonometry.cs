using System;

namespace GeometryStuff
{
    public partial class Angle
    {
        /// <summary>
        /// Returns the Sine value of this angle
        /// </summary>
        /// <returns>Double Sine value</returns>
        public double Sin
        {
            get => Math.Sin(Rad);
        }

        /// <summary>
        /// Returns the Cosine value of this angle
        /// </summary>
        /// <returns>Double Cosine value</returns>
        public double Cos => Math.Cos(Rad);

        /// <summary>
        /// Returns the Tangens value of this angle
        /// </summary>
        /// <returns>Double Tangens value</returns>
        public double Tan => Math.Tan(Rad);

        /// <summary>
        /// Returns the Cosecant value of this angle
        /// </summary>
        /// <returns>Double Cosecant value</returns>
        public double Csc => 1 / Math.Sin(Rad);

        /// <summary>
        /// Returns the Secant value of this angle
        /// </summary>
        /// <returns>Double Secant value</returns>
        public double Sec => 1 / Math.Cos(Rad);

        /// <summary>
        /// Returns the Cotangens value of this angle
        /// </summary>
        /// <returns>Double Cotangens value</returns>
        public double Cot => 1 / Math.Tan(Rad);
    }
}
