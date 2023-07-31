using Smogon_MAUIapp.DTO.PostDTO;
using Smogon_MAUIapp.DTO.ThreadDTO;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Smogon_MAUIapp.Pages;

public partial class CreateThread : ContentPage
{
    #region Properties
    
    ThreadService threadService = new ThreadService();
    PostService postService = new PostService();
    int subForumId = 0;

    #endregion
    //TODO:
    /*
        - implementation de la methode pour sauvegarder
        - aggrémenter le postService pour la création du thread
        - rajouter l'id du post et gérer l'id de l'utilisateur qui créé le thread
     */

    #region Constructor

    public CreateThread(int subforumId)
	{
		InitializeComponent();
        this.subForumId = subforumId;
    }

    #endregion

    #region Methods

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if(String.IsNullOrEmpty(titleEntry.Text))
        {
            await DisplayAlert("Title", "You need to set a title !", "OK");
        }
        else if(titleEntry.Text.Length < 8 ) 
        {
            await DisplayAlert("Title", "Your title needs to be at least 8 letters long", "OK");
        }
        else if (String.IsNullOrEmpty(MarkdownEditor.Text))
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

            ThreadDto threadDto = new ThreadDto();

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                threadDto.title = titleEntry.Text;
                threadDto.create_date = DateTime.Now;
                threadDto.user_id = userId;
                threadDto.subforum_id = subForumId;
            }

            var thread = await threadService.CreateThread(threadDto, jwtToken);

            if (thread != null)
            {
                PostDto postDto = new PostDto();
                postDto.content = MarkdownEditor.Text;
                postDto.create_date = DateTime.Now;
                postDto.user_id = Int32.Parse(userIdClaim.Value);
                postDto.thread_id = thread.thread_id;

                var post = await postService.CreatePost(postDto, jwtToken);
                if (post != null)
                {
                    await DisplayAlert("Created", "The thread has been created", "Continue");
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send<object, int>(this, "threadCreated", subForumId);
                }
                else
                {
                    await DisplayAlert("Created", "The thread has been created, but the post wasn't", "Continue");
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send<object, int>(this, "threadCreated", subForumId);
                }
            }
            else
            {
                await DisplayAlert("Creation Error", "We haven't been able to create your Thread !", "Continue");
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