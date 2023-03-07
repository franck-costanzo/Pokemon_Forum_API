namespace Smogon_MAUIapp.Pages;

public partial class TeamView : ContentPage
{
    string url;
	public TeamView(string url)
	{		
		InitializeComponent();
        this.url = url;
        webview.Source = url;
    }

    private async void SaveTeam(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(url);
        await Navigation.PopModalAsync();
    }
}