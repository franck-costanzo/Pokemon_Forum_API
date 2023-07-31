using Microsoft.IdentityModel.Tokens;
using Smogon_MAUIapp.DTO.PostDTO;
using Smogon_MAUIapp.DTO.TeamDTO;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;
using Smogon_MAUIapp.Tools;
using System.IdentityModel.Tokens.Jwt;

namespace Smogon_MAUIapp.Pages;

public partial class CreateTeam : ContentPage
{
    #region Properties

    TeamService teamService = new TeamService();
    private int user_Id = 0;

    #endregion

    #region Constructor

    public CreateTeam(int id)
	{
		InitializeComponent();
        this.user_Id = id;
    }

    #endregion

    #region Methods

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string substring = "https://pokepast.es/";
        if (String.IsNullOrEmpty(teamname.Text) || String.IsNullOrEmpty(link.Text))
        {
            await DisplayAlert("Team", "You need to fill both field to save your team", "OK");
        }
        else if (!link.Text.StartsWith(substring))
        {
            await DisplayAlert("Team", "You need to enter a pokepaste address !", "OK");
        }
        else
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(Preferences.Get("token", ""));
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "User_id");
            TeamDto teamDto = new TeamDto();

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                teamDto.name = teamname.Text;
                teamDto.link = link.Text;
                teamDto.user_id = userId;
            }

            var team = await teamService.CreateTeam(teamDto, jwtToken);

            

            if (team != null)
            {
                await DisplayAlert("Created", "Your team has been created", "Continue");

                await Navigation.PopModalAsync();
                MessagingCenter.Send<object, int>(this, "PostCreated", user_Id);
            }
            else
            {
                await DisplayAlert("Creation Error", "We haven't been able to create your team !", "Continue");
            }
        }
    }

    private async void PokepasteTapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PokePaste());
    }

    #endregion
}