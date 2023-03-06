using Smogon_MAUIapp.DTO.PostDTO;
using Smogon_MAUIapp.Services;
using Smogon_MAUIapp.Tools;
using System.IdentityModel.Tokens.Jwt;

namespace Smogon_MAUIapp.Pages;

public partial class CreatePost : ContentPage
{
    PostService postService = new PostService();
    private int threadId = 0;

    //TODO:
    /*
        - implementation de la methode pour sauvegarder
        - aggrémenter le postService pour la création du post
        - rajouter l'id du post et gérer l'id de l'utilisateur qui créé le post
     */

    #region Constructor

    public CreatePost(int id)
	{
		InitializeComponent();
        this.threadId = id;
    }

    #endregion

    #region Methods
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
            //var token = new JwtSecurityToken(Preferences.Get("token", ""));
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

            var post = await postService.CreatePost(postDto, jwtToken);

            if (post != null)
            {
                await DisplayAlert("Created", "Your post has been created", "Continue");
                /// TODO :
                /// - fermer le modal
                /// - niquer des memes
                /// - reload les datas dans la page !
                
            }
            else
            {
                await DisplayAlert("Creation Error", "We weren't able to create your post !", "Continue");
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