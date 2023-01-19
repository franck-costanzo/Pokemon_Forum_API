using Microsoft.IdentityModel.Tokens;
using Smogon_MAUIapp.DTO.UserDTO;
using Smogon_MAUIapp.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Smogon_MAUIapp.Services
{
    public class UserService : InfosAPI
    {
        
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
        /// <param name="connString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Users> CreateUser(UserDtoCreate user)
        {
            return new Users();
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
        public async Task<SecurityToken> LoginUserJWT(string _username, string _password)
        {
            Users user = new Users();


            // create a JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MySecretKeyThatNeedsToBeLonger");  // replace with your secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.user_id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;

 
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