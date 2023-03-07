using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using Smogon_MAUIapp.Tools;
using System;
using System.ComponentModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace Smogon_MAUIapp.Pages;

public partial class Profile : ContentPage
{
    #region Properties

    Users user = new Users();
    UserService userService = new UserService();
    TeamService teamService = new TeamService();
    int user_id = 0;
    #endregion

    #region Constructor

    public Profile()
    {
        
        InitializeComponent();

        MessagingCenter.Subscribe<object, int>(this, "PostCreated", (sender, user_id) =>
        {
            UpdateRowSource(user_id);
        });

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");
        this.user_id = Int32.Parse(userIdClaim.Value);
        UpdateRowSource(user_id);

    }

    #endregion

    #region Methods

    private async void UpdateRowSource(int user_id)
    {
        this.user = await userService.GetLast3PostsAndLast5TeamsByUserId(user_id);
        this.BindingContext = this.user;
    }

    private async void TeamTapped(object sender, EventArgs e)
    {
        var button = (Label)sender;
        var team = (Teams)button.BindingContext;
        var link = team.link;
        await Navigation.PushModalAsync(new TeamView(link));
    }


    #endregion

    private async void AddATeam(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var user = (Users)button.BindingContext;
        var userId = user.user_id;
        await Navigation.PushModalAsync(new CreateTeam(userId));
    }

    private async void DeleteATeam(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var team = (Teams)button.BindingContext;
        var teamId = team.team_id;
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");
        bool answer = await DisplayAlert("Question?", "Do you really want to delete your team ?", "Yes", "No");
        if (answer)
        {
            await teamService.DeleteTeam(teamId, jwtToken);
            UpdateRowSource(user_id);
        }
    }
}