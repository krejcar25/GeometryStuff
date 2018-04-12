using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Shapes = System.Windows.Shapes;

namespace GeometryStuff
{
    public partial class Arrow : Animatable
    {
        public CartesianPoint Origin
        {
            get { return (CartesianPoint)GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Origin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginProperty =
            DependencyProperty.Register("Origin", typeof(CartesianPoint), typeof(Arrow), new PropertyMetadata(new CartesianPoint(0, 0), RaiseArrowChangedEvent));

        public Vector Vector
        {
            get { return (Vector)GetValue(VectorProperty); }
            set { SetValue(VectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Vector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Vector", typeof(Vector), typeof(Arrow), new PropertyMetadata(new Vector(0, 0), RaiseArrowChangedEvent));
        
        public CartesianPoint End
        {
            get => Origin + Vector;
            set => Vector = value - Origin;
        }

        public string Label
        {
            get { return re.Replace((string)GetValue(LabelProperty), match => LabelParameters[match.Groups[1].Value]().ToString()); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(Arrow), new PropertyMetadata("{lenght}", RaiseArrowChangedEvent));
        
        public double LabelLengthScale
        {
            get { return (double)GetValue(LabelLengthScaleProperty); }
            set { SetValue(LabelLengthScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelLengthScale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelLengthScaleProperty =
            DependencyProperty.Register("LabelLengthScale", typeof(double), typeof(Arrow), new PropertyMetadata(1d, RaiseArrowChangedEvent));



        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register("LineThickness", typeof(double), typeof(Arrow), new PropertyMetadata(1d, RaiseArrowChangedEvent));
        
        public double HeadLength
        {
            get { return (double)GetValue(HeadLengthProperty); }
            set { SetValue(HeadLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeadLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadLengthProperty =
            DependencyProperty.Register("HeadLength", typeof(double), typeof(Arrow), new PropertyMetadata(10d, RaiseArrowChangedEvent));
        
        public double HeadWidth
        {
            get { return (double)GetValue(HeadWidthProperty); }
            set { SetValue(HeadWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeadWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadWidthProperty =
            DependencyProperty.Register("HeadWidth", typeof(double), typeof(Arrow), new PropertyMetadata(10d, RaiseArrowChangedEvent));
        
        public double HeadOffset
        {
            get { return (double)GetValue(HeadOffsetProperty); }
            set { SetValue(HeadOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeadOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadOffsetProperty =
            DependencyProperty.Register("HeadOffset", typeof(double), typeof(Arrow), new PropertyMetadata(0d, RaiseArrowChangedEvent));



        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Arrow), new PropertyMetadata(1d, RaiseArrowChangedEvent));

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(Arrow), new PropertyMetadata(Brushes.Black, RaiseArrowChangedEvent));

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(Arrow), new PropertyMetadata(Brushes.Black, RaiseArrowChangedEvent));
        
        

        public MouseButtonEventHandler MouseDown { get; set; }
        public MouseEventHandler MouseMove { get; set; }
        public MouseButtonEventHandler MouseUp { get; set; }
        public bool Movable { get; set; } = false;

        public Shapes.Polygon OutputArrow { get; set; } = null;
        public System.Windows.Controls.Label OutputLabel { get; set; } = null;

        #region Events
        public delegate void ArrowChangedEventHandler(object sender, DependencyPropertyChangedEventArgs e);
        public event ArrowChangedEventHandler ArrowChanged;
        #endregion

        #region LabelReplacements
        private readonly Dictionary<string, Func<object>> LabelParameters;
        static readonly Regex re = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);
        #endregion
    }
}
