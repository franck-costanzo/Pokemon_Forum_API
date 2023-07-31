using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.TeamDTO;
using Smogon_MAUIapp.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Smogon_MAUIapp.Services
{
    internal class TeamService : InfosAPI
    {
        #region Properties

        public string url
        {
            get
            {
                return base.baseUrl;
            }
            set
            {
                base.baseUrl = value;
            }
        }

        private HttpClient client;

        #endregion


        #region Constructor

        public TeamService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to create a team
        /// </summary>
        /// <param name="team"></param>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public async Task<Teams> CreateTeam(TeamDto team, JwtSecurityToken jwtToken)
        {
            var json = JsonConvert.SerializeObject(team);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add JWT token to Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            var response = await client.PostAsync($"teams", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdTeam = JsonConvert.DeserializeObject<Teams>(responseJson);
            return createdTeam;
        }

        /// <summary>
        /// Method to delete a team
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public async Task<Teams> DeleteTeam(int id, JwtSecurityToken jwtToken)
        {
            // Add JWT token to Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            var response = await client.DeleteAsync($"teams/{id}");
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var deletedTeam = JsonConvert.DeserializeObject<Teams>(responseJson);
            return deletedTeam;
        }

        #endregion


    }
}
