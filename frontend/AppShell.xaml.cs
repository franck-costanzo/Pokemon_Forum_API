using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Pages;

namespace Smogon_MAUIapp;

public partial class AppShell : Shell
{
    public AppShell()
	{
        Routing.RegisterRoute("Home", typeof(MainPage));
        Routing.RegisterRoute("Search", typeof(Search));
        Routing.RegisterRoute("Profile", typeof(Profile));
        Routing.RegisterRoute("ShowDown", typeof(Showdown));

        InitializeComponent();
    }
}
