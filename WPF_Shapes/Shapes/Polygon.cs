using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPF_Shapes.Shapes
{
    class Polygon : IShape
    {
        public int Components { get; set; } = -1;

        public Brush Fill { get; set; }

        public List<Point> Points { get; set; }
    }
}