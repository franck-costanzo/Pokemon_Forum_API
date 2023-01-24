using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.Threading;

namespace Smogon_MAUIapp.Pages;

public partial class Forum : ContentPage
{
    #region Properties

    #endregion

    #region Constructor

    public Forum(int forumId)
	{
		InitializeComponent();
        UpdateItemSource(forumId);
	}

    #endregion

    #region Methods
    private async void UpdateItemSource(int id)
    {
        forumTitle.IsVisible= false;
        forumTitle.Text = "";
        subforumView.IsVisible= false;
        subforumView.ItemsSource = null;
        threadsView.IsVisible= false;
        threadsView.ItemsSource = null;
        loadingImage.IsVisible= true;
        ForumService forumService = new ForumService();
        Forums forum = new Forums();
        List<Threads> threads = new List<Threads>();

        try
        {


            var aTask = Task.Run(async () => {

                forum = await forumService.GetAllSubForumsByForumId(id);

            });

            await Task.WhenAll(aTask);

            if (forum != null)
            {
                forumTitle.Text = forum.name;
                forumTitle.IsVisible = true;

                if (forum.subforums != null)
                {                    
                    subforumView.ItemsSource = forum.subforums;
                    subforumView.IsVisible = true;                    
                }

                var bTask = Task.Run(async () =>
                {
                    threads = await forumService.GetAllThreadsByForumId(id);
                });

                await Task.WhenAll(bTask);

                if (threads != null)
                {
                    threadsView.ItemsSource = threads;
                    threadsView.IsVisible = true;
                }

                loadingImage.IsVisible = false;
            }
            else
            {
                throw new InvalidOperationException("There is no Forum with such ID");
            }

        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Search Error", "You have to enter text in order to search for posts", "OK");
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Search", ex.Message, "OK");
        }


    }

    private async void PreviousPageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    #endregion


}