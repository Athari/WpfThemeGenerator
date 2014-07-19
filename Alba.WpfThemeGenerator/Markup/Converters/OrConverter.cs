using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Alba.WpfThemeGenerator.Markup
{
    public class OrConverter : IMultiValueConverter
    {
        public object Convert (object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Any(v => v is bool && (bool)v);
        }

        public object[] ConvertBack (object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}