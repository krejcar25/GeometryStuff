using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeometryStuff
{
    public partial class Line : Coords
    {
        public Vector Vector
        {
            get { return (Vector)GetValue(VectorProperty); }
            set { SetValue(VectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Vector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VectorProperty =
            DependencyProperty.Register("Vector", typeof(Vector), typeof(Line), new PropertyMetadata(new Vector(0, 0)));
        
    }
}
