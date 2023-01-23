using Pokemon_Forum_API.Services;
using Smogon_MAUIapp.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class Search : ContentPage
{
    #region Properties

    private List<Posts> _postsList;
    public List<Posts> postsList
    {
        get => postsList;
        set
        {
            if (_postsList != value)
            {
                _postsList = value;
                OnPropertyChanged();
            }
        }
    }

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
        SearchService searchService = new SearchService();
        if (searchInput.Text.Length == 0)
        {
            await DisplayAlert("Search Error", "You have to enter text in order to search for posts", "OK");
        }
        else
        {
            var posts = await searchService.SearchPosts(searchInput.Text);
            if (posts != null)
            {
                searchList.ItemsSource = posts;
            }
            
        }
    }

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion

    #endregion


}