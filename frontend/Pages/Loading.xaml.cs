using Microsoft.Maui.Dispatching;

namespace Smogon_MAUIapp.Pages;

public partial class Loading : ContentPage
{
	public Loading()
	{
		InitializeComponent();
        LaunchLogin();
    }

    private async void LaunchLogin()
    {
        await Task.Delay(5000);
        Application.Current.MainPage = new Login();
    }
}