using Microsoft.Maui.Dispatching;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using System.ComponentModel;

namespace Smogon_MAUIapp.Pages;

public partial class Loading : ContentPage
{
    BackgroundWorker pedro = new BackgroundWorker();

    List<Topics> topics = new List<Topics>();

	public Loading()
	{
		InitializeComponent();

        pedro.DoWork += Pedro_DoWork;

        pedro.RunWorkerCompleted += Pedro_RunWorkerCompleted;

        pedro.RunWorkerAsync();
        
    }

    private void Pedro_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        Application.Current.MainPage = new AppShell();
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