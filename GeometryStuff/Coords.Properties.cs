namespace GeometryStuff
{
    public partial class Coords
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z coordinate
        /// </summary>
        public double Z { get; private set; }
        /// <summary>
        /// 2D or 3D
        /// </summary>
        public SpaceType Space { get; set; }

        public enum SpaceType { TwoDim, ThreeDim };
    }
}
