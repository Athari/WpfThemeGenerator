using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Alba.WpfThemeGenerator.Markup
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueVisibility { get; set; }
        public Visibility FalseVisibility { get; set; }

        public BoolToVisibilityConverter (Visibility trueVisibility = Visibility.Visible, Visibility falseVisibility = Visibility.Collapsed)
        {
            TrueVisibility = trueVisibility;
            FalseVisibility = falseVisibility;
        }

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;
            return (bool)value ? TrueVisibility : FalseVisibility;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                return DependencyProperty.UnsetValue;
            var visibility = (Visibility)value;
            if (visibility == TrueVisibility)
                return true;
            else if (visibility == FalseVisibility)
                return false;
            else
                return Binding.DoNothing;
        }
    }
}