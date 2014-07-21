using System.Windows;
using System.Windows.Data;

// ReSharper disable RedundantArgumentDefaultValue
namespace Alba.WpfThemeGenerator.Markup
{
    public static class Converters
    {
        public static readonly IMultiValueConverter Or = new OrConverter();
        public static readonly IValueConverter BoolToVisibleCollapsed = new BoolToVisibilityConverter(Visibility.Visible, Visibility.Collapsed);
        public static readonly IValueConverter BoolToVisibleHidden = new BoolToVisibilityConverter(Visibility.Visible, Visibility.Hidden);
        public static readonly IValueConverter BoolToCollapsedVisible = new BoolToVisibilityConverter(Visibility.Collapsed, Visibility.Visible);
    }
}