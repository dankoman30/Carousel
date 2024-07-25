using System.Globalization;
using System.Windows.Data;

namespace WpfApp1
{
    public class MultiplyConverter : IValueConverter
    {
        public double Factor { get; set; } = 1.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue * Factor;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
