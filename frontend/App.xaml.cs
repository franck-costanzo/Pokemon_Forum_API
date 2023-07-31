using Smogon_MAUIapp.Pages;
using Smogon_MAUIapp.Tools;
using System.IdentityModel.Tokens.Jwt;

namespace Smogon_MAUIapp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		try
		{
			var token = new JwtSecurityToken(Preferences.Get("token", ""));
			if(token != null && token.ValidTo > DateTime.Now)
			{
                MainPage = new AppShell();
            }
			else if (token != null && token.ValidTo < DateTime.Now)
			{
                MainPage = new Login();
            }
			else
			{
                MainPage = new Login();
            }
			/// MAINTENANT FAUT FAIRE PAREIL SUR CHAQUE PAGE ET DECODER LE TOKEN,
			/// T AS QU A LIRE COMMENT T'AS FAIT ESPECE DE GUIGNOL

        }
		catch 
		{
            MainPage = new Login();
        }
		
	}
}
