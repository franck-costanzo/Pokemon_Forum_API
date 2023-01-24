using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class Profile : ContentPage
{
    #region Properties

    public Users user
    {
        get => _user;
        set
        {
            if (_user != value)
            {
                _user = value;
                OnPropertyChanged();
            }
        }
    }

    private Users _user = new Users(
        0, "Domo-Kun", "domokun@smogon.io", DateTime.Now, 3, false,
        new List<Posts> {
            new Posts(1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                          "Donec fringilla ligula eget odio interdum auctor. " +
                          "Phasellus dignissim metus ut urna vestibulum, nec imperdiet sem auctor. " +
                          "Donec felis est, tincidunt quis libero vel, scelerisque pharetra sem. " +
                          "Cras sed suscipit tortor, quis viverra dolor. Donec malesuada vehicula tempor. " +
                          "Duis consectetur erat nec feugiat vehicula. Aliquam sed maximus turpis, " +
                          "a lobortis orci.", DateTime.Now, 1, 0),
            new Posts(13, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                          "Donec fringilla ligula eget odio interdum auctor. " +
                          "Phasellus dignissim metus ut urna vestibulum, nec imperdiet sem auctor. " +
                          "Donec felis est, tincidunt quis libero vel, scelerisque pharetra sem. " +
                          "Cras sed suscipit tortor, quis viverra dolor. Donec malesuada vehicula tempor. " +
                          "Duis consectetur erat nec feugiat vehicula. Aliquam sed maximus turpis, " +
                          "a lobortis orci.", DateTime.Now, 12, 0),
            new Posts(14, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                          "Donec fringilla ligula eget odio interdum auctor. " +
                          "Phasellus dignissim metus ut urna vestibulum, nec imperdiet sem auctor. " +
                          "Donec felis est, tincidunt quis libero vel, scelerisque pharetra sem. " +
                          "Cras sed suscipit tortor, quis viverra dolor. Donec malesuada vehicula tempor. " +
                          "Duis consectetur erat nec feugiat vehicula. Aliquam sed maximus turpis, " +
                          "a lobortis orci.", DateTime.Now, 25, 0) }
        );

    #endregion

    #region Constructor

    public Profile()
	{
		InitializeComponent();
        this.BindingContext = this;
    }

    #endregion

    #region Methods
    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        Posts post = viewCell.BindingContext as Posts;
        int id = post.post_id;
        await Navigation.PushAsync(new Thread(id, true, false));
    }

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion    

    #endregion
}