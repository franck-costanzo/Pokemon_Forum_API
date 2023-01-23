using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smogon_MAUIapp.Tools
{
    class StringTruncate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)value;
            if(text.Length > 150)
            {
                text = text.Substring(0, 149) + " [...]";
                
                return text;
            } else
            {
                return text;
            }
               
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }

    }
}
