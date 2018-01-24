namespace GeometryStuff
{
    public partial class Coords
    {
        /// <summary>
        /// Initialises a new instance of the Coords class
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Coords(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", X, Y);
        }
    }
}
