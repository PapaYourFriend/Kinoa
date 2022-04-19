using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kinoa.ViewModel.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var year = date.Year;
            var day = date.Day;
            var month = date.ToString("MMM");

            return $"{month} {day}, {year}";
        }




        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
