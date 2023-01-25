namespace Smogon_MAUIapp.Pages;

public partial class CreatePost : ContentPage
{
    public string MarkdownText { get; set; }
	
    public CreatePost()
	{
		InitializeComponent();
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        
    }
    private void BoldButton_Clicked(object sender, EventArgs e)
    {
        int length = MarkdownEditor.SelectionLength;
        int cursor = MarkdownEditor.CursorPosition;
        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor, "**");

        MarkdownEditor.Text = MarkdownEditor.Text.Insert(cursor + length +2, "**");
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
}