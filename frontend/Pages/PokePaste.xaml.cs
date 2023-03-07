using Microsoft.Maui.Platform;
namespace Smogon_MAUIapp.Pages;

public partial class PokePaste : ContentPage
{
	public PokePaste(int? id = null)
	{
		InitializeComponent();
	}

    private async void SaveClicked(object sender, EventArgs e)
    {
#if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        UrlWebViewSource currentURL = (UrlWebViewSource)pokepasteView.Source;
        await Clipboard.Default.SetTextAsync(currentURL.Url);
        await Navigation.PopModalAsync();

        

    }
}