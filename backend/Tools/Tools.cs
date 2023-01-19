using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Pokemon_Forum_API.Tools
{
    public static class Tools
    {
        private static IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

        private static IConfiguration configuration = builder.Build();

        public static string connectionString = configuration.GetConnectionString("DefaultConnection");

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
