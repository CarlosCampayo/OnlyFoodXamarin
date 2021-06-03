using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.Converters
{
    public class IntStringConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //VALUE: El valor enlazado en el Binding
            //TargetType: El tipo de objeto enlazado
            //Parameter: Parametros asociados al binding
            //Culture: El tipo de cultura actual de la App
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
