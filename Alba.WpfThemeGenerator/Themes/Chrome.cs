using System.Windows;

namespace Alba.WpfThemeGenerator.Themes
{
    public static class Chrome
    {
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof(bool), typeof(Chrome));

        public static void SetIsFocused (DependencyObject element, bool value)
        {
            element.SetValue(IsFocusedProperty, value);
        }

        public static bool GetIsFocused (DependencyObject element)
        {
            return (bool)element.GetValue(IsFocusedProperty);
        }

        public static readonly DependencyProperty IsHoverProperty = DependencyProperty.RegisterAttached(
            "IsHover", typeof(bool), typeof(Chrome));

        public static void SetIsHover (DependencyObject element, bool value)
        {
            element.SetValue(IsHoverProperty, value);
        }

        public static bool GetIsHover (DependencyObject element)
        {
            return (bool)element.GetValue(IsHoverProperty);
        }
    }
}