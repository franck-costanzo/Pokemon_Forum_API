using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;

namespace Smogon_MAUIapp.Pages;

public partial class Thread : ContentPage
{
    #region Constructor 

    public Thread() {}

    public Thread(int id, bool IsFromPosts)
	{		
		InitializeComponent();
        loadingImage.IsVisible = true;
        pokeball.RelRotateTo(360, 2000);
        pokeball.Rotation = 10;
        UpdateItemSource(id, IsFromPosts);
    }

    #endregion

    #region Methods

    private async void UpdateItemSource(int id, bool IsFromPosts)
    {
        myThread.ItemsSource = null;
        ThreadService threadService = new ThreadService();
        PostService postService = new PostService();
        Posts post = new Posts();
        Threads thread = new Threads();

        if (IsFromPosts)
        {
            try
            {
                

                var aTask = Task.Run(async () => {

                    post = await postService.GetPostById(id);

                });

                await Task.WhenAll(aTask);

                if (post != null)
                {
                    var bTask = Task.Run(async () =>
                    {
                        thread = await threadService.GetThreadWithPostByThreadId(post.thread_id);
                    });

                    await Task.WhenAll(bTask);

                    
                    if (thread != null)
                    {
                        loadingImage.IsVisible = false;
                        threadView.IsVisible = true;
                        threadTitle.Text = thread.title;
                        myThread.ItemsSource = thread.posts;                        
                    }
                    else
                    {
                        throw new InvalidOperationException("There is no thread with such ID");
                    }
                }
                else
                {
                    throw new InvalidOperationException("There is no post with such ID");
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
        else
        {
            try
            {

                var aTask = Task.Run(async () => {
                    thread = await threadService.GetThreadWithPostByThreadId(id);
                });

                await Task.WhenAll(aTask);

                if (thread != null)
                {
                    loadingImage.IsVisible = false;
                    threadView.IsVisible = true;
                    threadTitle.Text = thread.title;
                    myThread.ItemsSource = thread.posts;
                }
                else
                {
                    throw new InvalidOperationException("There is no thread with such ID");
                }


            }
            catch (ArgumentNullException ex)
            {
                await DisplayAlert("Search Error", "You have to enter text in order to search for posts", "OK");
            }
            catch (InvalidOperationException ex)
            {
                await DisplayAlert("Search", "There is no result for your search", "OK");
            }
        }
    }
    private void PreviousPageLabel_Tapped(object sender, EventArgs e)
    {

    }


    #endregion

    
}