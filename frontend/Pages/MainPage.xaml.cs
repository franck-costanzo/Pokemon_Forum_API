using Microsoft.Maui.Controls;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class MainPage : ContentPage
{
    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion

    private List<Topics> _topics;

    public List<Topics> __topics
    {
        get => _topics;
        set
        {
            if (_topics != value)
            {
                _topics = value;
                OnPropertyChanged(); // reports this property
            }
        }
    }
    private List<Topics> topics = new List<Topics>();

    public MainPage()
    {   
        InitializeComponent();
        //LoadAfterConstruction();
    }
    private async void LoadAfterConstruction()
    {
        TopicService service = new TopicService();
        topics = await service.GetAllTopics();
        foreach (var topic in topics)
        {

            TableView table = new TableView();

            table.Margin = new Thickness(20, 20, 20, 20);

            layoutView.Children.Add(table);

            TableRoot root = new TableRoot();

            table.Root = root;

            TableSection section = new TableSection();
            section.Title = topic.name;
            section.TextColor = Color.FromRgba("#FFFFFF");

            root.Add(section);

            foreach (var forum in topic.forums)
            {
                TextCell cell = new TextCell();
                cell.Text = forum.name;
                cell.TextColor = Color.FromRgba("#000000");
                cell.Detail = forum.description;
                cell.DetailColor = Color.FromRgba("#ACACAC");
                section.Add(cell);
            }

        }
    }



}