using Microsoft.Maui.Controls;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class MainPage : ContentPage
{  
    #region Properties

    public string SmogonIntroduction = Tools.LinkText.smogonIntroduction;

    public string GlobalRules = Tools.LinkText.globalRules;

    #endregion

    #region Constructor

    public MainPage()
    {   
        InitializeComponent();        
        ChangeItemSource();
    }

    #endregion

    #region Methods

    private void Rules_Tapped(object sender, EventArgs e)
    {
        DisplayAlert("Global Rules", GlobalRules, "OK");
    }

    private void Intro_Tapped(object sender, EventArgs e)
    {
        DisplayAlert("Introduction to Smogon", SmogonIntroduction, "OK");
    }

    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        Forums forum = viewCell.BindingContext as Forums;
        int id = forum.forum_id;
        await Navigation.PushAsync(new Forum(id));
    }

    private async void ChangeItemSource()
    {
        loadingImage.IsVisible = true;

        TopicService topicService = new TopicService();
        List<Topics> topics = new List<Topics>();
        var aTask = Task.Run(async () =>
        {
            topics = await topicService.GetAllTopics();
        });

        await Task.WhenAll(aTask);

        List<Task> topicsTask = new List<Task>();

        foreach (var item in topics)
        {
            var tempTask = Task.Run(async () =>
            {
                item.forums = await topicService.GetForumsByTopicId(item.topic_id);
            });
            topicsTask.Add(tempTask);
        }

        await Task.WhenAll(topicsTask);

        if (topics != null)
        {            
            loadingImage.IsVisible = false;
            mainPageView.ItemsSource = topics;
        }
    }

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion

    #endregion
}