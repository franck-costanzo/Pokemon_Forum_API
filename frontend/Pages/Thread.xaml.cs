using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;

namespace Smogon_MAUIapp.Pages;

public partial class Thread : ContentPage
{
    #region Properties

    private Threads thread = new Threads();

    private bool IsFromForum = new bool();
    private bool IsFromPost = new bool();
    private int id = 0;


    #endregion

    #region Constructor 

    public Thread() {}

    public Thread(int id, bool IsFromPosts, bool IsFromForum)
	{		
		InitializeComponent();
        this.id = id;
        this.IsFromForum = IsFromForum;
        loadingImage.IsVisible = true;
        UpdateItemSource(id, IsFromPosts, IsFromForum);
    }

    #endregion

    #region Methods

    private async void UpdateItemSource(int id, bool IsFromPosts, bool IsFromForum)
    {
        myThread.ItemsSource = null;
        ThreadService threadService = new ThreadService();
        ForumService forumService = new ForumService();
        SubForumService subForumService = new SubForumService();
        PostService postService = new PostService();
        Posts post = new Posts();        

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

                if (thread != null && IsFromForum)
                {

                    var bTask = Task.Run(async () =>
                    {
                        thread.forum = await forumService.GetForumById((int)thread.forum_id);
                    });

                    await Task.WhenAll(bTask);

                    loadingImage.IsVisible = false;
                    previousPage.IsVisible = true;
                    previousPage.Text = thread.forum.name;
                    threadView.IsVisible = true;
                    threadTitle.Text = thread.title;
                    myThread.ItemsSource = thread.posts;

                    
                }
                else if (thread != null && !IsFromForum)
                {
                    var bTask = Task.Run(async () =>
                    {
                        thread.subforum = await subForumService.GetSubForumById((int)thread.subforum_id);
                    });

                    await Task.WhenAll(bTask);

                    loadingImage.IsVisible = false;
                    previousPage.IsVisible = true;
                    previousPage.Text = thread.subforum.name;
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
    private async void PreviousPageClicked(object sender, EventArgs e)
    {
        if (IsFromForum)
        {
            await Navigation.PushAsync(new Forum(id));
        }
        else
        {
            await Navigation.PushAsync(new SubForum(id));
        }
    }



    #endregion

    private async void CreatePost(object sender, EventArgs e)
    {
        var content = new CreatePost();
        await Navigation.PushModalAsync(content);
    }
}