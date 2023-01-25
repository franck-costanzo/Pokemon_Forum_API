namespace Smogon_MAUIapp.Pages;

public partial class CreateThread : ContentPage
{
    //TODO:
    /*
        - implementation de la methode pour sauvegarder
        - aggrémenter le postService pour la création du thread
        - rajouter l'id du post et gérer l'id de l'utilisateur qui créé le thread
     */

    #region Constructor

    public CreateThread()
	{
		InitializeComponent();
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
            //TODO
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