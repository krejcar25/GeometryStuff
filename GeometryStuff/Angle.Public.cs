using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryStuff
{
    public partial class Angle
    {
        public Angle(AngleUnit unit, double value, AngleDirection direction = AngleDirection.Anticlockwise)
        {
            switch (unit)
            {
                case AngleUnit.Deg:
                    Deg = value * (int)direction;
                    break;
                case AngleUnit.Rad:
                    Rad = value * (int)direction;
                    break;
                case AngleUnit.Grad:
                    Grad = value * (int)direction;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter value", "unit");
            }
        }
    }
}
