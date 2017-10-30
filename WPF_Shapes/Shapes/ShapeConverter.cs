using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_Shapes.Shapes
{
    class MultiShape
    {
        public PointCollection PolyPoints { get; set; }
        public Brush Fill { get; set; } = Brushes.Black;
        public Point Center { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ShapeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<MultiShape> l = new ObservableCollection<MultiShape>();
            foreach (var i in value as ObservableCollection<IShape>)
            {
                if (i is Shapes.Polygon)
                {
                    var poly = i as Polygon;
                    l.Add(new MultiShape() {PolyPoints = new PointCollection(poly.Points), Fill = poly.Fill});
                }
                else if (i is Shapes.Ellipse)
                {
                    var el = i as Ellipse;
                    var m = new MultiShape();
                    m.Center = el.Points[0];
                    m.Width = (int) Math.Abs(m.Center.X - el.Points[1].X);
                    m.Height = (int)Math.Abs(m.Center.Y - el.Points[2].Y);
                    l.Add(m);
                }
            }
            return l;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}