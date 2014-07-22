using System.Windows;

namespace Alba.WpfThemeGenerator.Themes.AeroAlt.Attached
{
    public static class AeroStyles
    {
        public static readonly DependencyProperty SelectionStyleProperty = DependencyProperty.RegisterAttached(
            "SelectionStyle", typeof(Style), typeof(AeroStyles));

        public static void SetSelectionStyle (DependencyObject element, Style value)
        {
            element.SetValue(SelectionStyleProperty, value);
        }

        public static Style GetSelectionStyle (DependencyObject element)
        {
            return (Style)element.GetValue(SelectionStyleProperty);
        }
    }
}