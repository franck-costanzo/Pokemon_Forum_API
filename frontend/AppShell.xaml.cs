using Smogon_MAUIapp.Pages;

namespace Smogon_MAUIapp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("login", typeof(Login));
        Routing.RegisterRoute("main", typeof(MainPage));
        Routing.RegisterRoute("search", typeof(Search));
        Routing.RegisterRoute("profile", typeof(Profile));
    }
}
