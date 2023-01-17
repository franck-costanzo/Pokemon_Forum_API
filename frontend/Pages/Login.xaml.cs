namespace Smogon_MAUIapp.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();

	}

    private void Button_Clicked(object sender, EventArgs e)
	{
        Application.Current.MainPage = new AppShell();
	}


}