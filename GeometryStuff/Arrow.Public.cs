using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Shapes = System.Windows.Shapes;
using static GeometryStuff.Coords;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public partial class Arrow : Animatable
    {
        public Arrow(CartesianPoint origin, CartesianPoint end, string label = "")
        {
            LabelParameters = InitReplaceDict();
            origin.CheckSpace(SpaceType.TwoDim, "Arrow may only be created from a 2D point", "origin");
            origin.Changed += Origin_Changed;
            Origin = origin;
            end.CheckSpace(SpaceType.TwoDim, "Arrow may only be created from a 2D point", "end");
            end.Changed += End_Changed;
            Vector = new Vector(origin, end);
        }

        public Arrow(CartesianPoint origin, Vector vector, string label = "")
        {
            LabelParameters = InitReplaceDict();
            origin.CheckSpace(SpaceType.TwoDim, "Arrow may only be created from a 2D point", "origin");
            origin.Changed += Origin_Changed;
            Origin = origin;
            vector.CheckSpace(SpaceType.TwoDim, "Arrow may only be created from a 2D vector", "vector");
            vector.Changed += Vector_Changed;
            Vector = vector;
        }

        private Dictionary<string,Func<object>> InitReplaceDict()
        {
            Dictionary<string, Func<object>> ret = new Dictionary<string, Func<object>>()
            {
                { "length",    () => Math.Round(Vector.Length / LabelLengthScale, 3) },
                { "originX",   () => Origin.X },
                { "originY",   () => Origin.Y },
                { "origin",    () => Origin },
                { "endx",      () => End.X },
                { "endY",      () => End.Y },
                { "end",       () => End },
                { "vectorX",   () => Vector.X },
                { "vectorY",   () => Vector.Y },
                { "vector",    () => Vector },
            };
            return ret;
        }

        private void Origin_Changed(Coords sender, CoordsChangedEventArgs e)
        {
            Origin = (CartesianPoint)sender;
        }

        private void End_Changed(Coords sender, CoordsChangedEventArgs e)
        {
            Vector = (CartesianPoint)sender - Origin;
        }

        private void Vector_Changed(Coords sender, CoordsChangedEventArgs e)
        {
            Vector = (Vector)sender;
        }

        public void Output(ref Canvas canvas, int zIndex = 0, MouseButtonEventHandler mouseLeftButtonDown = null, object tag = null)
        {
            canvas.Children.Add(Output(zIndex, mouseLeftButtonDown, tag));
        }

        public void Output(ref Canvas canvas, Label label, int zIndex = 0)
        {
            canvas.Children.Add(Output(label));
        }

        public Shapes.Polygon Output(int zIndex = 0, MouseButtonEventHandler mouseLeftButtonDown = null, object tag = null)
        {
            if (HeadOffset < 0 || HeadOffset > 1) throw new Exception("Head offset must be greater than 0 and less than 1");

            Shapes.Polygon arrow = GenerateArrow();

            if (Movable)
            {
                arrow.MouseLeftButtonDown += MouseDown;
                arrow.MouseMove += MouseMove;
                arrow.MouseLeftButtonUp += MouseUp;
            }

            arrow.Stroke = Stroke;
            arrow.StrokeThickness = StrokeThickness;
            arrow.Fill = Fill;

            arrow.SetValue(Panel.ZIndexProperty, zIndex);

            if (mouseLeftButtonDown != null) arrow.MouseLeftButtonDown += mouseLeftButtonDown;
            arrow.Tag = tag;

            OutputArrow = arrow;

            return arrow;
        }

        public Label Output(Label label, int zIndex = 0)
        {
            label.Content = Label;
            Vector offset = Vector.Null;
            label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Size labelSize = label.DesiredSize;
            if (Vector.X == 0 && Vector.Y != 0)
            {
                offset = new Vector(0, labelSize.Height / 2);
            }
            else if (Vector.X != 0 && Vector.Y == 0)
            {
                offset = new Vector(labelSize.Width / 2, labelSize.Height);
            }
            else
            {
                if ((Vector.X > 0 && Vector.Y > 0) || (Vector.X < 0 && Vector.Y < 0))
                {
                    offset = new Vector(0, labelSize.Height);
                }
                else if ((Vector.X > 0 && Vector.Y < 0) || (Vector.X < 0 && Vector.Y > 0))
                {
                    offset = new Vector(labelSize.Width, labelSize.Height);
                }
            }
            CartesianPoint labelCorner = (Origin + (Vector * 0.5)) - offset;
            label.SetValue(Canvas.LeftProperty, labelCorner.X);
            label.SetValue(Canvas.TopProperty, labelCorner.Y);
            label.SetValue(Panel.ZIndexProperty, zIndex);
            OutputLabel = label;
            return label;
        }

        public Tuple<bool,bool> Update(ref Canvas canvas)
        {
            bool arrowSuccess = false;
            bool labelSuccess = false;
            if (canvas.Children.Contains(OutputArrow))
            {
                int index = canvas.Children.IndexOf(OutputArrow);
                Shapes.Polygon arrow = GenerateArrow();
                ((Shapes.Polygon)canvas.Children[index]).Stroke = Stroke;
                ((Shapes.Polygon)canvas.Children[index]).StrokeThickness = StrokeThickness;
                ((Shapes.Polygon)canvas.Children[index]).Fill = Fill;
                ((Shapes.Polygon)canvas.Children[index]).Points = arrow.Points;
                OutputArrow = (Shapes.Polygon)canvas.Children[index];
                arrowSuccess = true;
            }
            if (canvas.Children.Contains(OutputLabel))
            {
                canvas.Children.Remove(OutputLabel);
                canvas.Children.Add(Output(OutputLabel));
                labelSuccess = true;
            }
            return new Tuple<bool, bool>(arrowSuccess, labelSuccess);
        }

        public void EnableMovement(MouseButtonEventHandler d, MouseEventHandler m, MouseButtonEventHandler u)
        {
            MouseDown = d;
            MouseMove = m;
            MouseUp = u;
            Movable = true;
        }

        private Shapes.Polygon GenerateArrow()
        {
            Shapes.Polygon arrow = new Shapes.Polygon();
            CartesianPoint headTip = Origin + (Vector * (1 - HeadOffset));
            CartesianPoint headBase = headTip + (-Vector).WithLength(HeadLength);
            Tuple<Vector, Vector> headWidth = Vector.WithLength(HeadWidth / 2).GetPerpendicularVectorsPair();
            CartesianPoint headCorner1 = headBase + headWidth.Item1;
            CartesianPoint headCorner2 = headBase + headWidth.Item2;
            Tuple<Vector, Vector> lineWidth = Vector.WithLength(LineThickness / 2).GetPerpendicularVectorsPair();
            CartesianPoint lineBegin1 = Origin + lineWidth.Item1;
            CartesianPoint lineBegin2 = Origin + lineWidth.Item2;
            CartesianPoint lineEnd1 = headBase + lineWidth.Item1;
            CartesianPoint lineEnd2 = headBase + lineWidth.Item2;

            arrow.Points.Add(headTip);
            arrow.Points.Add(headCorner1);
            arrow.Points.Add(lineEnd1);
            arrow.Points.Add(lineBegin1);
            arrow.Points.Add(lineBegin2);
            arrow.Points.Add(lineEnd2);
            arrow.Points.Add(headCorner2);

            return arrow;
        }

        public static void RaiseArrowChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Arrow sender = (Arrow)d;
            sender.ArrowChanged?.Invoke(sender, e);
        }

        protected override Freezable CreateInstanceCore() => new Arrow(new CartesianPoint(0, 0), new CartesianPoint(1, 1));
    }
}
