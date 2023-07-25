using Microsoft.Maui.Platform;
namespace Smogon_MAUIapp.Pages;

public partial class PokePaste : ContentPage
{
    private string _url;
	public PokePaste(int? id = null)
	{
		InitializeComponent();

        pokepasteView.Navigating += WebView_Navigating;

    }

    private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        var url = e.Url;
        var pokepasteUrl = @"https://pokepast.es/";
        if (url.Contains(pokepasteUrl) && url.Length > (pokepasteUrl).Length)
        {
            _url = url;
        }
    }

    private async void SaveClicked(object sender, EventArgs e)
    {
#if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        
        await Clipboard.Default.SetTextAsync(_url);
        await Navigation.PopModalAsync();

        

    }
}