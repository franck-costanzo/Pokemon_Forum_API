using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.Threading;

namespace Smogon_MAUIapp.Pages;

public partial class Forum : ContentPage
{
    #region Properties

    private Forums forum = new Forums();

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

        loadingImage.IsVisible = true;
        forumTitle.IsVisible= false;
        forumTitle.Text = "";
        subforumView.IsVisible= false;
        subforumView.ItemsSource = null;
        threadsView.IsVisible= false;
        threadsView.ItemsSource = null;
        ForumService forumService = new ForumService();
        
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
                    subforumView.IsVisible = true;                    
                    subforumView.ItemsSource = forum.subforums;
                }

                /*var bTask = Task.Run(async () =>
                {
                    threads = await forumService.GetAllThreadsByForumId(id);
                });

                await Task.WhenAll(bTask);

                if (threads != null)
                {
                    threadsView.IsVisible = true;
                    threadsView.ItemsSource = threads;
                }*/

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

    private async void SubForumTapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        SubForums subforum = viewCell.BindingContext as SubForums;
        int id = subforum.subforum_id;
        await Navigation.PushAsync(new SubForum(id));
    }


    private async void ThreadTapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        SubForums subforum = viewCell.BindingContext as SubForums;
        int id = subforum.subforum_id;
        await Navigation.PushAsync(new Thread(id, false, true));
    }

    private async void PreviousPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    #endregion


}