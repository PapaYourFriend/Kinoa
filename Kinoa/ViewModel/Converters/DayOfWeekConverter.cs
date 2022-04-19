using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kinoa.ViewModel.Converters
{
    public class DayOfWeekConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("ddd");
        }




        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}