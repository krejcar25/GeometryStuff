using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public abstract partial class Coords : Animatable
    {
        public static DependencyProperty XProperty = DependencyProperty.Register("XProperty", typeof(double), typeof(Coords), new PropertyMetadata((double)0, CoordPropertyChanged));
        public static DependencyProperty YProperty = DependencyProperty.Register("YProperty", typeof(double), typeof(Coords), new PropertyMetadata((double)0, CoordPropertyChanged));
        public static DependencyProperty ZProperty = DependencyProperty.Register("ZProperty", typeof(double), typeof(Coords), new PropertyMetadata((double)0, CoordPropertyChanged));

        private static void CoordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Coords)d).Changed?.Invoke((Coords)d, new CoordsChangedEventArgs(e.Property, (double)e.OldValue));
        }
        /*
        private double x;
        private double y;
        private double z;

        /// <summary>
        /// X coordinate
        /// </summary>
        public double X
        {
            get => x;
            set
            {
                double old = x;
                x = value;
                Changed?.Invoke(this, new CoordsChangedEventArgs(CoordsChangedEventArgs.CoordNames.X, old));
            }
        }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y
        {
            get => y;
            set
            {
                double old = z;
                y = value;
                Changed?.Invoke(this, new CoordsChangedEventArgs(CoordsChangedEventArgs.CoordNames.Y, old));
            }
        }

        /// <summary>
        /// Z coordinate
        /// </summary>
        public double Z
        {
            get => z;
            set
            {
                double old = z;
                z = value;
                Changed?.Invoke(this, new CoordsChangedEventArgs(CoordsChangedEventArgs.CoordNames.Z, old));
            }
        }*/

        public double X
        {
            get => (double)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }
        public double Y
        {
            get => (double)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }
        public double Z
        {
            get => (double)GetValue(ZProperty);
            set => SetValue(ZProperty, value);
        }

        /// <summary>
        /// 2D or 3D
        /// </summary>
        public SpaceType Space { get; set; }

        public enum SpaceType { TwoDim, ThreeDim };

        #region Events
        /// <summary>
        /// Raised when any of the coordinates are changed
        /// </summary>
        public event CoordsChangedEventHandler Changed;
        #endregion

        #region Event Delegates
        /// <summary>
        /// Provides data about change of coordinate value
        /// </summary>
        public class CoordsChangedEventArgs : EventArgs
        {
            /// <summary>
            /// List of possible coordinate names
            /// </summary>
            public enum CoordNames { X, Y, Z };
            /// <summary>
            /// Which coordinate was changed
            /// </summary>
            public CoordNames Coord { get; private set; }
            /// <summary>
            /// Specifies the coordinate's valus before change
            /// </summary>
            public double OldValue { get; private set; }
            public enum Reasons { Change, Animation };
            public Reasons Reason { get; set; }

            /// <summary>
            /// Initialises new CoordsChangedEventArgs class instance
            /// </summary>
            /// <param name="coord">Which coordinate was changed</param>
            /// <param name="old">Coordinate's old valus</param>
            public CoordsChangedEventArgs(CoordNames coord, double old)
            {
                Coord = coord;
                OldValue = old;
                Reason = Reasons.Change;
            }

            public CoordsChangedEventArgs(DependencyProperty dp, double old)
            {
                Reason = Reasons.Animation;
                OldValue = old;
                switch (dp.Name)
                {
                    case "XProperty":
                        Coord = CoordNames.X;
                        break;
                    case "YProperty":
                        Coord = CoordNames.Y;
                        break;
                    case "ZProperty":
                        Coord = CoordNames.Z;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Event handler for Changed event
        /// </summary>
        /// <param name="sender">This Coords</param>
        /// <param name="e">Provides information about this change</param>
        public delegate void CoordsChangedEventHandler(Coords sender, CoordsChangedEventArgs e);
        #endregion
    }
}
