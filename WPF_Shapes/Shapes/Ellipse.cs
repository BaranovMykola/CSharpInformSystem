using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPF_Shapes.Shapes
{
    public class Ellipse : IShape
    {
        public int Components { get; set; } = 3;

        public Brush Fill { get; set; }

        public List<Point> Points { get; set; }
    }
}