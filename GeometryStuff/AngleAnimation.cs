using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeometryStuff
{
    public class AngleAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(Angle);


        public Angle From
        {
            get { return (Angle)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for From.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(Angle), typeof(AngleAnimation), new PropertyMetadata(new Angle(0)));


        public Angle To
        {
            get { return (Angle)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        // Using a DependencyProperty as the backing store for To.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(Angle), typeof(AngleAnimation), new PropertyMetadata(new Angle(0)));

        public AngleAnimation(Angle fromValue, Angle toValue, Duration duration)
        {
            From = fromValue;
            To = toValue;
            Duration = duration;
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            Angle from = (Angle)GetValue(FromProperty);
            Angle to = (Angle)GetValue(ToProperty);
            return new Angle(animationClock.CurrentProgress.Value * (to.Rad - from.Rad) + from.Rad);
        }

        protected override Freezable CreateInstanceCore() => new AngleAnimation(new Angle(0), new Angle(0), TimeSpan.FromSeconds(1));
    }
}
