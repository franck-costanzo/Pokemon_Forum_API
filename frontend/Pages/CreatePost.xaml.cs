using Microsoft.IdentityModel.Tokens;
using Smogon_MAUIapp.DTO.PostDTO;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using Smogon_MAUIapp.Tools;
using System.IdentityModel.Tokens.Jwt;

namespace Smogon_MAUIapp.Pages;

public partial class CreatePost : ContentPage
{
    #region Properties

    PostService postService = new PostService();
    private int threadId = 0;
    private int? postid = null;

    public event EventHandler ModalClosed;

    #endregion

    #region Constructor

    public CreatePost(int id, int? post_id = null)
	{
		InitializeComponent();
        this.threadId = id;
        if (post_id != null)
        {
            this.postid = post_id;
            UpdateItemSource((int)post_id);
        }
    }

    #endregion

    #region Methods

    private async void UpdateItemSource(int id)
    {
        Posts post = await postService.GetPostById(id);
        MarkdownEditor.Text = post.content;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(MarkdownEditor.Text))
        {
            await DisplayAlert("Post", "You need to write something to post your thread !", "OK");
        }
        else if (MarkdownEditor.Text.Length < 50)
        {
            await DisplayAlert("Post", "Your thread needs to be at least 50 letters long", "OK");
        }
        else
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");
            PostDto postDto = new PostDto();

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {              
                postDto.content = MarkdownEditor.Text;
                postDto.create_date = DateTime.Now;
                postDto.user_id = userId;
                postDto.thread_id = this.threadId;
            }

            Posts post; 
            if(postid != null)
            {
                post = await postService.UpdatePost((int)postid, postDto, jwtToken);
            }
            else
            {
                post = await postService.CreatePost(postDto, jwtToken);
            }
            

            if (post != null)
            {
                if(postid != null)
                {
                    await DisplayAlert("Created", "The post has been updated", "Continue");
                }
                else
                {
                    await DisplayAlert("Created", "The post has been created", "Continue");
                }
                await Navigation.PopModalAsync();
                MessagingCenter.Send<object, int>(this, "PostCreated", threadId);
            }
            else
            {
                await DisplayAlert("Creation Error", "We haven't been able to create your post !", "Continue");
            }
        }
    }

    private void BoldButton_Clicked(object sender, EventArgs e)
    {
        int length = MarkdownEditor.SelectionLength;
        int cursor = MarkdownEditor.CursorPosition;
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor, "**");
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor + length + 2, "**");
    }

    private void ItalicButton_Clicked(object sender, EventArgs e)
    {
        int length = MarkdownEditor.SelectionLength;
        int cursor = MarkdownEditor.CursorPosition;
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor, "_");
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor + length + 1, "_");
    }

    private void LinkButton_Clicked(object sender, EventArgs e)
    {
        int length = MarkdownEditor.SelectionLength;
        int cursor = MarkdownEditor.CursorPosition;
        string address = MarkdownEditor.Text.Substring(cursor, length);
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor, "[");
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor + length + 1, $"]({address})");
    }

    private void ImgButton_Clicked(object sender, EventArgs e)
    {
        int length = MarkdownEditor.SelectionLength;
        int cursor = MarkdownEditor.CursorPosition;
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor, "![");
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor + length + 2, "](https://example.com)");
    }

    private async void PokepasteTapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PokePaste());
    }

    #endregion
}