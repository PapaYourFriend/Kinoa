using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kinoa.ViewModel.Converters
{
    public class CardConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var cardNumber = (string)value;

            if(cardNumber != null)
            {
                var result = cardNumber.Substring(12, 4);

                return $"**** **** **** {result}";
            }
            return string.Empty;
        }




        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
