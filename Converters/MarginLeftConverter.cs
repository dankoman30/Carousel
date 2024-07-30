using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace WpfApp1.Converters
{
    public class MarginLeftConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is double angle &&
                values[1] is double carouselHeight &&
                values[2] is double textOffset)
            {
                double normalizedY = (Math.Sin(angle * Math.PI / 180) + 1) / 2; // Normalize angle to 0-1 range
                double leftMargin = textOffset * (1 - 2 * normalizedY); // Map 0-1 to 20 to -20
                return new Thickness(leftMargin, 0, 0, 0);
            }
            return new Thickness(0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
