using System.Text.RegularExpressions;

namespace Pokemon_Forum_API.Tools
{
    public static class Tools
    {
        public static bool IsValidEmail(string email)
        {
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
