namespace Smogon_MAUIapp.Pages;

public partial class Create : ContentPage
{
	public Create()
	{
		InitializeComponent();

	}

    private async void CreateAccountAsync(object sender, EventArgs e)
    {
        await DisplayAlert("Created", "Your account has been alerted", "Continue");
        Application.Current.MainPage = new AppShell();
    }

    private void ReturnLogin(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Login();
    }
}