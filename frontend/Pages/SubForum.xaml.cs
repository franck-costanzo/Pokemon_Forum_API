using Microsoft.Maui.ApplicationModel.Communication;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;

namespace Smogon_MAUIapp.Pages;

public partial class SubForum : ContentPage
{
    #region Properties
    private int forumId { get; set; }

    #endregion

    #region Constructor

    public SubForum(int id)
	{
		InitializeComponent();
        loadingImage.IsVisible = true;
        UpdateItemSource(id);
    }

    private async void UpdateItemSource(int id)
    {
        subForumTitle.IsVisible = false;
        subForumTitle.Text = "";
        threadsView.IsVisible = false;
        threadsView.ItemsSource = null;
        SubForumService subForumService = new SubForumService();
        ForumService forumService = new ForumService();
        Forums forum = new Forums();
        SubForums subforum = new SubForums();

        try
        {
            

            var aTask = Task.Run(async () => {

                subforum = await subForumService.GetAllThreadsBySubForumId(id);

            });

            await Task.WhenAll(aTask);

            if (subforum != null)
            {
                var alphaTask = Task.Run(async () =>
                {
                    forum = await forumService.GetForumById(subforum.forum_id);
                });

                await Task.WhenAll(alphaTask);

                previousPage.Text = forum.name;
                subForumTitle.Text = subforum.name;
                subForumTitle.IsVisible = true;
                this.forumId = subforum.forum_id;

                if (subforum.threads != null)
                {
                    threadsView.ItemsSource = subforum.threads;
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

    #endregion

    #region Methods
    private async void PreviousPageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Forum(forumId));
    }

    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        Threads thread = viewCell.BindingContext as Threads;
        int threadId = thread.thread_id;
        await Navigation.PushAsync(new Thread(threadId, false, false));
    }

    private async void CreateThread(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CreateThread());
    }

    #endregion


}