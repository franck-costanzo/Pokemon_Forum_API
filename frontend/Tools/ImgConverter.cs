using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smogon_MAUIapp.Tools
{
    internal class ImgConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool IsLikedByUser = (bool)value;

            if (!IsLikedByUser)
            {
                return "Resources/Images/coeurvide.png";
            }
            else
            {
                return "Resources/Images/coeurplein.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }

    }

}