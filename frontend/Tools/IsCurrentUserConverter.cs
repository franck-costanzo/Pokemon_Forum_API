using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smogon_MAUIapp.Tools
{
    internal class IsCurrentUserConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            int userId = (int)value;

            // Get the current user ID
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");
            var moderatorClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role_id");

            // Check if the user ID of the content matches the current user ID
            bool isCurrentUser = userId == Int32.Parse(userIdClaim.Value);

            // Return a visibility value based on whether the user is the current user or not
            if (isCurrentUser)
            {
                return true;
            }
            else if(Int32.Parse(moderatorClaim.Value) < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }

    }

}