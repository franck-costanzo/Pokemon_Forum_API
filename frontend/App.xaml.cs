using Smogon_MAUIapp.Pages;

namespace Smogon_MAUIapp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new Loading();
	}
}
