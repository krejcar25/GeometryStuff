using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryStuff
{
    public class SpaceTypeNotSameException : Exception
    {
        public Coords Coords1 { get; set; }
        public Coords Coords2 { get; set; }

        public SpaceTypeNotSameException(Coords coords1, Coords coords2)
        {
            Coords1 = coords1;
            Coords2 = coords2;
        }

        public SpaceTypeNotSameException(Coords coords1, Coords coords2, string message) : base(message)
        {
            Coords1 = coords1;
            Coords2 = coords2;
        }

        public SpaceTypeNotSameException(Coords coords1, Coords coords2, string message, Exception inner) : base(message, inner)
        {
            Coords1 = coords1;
            Coords2 = coords2;
        }
    }
}
