using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public partial class CartesianPointAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(CartesianPoint);

        public CartesianPoint From
        {
            get { return (CartesianPoint)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for From.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(CartesianPoint), typeof(CartesianPointAnimation), new PropertyMetadata(new CartesianPoint(0, 0, 0)));

        public CartesianPoint To
        {
            get { return (CartesianPoint)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        // Using a DependencyProperty as the backing store for To.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(CartesianPoint), typeof(CartesianPointAnimation), new PropertyMetadata(new CartesianPoint(1, 1, 1)));

        public CartesianPointAnimation(CartesianPoint fromValue, CartesianPoint toValue, Duration duration)
        {
            fromValue.CheckSpace(toValue, "Both Coords must be same space type");
            try
            {
                From = fromValue;
            }
            catch (SpaceTypeNotSameException)
            {
                Debug.WriteLine("SpaceTypeNotSameException occured. This is perfectly normal in .ctor when setting the From value to a 2D Coords");
            }
            To = toValue;
            Duration = duration;
        }

        public CartesianPointAnimation(CartesianPoint fromValue, Vector direction, Duration duration)
        {
            fromValue.CheckSpace(direction, "Both fromValue and direction must be the same space type");
            From = fromValue;
            To = fromValue + direction;
            Duration = duration;
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            CartesianPoint from = (CartesianPoint)GetValue(FromProperty);
            CartesianPoint to = (CartesianPoint)GetValue(ToProperty);
            CartesianPoint ret = new CartesianPoint(animationClock.CurrentProgress.Value * (to.X - from.X) + from.X, animationClock.CurrentProgress.Value * (to.Y - from.Y) + from.Y, animationClock.CurrentProgress.Value * (to.Z - from.Z) + from.Z);
            ret.To2D(to.Space);
            return ret;
        }

        protected override Freezable CreateInstanceCore() => new CartesianPointAnimation(new CartesianPoint(0, 0, 0), new CartesianPoint(1, 1, 1), TimeSpan.FromSeconds(1));
    }
}
