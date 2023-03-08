using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smogon_MAUIapp.DTO.LikeDTO;
using Smogon_MAUIapp.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace Smogon_MAUIapp.Services
{
    public class LikeService : InfosAPI
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

        public LikeService() 
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        /// <summary>
        /// Method to get one like by his ID from DB
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Likes> GetLikeByPostIdAndUserId(string postAndUserId)
        {            
            var json = await client.GetStringAsync($"likes/postanduser/{postAndUserId}");
            var like = JsonConvert.DeserializeObject<Likes>(json);
            return like;
        }

        /// <summary>
        /// Method to create a like
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        public async Task<Likes> CreateLike(LikeDto like, JwtSecurityToken jwtToken)
        {
            var json = JsonConvert.SerializeObject(like);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add JWT token to Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            var response = await client.PostAsync($"likes", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdLike = JsonConvert.DeserializeObject<Likes>(responseJson);
            return createdLike;
        }


        /// <summary>
        /// Method to delete a like by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Likes> DeleteLike(LikeDto like, JwtSecurityToken jwtToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            var json = JsonConvert.SerializeObject(like);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"likes/delete", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var deletedLike = JsonConvert.DeserializeObject<Likes>(responseJson);
            return deletedLike;
        }


    }

}