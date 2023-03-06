using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Smogon_MAUIapp.Tools
{
    static class Jwtools
    {
        static System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public static string Issuer 
        {
            get
            {
                return "com.smogon-forum";
            }
        }

        public static string Audience 
        {
            get
            {
                return "com.strat-pokemon-trainers";
            }
        }

        public static string Key 
        {
            get
            {
                return "glWePq77Bg}RCl>Df7ya`2kKjzBtI6wZ.`2-)cjMb#j_V:hy2v";
            }
        }


    }
}
