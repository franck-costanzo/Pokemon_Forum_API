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
            const string pattern = @"^(?<host>[a-zA-Z0-9]+[a-zA-Z0-9-_.]*[a-zA-Z0-9])"
                                   + "@"
                                   + @"(?<domain>[a-zA-Z0-9]+[a-zA-Z0-9-_.]*[a-zA-Z0-9](?<extension>\.[a-z]+))";

            var match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            if(match.Success
               && match.Groups["host"].Value.Length <= 255
               && match.Groups["host"].Value.Length <= 320)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
