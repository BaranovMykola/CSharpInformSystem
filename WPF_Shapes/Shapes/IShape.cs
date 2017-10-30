using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPF_Shapes.Shapes
{
    public interface IShape
    {
        int Components { get; set; }

        Brush Fill { get; set; }
    }
}
