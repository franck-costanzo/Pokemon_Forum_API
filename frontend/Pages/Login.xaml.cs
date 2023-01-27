using Microsoft.IdentityModel.Tokens;
using Smogon_MAUIapp.Services;

namespace Smogon_MAUIapp.Pages;

public partial class Login : ContentPage
{
    private UserService userService = new UserService();
	public Login()
	{
		InitializeComponent();
	}

    private async void LoginUser(object sender, EventArgs e)
	{
        if(usernameInput.Text.IsNullOrEmpty() || passwordInput.Text.IsNullOrEmpty())
        {
            await DisplayAlert("Error", "All the fields must be filled in order to login", "OK");
        }
        else
        {
            var token = await userService.LoginUserJWT(usernameInput.Text.Trim(), passwordInput.Text.Trim());
            if(token != null)
            {
                Preferences.Set("token", token.RawData);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Error", "We have been unable to log you in with the information provided", "OK");
            }
        }
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