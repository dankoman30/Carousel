using System.Windows;

namespace WpfApp1
{
    public static class CarouselProperties
    {
        public static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.RegisterAttached(
            "ScaleFactor", typeof(double), typeof(CarouselProperties), new PropertyMetadata(1.0));

        public static void SetScaleFactor(UIElement element, double value)
        {
            element.SetValue(ScaleFactorProperty, value);
        }

        public static double GetScaleFactor(UIElement element)
        {
            return (double)element.GetValue(ScaleFactorProperty);
        }
    }
}
