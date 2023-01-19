namespace Smogon_MAUIapp.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private void LoginUser(object sender, EventArgs e)
	{
        Application.Current.MainPage = new AppShell();
	}

    private async void PasswordForgottenAsync(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Forgotten Password :", "Write down your email address :");
    }

    private void CreateAccount(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Create(); 
    }
}