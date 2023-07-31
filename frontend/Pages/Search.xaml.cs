using Microsoft.Maui.Platform;
using Pokemon_Forum_API.Services;
using Smogon_MAUIapp.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class Search : ContentPage
{
    #region Properties

    private List<Posts> postsList = new List<Posts>();

    #endregion

    #region Constructor

    public Search()
    {
        InitializeComponent();
    }

    #endregion

    #region Methods

    private void ViewCell_Tapped(object sender, EventArgs e)
    {

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            searchList.ItemsSource = null;
#if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
            if (searchInput.Text.Length == 0)
            {
                throw new ArgumentNullException("Search String is empty");
            }
            else
            {
                loadingImage.IsVisible = true;

                var aTask = Task.Run(async () => {
                    SearchService searchService = new SearchService();
                
                        postsList = await searchService.SearchPosts(searchInput.Text);

                });

                await Task.WhenAll(aTask);

                loadingImage.IsVisible = false;
                if (postsList != null)
                {
                    pikachusearch.IsVisible = false;
                    searchList.ItemsSource = postsList;
                }
                else
                {
                    throw new InvalidOperationException("There is no result for your search");                    
                }

            }

        }
        catch (ArgumentNullException ex) 
        {
            await DisplayAlert("Search Error", "You have to enter text in order to search for posts", "OK");
        }
        catch(InvalidOperationException ex)
        {
            await DisplayAlert("Search", "There is no result for your search", "OK");
        }
    }


    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion

    #endregion

}