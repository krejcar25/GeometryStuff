using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryStuff
{
    public partial class Angle
    {
        /// <summary>
        /// This angle in radians
        /// </summary>
        public double Rad { get; set; }
        /// <summary>
        /// This angle in Degrees
        /// </summary>
        public double Deg
        {
            get
            {
                return (Rad * 180) / Math.PI;
            }
            set
            {
                Rad = (value * Math.PI) / 180;
            }
        }
        /// <summary>
        /// This angle in Grads
        /// </summary>
        public double Grad
        {
            get
            {
                return Rad * 63.6638548;
            }
            set
            {
                Rad = value / 0.0157075;
            }
        }
        /// <summary>
        /// The direction of this angle
        /// </summary>
        public AngleDirection Direction { get; set; }

        public enum AngleDirection { Anticlockwise=1, Clockwise=-1 };
        public enum AngleUnit { Deg, Rad, Grad };
    }
}
