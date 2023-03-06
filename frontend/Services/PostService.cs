using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smogon_MAUIapp.DTO.PostDTO;
using Smogon_MAUIapp.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Smogon_MAUIapp.Tools;

namespace Smogon_MAUIapp.Services
{
    public class PostService : InfosAPI
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

        public PostService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        /// <summary>
        /// Method to get all posts from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Posts>> GetAllPosts()
        {
            return new List<Posts>();
        }

        /// <summary>
        /// Method to get one post by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Posts> GetPostById(int _id)
        {
            var json = await client.GetStringAsync($"posts/{_id}");
            var post = JsonConvert.DeserializeObject<Posts>(json);

            return post;
        }

        /// <summary>
        /// Method to create a post
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Posts> CreatePost(PostDto post, JwtSecurityToken jwtToken)
        {
            var json = JsonConvert.SerializeObject(post);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Verify JWT token
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Jwtools.Issuer,
                ValidAudience = Jwtools.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtools.Key))
            };

            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwtToken.RawData, tokenValidationParameters, out var validatedToken);
            }
            catch (SecurityTokenException)
            {
                // Token validation failed
                throw new Exception("Invalid JWT token.");
            }

            // Add JWT token to Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            var response = await client.PostAsync($"posts", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdPost = JsonConvert.DeserializeObject<Posts>(responseJson);
            return createdPost;
        }

        /// <summary>
        /// Method to update a post by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Posts> UpdatePost(int id, PostDto post)
        {
            return new Posts();
        }

        /// <summary>
        /// Method to delete a post by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Posts> DeletePost(int id)
        {
            return new Posts();
        }



        /// <summary>
        /// Method to get all posts by post ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Posts> GetAllLikesByPostId(int _id)
        {
            return new Posts();
        }

    }

}