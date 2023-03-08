using Smogon_MAUIapp.DTO.LikeDTO;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Smogon_MAUIapp.Pages;

public partial class Thread : ContentPage
{
    #region Properties

    private Threads thread = new Threads();
    ThreadService threadService = new ThreadService();
    ForumService forumService = new ForumService();
    SubForumService subForumService = new SubForumService();
    PostService postService = new PostService();
    LikeService likeService = new LikeService();


    private bool IsFromForum = new bool();
    private bool IsFromPost = new bool();
    private int id = 0;
    private int subforumId = 0;

    #endregion

    #region Constructor 

    public Thread() {}

    public Thread(int id, bool IsFromPosts, bool IsFromForum)
	{		
		InitializeComponent();

        MessagingCenter.Subscribe<object, int>(this, "PostCreated", (sender, threadId) =>
        {
            UpdateItemSource(threadId, IsFromPost, IsFromForum);
        });

       
        UpdateItemSource(id, IsFromPosts, IsFromForum);
        this.id = id;
        this.IsFromPost = IsFromPosts;
        this.IsFromForum = IsFromForum;
        loadingImage.IsVisible = true;
    }

    #endregion

    #region Methods

    private async void UpdateItemSource(int id, bool IsFromPosts, bool IsFromForum)
    {
        myThread.ItemsSource = null;        
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
                    this.subforumId = (int)thread.subforum_id;

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
                    this.subforumId = (int)thread.subforum_id;

                    var bTask = Task.Run(async () =>
                    {
                        thread.subforum = await subForumService.GetSubForumById((int)thread.subforum_id);
                    });

                    await Task.WhenAll(bTask);

                    loadingImage.IsVisible = false;
                    previousPage.IsVisible = true;
                    previousPage.Text = thread.subforum.name.Length <= 18 ? thread.subforum.name : thread.subforum.name.Substring(0, 17) + " ..."; ;
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
            await Navigation.PushAsync(new Forum(subforumId));
        }
        else
        {
            await Navigation.PushAsync(new SubForum(subforumId));
        }
    }
    private async void CreatePost(object sender, EventArgs e)
    {
        var content = new CreatePost(this.id);
        await Navigation.PushModalAsync(content);    
    }
    private async void Edit_Post(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var post = (Posts)button.BindingContext;
        var postid = post.post_id;
        var content = new CreatePost(this.id, postid);
        await Navigation.PushModalAsync(content);
    }
    private async void Delete_Post(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var post = (Posts)button.BindingContext;
        var postid = post.post_id;
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
        bool answer = await DisplayAlert("Question?", "Do you really want to delete your post ?", "Yes", "No");
        if (answer)
        {
            await postService.DeletePost(postid, jwtToken);
            UpdateItemSource(id, IsFromPost, IsFromForum);
        }
    }
    private async void reloadPostView(int post_id)
    {
        var post = await postService.GetAllLikesByPostId(post_id);
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");
        string postAndUserId = post.post_id + "-" + userIdClaim.Value;
        var liked = await likeService.GetLikeByPostIdAndUserId(postAndUserId);
        if (liked.like_id != 0)
        {
            post.IsLikedByUser = true;
        }
        post.likeCount = post.likes.Count;

        for (var i = 0; i < thread.posts.Count; i++)
        {
            if ( post.post_id == thread.posts[i].post_id)
            {                
                thread.posts[i] = post;
            }
        }
        myThread.ItemsSource = null;
        myThread.ItemsSource = thread.posts;
    }

    #endregion

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");

        var userId = Int32.Parse(userIdClaim.Value);

        var button = (ImageButton)sender;
        var post = (Posts)button.BindingContext;
        var postid = post.post_id;

        LikeDto likeDto = new LikeDto();
        likeDto.post_id = postid;
        likeDto.user_id = userId;

        if (button.Source.ToString().Substring(6) == "Resources/Images/coeurvide.png")
        {
            try
            {
                var like = await likeService.CreateLike(likeDto, jwtToken);
                reloadPostView(postid);
            }
            catch (Exception)
            {
                await DisplayAlert("Like Error", "There has been a problem when trying to save your like", "OK");
            }            
        }
        else
        {
            try
            {
                var like = await likeService.DeleteLike(likeDto, jwtToken);
                reloadPostView(postid);
            }
            catch (Exception)
            {
                await DisplayAlert("Like Error", "There has been a problem when trying to remove your like", "OK");
            }
            
        }
    }
}