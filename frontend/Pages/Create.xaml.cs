using Microsoft.IdentityModel.Tokens;
using Smogon_MAUIapp.Services;

namespace Smogon_MAUIapp.Pages;

public partial class Create : ContentPage
{
    UserService userService = new UserService();

	public Create()
	{
		InitializeComponent();
	}

    private async void CreateAccountAsync(object sender, EventArgs e)
    {
        if( usernameInput.Text.IsNullOrEmpty() && 
            passwordInput.Text.IsNullOrEmpty()  && 
            repeatPasswordinput.Text.IsNullOrEmpty() &&
            emailinput.Text.IsNullOrEmpty() )
        {
            await DisplayAlert("Field Error", "All your fields must be filled !", "Continue");
        }
        else if (passwordInput.Text != repeatPasswordinput.Text)
        {
            await DisplayAlert("Password confirmation error", "Your password and password confirmation must match !", "Continue");
        }
        else
        {
            var user = await userService.CreateUser(usernameInput.Text.Trim(), passwordInput.Text.Trim(), emailinput.Text.Trim());
            if(user != null)
            {
                await DisplayAlert("Created", "Your account has been created", "Continue");
                Application.Current.MainPage = new Login();
            }
            else
            {
                await DisplayAlert("Creation Error", "We weren't able to create you a new account !", "Continue");
            }
        }
    }

    private void ReturnLogin(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Login();
    }
}