using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public partial class Angle : Animatable
    {
        public static DependencyProperty RadiansProperty = DependencyProperty.Register("RadiansProperty", typeof(double), typeof(Angle), new PropertyMetadata(0d, RadiansChangedCallback));

        private static void RadiansChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// This angle in radians
        /// </summary>
        public double Rad
        {
            get => (double)GetValue(RadiansProperty);
            set => SetValue(RadiansProperty, value);
        }
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

        public string DegMinSec
        {
            get
            {
                int deg = (int)Math.Round(Deg, 0);
                if (deg > Deg) deg--;
                double minD = (Deg - deg) * 60;
                int min = (int)Math.Round(minD, 0);
                if (min > minD) min--;
                double secD = (minD - min) * 60;
                return string.Format("{0}° {1}' {2}\"", deg, min, secD);
            }
        }

        public enum AngleUnit { Deg, Rad, Grad };
    }
}
