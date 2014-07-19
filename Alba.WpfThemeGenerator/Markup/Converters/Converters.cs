using System.Windows.Data;

namespace Alba.WpfThemeGenerator.Markup
{
    public static class Converters
    {
        public static readonly IMultiValueConverter Or = new OrConverter();
    }
}