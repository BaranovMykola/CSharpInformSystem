using System;
using System.Globalization;
using System.Windows.Data;

namespace TaxiGUI
{
    class StringIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int target;
            if (value == null || !int.TryParse(value.ToString(), out target))
            {
                return 0;
            }
            return target;
        }
    }
}