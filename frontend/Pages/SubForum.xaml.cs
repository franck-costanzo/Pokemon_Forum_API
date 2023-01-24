namespace Smogon_MAUIapp.Pages;

public partial class SubForum : ContentPage
{
    #region Properties

    #endregion

    #region Constructor

    public SubForum()
	{
		InitializeComponent();
	}

    #endregion

    #region Methods
    private async void PreviousPageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    #endregion
}