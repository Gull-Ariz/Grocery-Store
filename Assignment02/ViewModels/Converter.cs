using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Assignment02.ViewModels
{
    public class Converter : IMultiValueConverter
    {
        /*this Converter class method Convert is used to make list of parameters which are send in command parameters
         since, passwordbox is not directly bind with View Model so we send passwordbox as command Parameter.*/
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.ToList();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
