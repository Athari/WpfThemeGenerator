using System;
using System.Collections;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Media;
using Alba.Framework.Collections;
using Alba.Framework.Text;
using Alba.Framework.Windows.Mvvm;

namespace Alba.WpfThemeGenerator.Editor.ViewModels
{
    public class EditorModel : ModelBase
    {
        public ObservableCollectionEx<ResourceDictionaryModel> ResourceDictionaries { get; private set; }

        public EditorModel ()
        {
            ResourceDictionaries = new ObservableCollectionEx<ResourceDictionaryModel>();

            Assembly stylesAssembly = Assembly.Load("Alba.WpfThemeGenerator");
            foreach (string resourceName in stylesAssembly.GetManifestResourceNames())
                using (var resourceReader = new ResourceReader(stylesAssembly.GetManifestResourceStream(resourceName)))
                    foreach (DictionaryEntry resourceEntry in resourceReader)
                        ResourceDictionaries.Add(new ResourceDictionaryModel(stylesAssembly, ((string)resourceEntry.Key).RemovePostfix(".baml")));
        }
    }

    public class ResourceDictionaryModel : ModelBase
    {
        private readonly Assembly _stylesAssembly;
        private ResourceDictionary _resources;

        public string ResourceName { get; private set; }
        public ObservableCollectionEx<ColorModel> Colors { get; private set; }

        public ResourceDictionaryModel (Assembly stylesAssembly, string resourceName)
        {
            ResourceName = resourceName;
            Colors = new ObservableCollectionEx<ColorModel>();
            _stylesAssembly = stylesAssembly;
            _resources = new ResourceDictionary {
                Source = new Uri("/{0};component/{1}.xaml".Fmt(stylesAssembly.GetName().Name, ResourceName), UriKind.Relative)
            };
            foreach (DictionaryEntry resource in _resources) {
                string key = resource.Key as string;
                if (key == null)
                    continue;
                var solidBrush = resource.Value as SolidColorBrush;
                if (solidBrush != null) {
                    Colors.Add(new ColorModel(key, solidBrush.Color));
                }
                var gradientBrush = resource.Value as GradientBrush;
                if (gradientBrush != null) {
                    for (int i = 0; i < gradientBrush.GradientStops.Count; i++)
                        Colors.Add(new ColorModel("{0}#{1}".Fmt(key, i), gradientBrush.GradientStops[i].Color));
                }
            }
        }

        public override string ToString ()
        {
            return ResourceName;
        }
    }

    public class ColorModel : ModelBase
    {
        public string Key { get; private set; }
        public Color Color { get; set; }

        public ColorModel (string key, Color color)
        {
            Key = key;
            Color = color;
        }

        public SolidColorBrush Brush
        {
            get { return new SolidColorBrush(Color); }
        }
    }
}