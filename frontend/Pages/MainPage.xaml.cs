using Microsoft.Maui.Controls;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class MainPage : ContentPage
{  
    #region Properties

    /*private List<Topics> _topics = new List<Topics>
    {
        new Topics("Sports")
        {
            forums = new List<Forums>()
            {
                new Forums("Basketball", "Discussions about basketball")
                {
                    threads = new List<Threads>()
                    {
                        new Threads("Game 1", DateTime.Now, null, 1, null, null),
                        new Threads("Game 2", DateTime.Now, null, 2, null, null)
                    }
                },
                new Forums("Football", "Discussions about football")
                {
                    subforums = new List<SubForums>()
                    {
                        new SubForums("NFL", "Discussion about NFL",1),
                        new SubForums("Soccer", "Discussion about Soccer", 2)
                    }
                }
            }
        },
        new Topics("Technology")
        {
            forums = new List<Forums>()
            {
                new Forums("Computers", "Discussions about computers")
                {
                    subforums = new List<SubForums>()
                    {
                        new SubForums("PCs", "Discussion about PCs",3),
                        new SubForums("Macs", "Discussion about Macs", 4)
                    }
                },
                new Forums("Smartphones", "Discussions about smartphones")
                {
                    subforums = new List<SubForums>()
                    {
                        new SubForums("iOS", "Discussion about iOS",5),
                        new SubForums("Android", "Discussion about Android", 6)
                    }
                }
            }
        }
    };

    public List<Topics> topics
    {
        get => _topics;
        set
        {
            if (topics != value)
            {
                topics = value;
                OnPropertyChanged(); // reports this property
            }
        }
    }*/

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
        TopicService topicService = new TopicService();
        List<Topics> topics = new List<Topics>();
        var aTask = Task.Run(async () =>
        {
            topics = await topicService.GetAllTopics();
        });

        await Task.WhenAll(aTask);

        if (topics != null)
        {
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