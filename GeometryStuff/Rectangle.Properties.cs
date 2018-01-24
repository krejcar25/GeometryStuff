using System;

namespace GeometryStuff
{
    public partial class Rectangle
    {
        /// <summary>
        /// Top left corner point
        /// </summary>
        public Coords TopLeft { get; set; }
        /// <summary>
        /// Bottom right corner point
        /// </summary>
        public Coords BottomRight { get; set; }
        /// <summary>
        /// Top right corner point
        /// </summary>
        public Coords TopRight
        {
            get => new Coords(BottomRight.X, TopLeft.Y);
            private set
            {
                BottomRight.X = value.X;
                TopLeft.Y = value.Y;
            }
        }
        /// <summary>
        /// Bottom left corner point
        /// </summary>
        public Coords BottomLeft
        {
            get => new Coords(TopLeft.X, BottomRight.Y);
            private set
            {
                TopLeft.X = value.X;
                BottomRight.Y = value.Y;
            }
        }
        /// <summary>
        /// Size of this rectangle along the X axis
        /// </summary>
        public double DimensionX { get => Math.Abs(TopLeft.X - TopRight.X); }
        /// <summary>
        /// Size of this rectangle along the Y axis
        /// </summary>
        public double DimensionY { get => Math.Abs(TopLeft.Y - TopRight.Y); }
        /// <summary>
        /// The area of this rectangle
        /// </summary>
        public double Area { get => DimensionX * DimensionY; }

        /// <summary>
        /// Corners of the rectangle
        /// </summary>
        public enum Corners { TopLeft, BottomRight, TopRight, BottomLeft };
    }
}
