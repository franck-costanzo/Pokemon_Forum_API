using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.ComponentModel;

namespace Smogon_MAUIapp.Pages;

public partial class MainPage : ContentPage
{
    /*public static readonly BindableProperty TopicsProperty =
        BindableProperty.Create(nameof(Topics), typeof(List<Topics>), typeof(MainPage));

    public List<Topics> topics
    {
        get => (List<Topics>)GetValue(TopicsProperty);
        set => SetValue(TopicsProperty, value);
    }*/

    BackgroundWorker pedro = new BackgroundWorker();

    List<Topics> topics = new List<Topics>();

    public MainPage()
    {
        InitializeComponent();

        pedro.DoWork += Pedro_DoWork;

        pedro.RunWorkerCompleted += Pedro_RunWorkerCompleted;

        pedro.RunWorkerAsync();
        
    }

    private void Pedro_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        foreach (var topic in topics)
        {

            TableView table = new TableView();

            table.Margin = new Thickness(10, 10, 10, 10);

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

    private async void Pedro_DoWork(object sender, DoWorkEventArgs e)
    {
        TopicService service = new TopicService();

        try
        {
            Dictionary<int, Task<List<Forums>>> taskDictionary = new Dictionary<int, Task<List<Forums>>>();

            topics = await service.GetAllTopics();

            foreach (var item in topics)
            {
                taskDictionary.Add(item.topic_id, Task.Run(() => service.GetForumsByTopicId(item.topic_id)));
            }
            Task.WhenAll(taskDictionary.Values).Wait();

            foreach (var topic in topics)
            {
                topic.forums = taskDictionary[topic.topic_id].Result;
            }
        }
        catch
        {

        }

    }

}