using System.Windows;

namespace Alba.WpfThemeGenerator.Themes
{
    public static class Chrome
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached(
            "IsChecked", typeof(bool?), typeof(Chrome), new PropertyMetadata(false));

        public static void SetIsChecked (DependencyObject element, bool? value)
        {
            element.SetValue(IsCheckedProperty, value);
        }

        public static bool? GetIsChecked (DependencyObject element)
        {
            return (bool?)element.GetValue(IsCheckedProperty);
        }

        public static readonly DependencyProperty IsDefaultedProperty = DependencyProperty.RegisterAttached(
            "IsDefaulted", typeof(bool), typeof(Chrome), new PropertyMetadata(false));

        public static void SetIsDefaulted (DependencyObject element, bool value)
        {
            element.SetValue(IsDefaultedProperty, value);
        }

        public static bool GetIsDefaulted (DependencyObject element)
        {
            return (bool)element.GetValue(IsDefaultedProperty);
        }

        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof(bool), typeof(Chrome), new PropertyMetadata(false));

        public static void SetIsFocused (DependencyObject element, bool value)
        {
            element.SetValue(IsFocusedProperty, value);
        }

        public static bool GetIsFocused (DependencyObject element)
        {
            return (bool)element.GetValue(IsFocusedProperty);
        }

        public static readonly DependencyProperty IsHoverProperty = DependencyProperty.RegisterAttached(
            "IsHover", typeof(bool), typeof(Chrome), new PropertyMetadata(false));

        public static void SetIsHover (DependencyObject element, bool value)
        {
            element.SetValue(IsHoverProperty, value);
        }

        public static bool GetIsHover (DependencyObject element)
        {
            return (bool)element.GetValue(IsHoverProperty);
        }

        public static readonly DependencyProperty IsPressedProperty = DependencyProperty.RegisterAttached(
            "IsPressed", typeof(bool), typeof(Chrome), new PropertyMetadata(false));

        public static void SetIsPressed (DependencyObject element, bool value)
        {
            element.SetValue(IsPressedProperty, value);
        }

        public static bool GetIsPressed (DependencyObject element)
        {
            return (bool)element.GetValue(IsPressedProperty);
        }
    }
}