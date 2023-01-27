using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.UserDTO;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Smogon_MAUIapp.Services
{
    public class UserService : InfosAPI
    {
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

        public UserService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        /// <summary>
        /// Method to get all users from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetAllUsers()
        {
            
            return new List<Users>();
        }

        /// <summary>
        /// Method to get one user by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Users> GetUserById(int _id)
        {

            return new Users();
        }

        /// <summary>
        /// Method to create a user
        /// </summary>
        /// <returns></returns>
        public async Task<Users> CreateUser(string _username, string _password, string _email)
        {
            try
            {
                // Create the request body
                var createData = new Dictionary<string, string>
                                            {
                                                { "username", _username },
                                                { "password", _password },
                                                { "email", _email }
                                            };

                var json = JsonConvert.SerializeObject(createData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync("users", content);

                if (response.IsSuccessStatusCode)
                {
                    // Extract the token from the response
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<Users>(responseJson);
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)

            {
                return null;
            }
        }

        /// <summary>
        /// Method to update a user by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Users> UpdateUser(int id, UserDtoUpdate user)
        {
            return new Users();
        }

        /// <summary>
        /// Method to delete a user by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users> DeleteUser(int id)
        {
            return new Users();
        }

        /// <summary>
        /// Method to login User using JWT tokens
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_username"></param>
        /// <param name="_password"></param>
        /// <returns></returns>
        public async Task<JwtSecurityToken> LoginUserJWT(string _username, string _password)
        {
            try
            {
                // Create the request body
                var loginData = new Dictionary<string, string>
                                            {
                                                { "username", _username },
                                                { "password", _password }
                                            };

                var json = JsonConvert.SerializeObject(loginData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync("users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Extract the token from the response
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseJson);
                    var token = new JwtSecurityToken(loginResponse.Token);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)

            {
                return null;
            }
        }

        /// <summary>
        /// Method to get all threads by user ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public async Task<List<Threads>> GetThreadsByUserId(int user_id)
        {
            return new List<Threads>();
        }

        /// <summary>
        /// Method to get all posts by User Id
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public async Task<List<Posts>> GetPostsByUserId(int user_id)
        {
            return new List<Posts>();
        }

    }
}