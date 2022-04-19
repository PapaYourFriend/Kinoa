using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kinoa.ViewModel.Converters
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var hours = date.Hour;
            var minutes = date.Minute;

            return $"{hours}ч {minutes}мин";
        }




        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
