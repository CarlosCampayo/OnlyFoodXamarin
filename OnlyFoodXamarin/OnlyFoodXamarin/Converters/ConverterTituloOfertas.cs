using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.Converters
{
    public class ConverterTituloOfertas : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if(value.ToString() != "")
                {
                    int longMax = 18;
                    String titulo = value.ToString();
                    if (titulo.Length > longMax)
                    {
                        titulo = titulo.Substring(0, longMax);
                        titulo += "...";
                    }
                    return titulo;
                } else
                {
                    return "";
                }
            } else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
