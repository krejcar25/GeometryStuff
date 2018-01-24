namespace GeometryStuff
{
    public partial class Rectangle
    {
        private void Initialise(Coords point1, Coords point2, Corners corner)
        {
            switch (corner)
            {
                case Corners.TopLeft:
                    TopLeft = point1;
                    BottomRight = point2;
                    break;
                case Corners.BottomRight:
                    TopLeft = point2;
                    BottomRight = point1;
                    break;
                case Corners.TopRight:
                    TopLeft = new Coords(point2.X, point1.Y);
                    BottomRight = new Coords(point1.X, point2.Y);
                    break;
                case Corners.BottomLeft:
                    TopLeft = new Coords(point1.X, point2.Y);
                    BottomRight = new Coords(point2.X, point1.Y);
                    break;
                default:
                    break;
            }
        }
    }
}
