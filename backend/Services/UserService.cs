using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.UserDTO;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Tools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class UserService
    {
        string connectionString = Tools.Tools.connectionString;
        public UserService() {}

        /// <summary>
        /// Method to get all users from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetAllUsers(string connString)
        {
            
            List<Users> users = new List<Users>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                    
                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            DateTime join_date = reader.GetDateTime(5);
                            string avatar_url = reader.IsDBNull(4) ? "no avatar" : reader.GetString(5);
                            bool isBanned = reader.GetBoolean(6);
                            int role_id = reader.GetInt32(7);                            
                            users.Add(new Users(id, username, "Password is encrypted", email, join_date, avatar_url,  role_id, isBanned));
                        }
                    }
                
                }
                return users;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get one user by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Users> GetUserById(string connString, int _id)
        {

            Users user = new Users();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users where user_id=@user_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string avatar_url = reader.IsDBNull(4)? "no avatar" : reader.GetString(4);
                            DateTime join_date = reader.GetDateTime(5);
                            bool isBanned = reader.GetBoolean(6);
                            int role_id = reader.GetInt32(7);
                            return user = new Users(id, username, password, email, join_date, avatar_url, role_id, isBanned);
                        }
                    }

                }

                return user;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get one user by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Users> GetUserByName(string connString, string name)
        {

            Users user = new Users();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users where username=@username", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@username", name);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            DateTime join_date = reader.GetDateTime(4);
                            string avatar_url = reader.GetString(5);
                            bool isBanned = reader.GetBoolean(6);
                            int role_id = reader.GetInt32(7);
                            return user = new Users(id, username, password, email, join_date, avatar_url, role_id, isBanned);
                        }
                    }

                }

                return user;

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        /// <summary>
        /// Method to get one user by his email from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Users> GetUserByEmail(string connString, string email)
        {

            Users user = new Users();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users where email=@email", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@email", email);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string _email = reader.GetString(3);
                            DateTime join_date = reader.GetDateTime(4);
                            string avatar_url = reader.GetString(5);
                            bool isBanned = reader.GetBoolean(6);
                            int role_id = reader.GetInt32(7);
                            return user = new Users(id, username, password, _email, join_date, avatar_url, role_id, isBanned);
                        }
                    }

                }

                return user;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to create a user
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Users> CreateUser(string connString, UserDtoCreate user)
        {
            try
            {
                if (!Tools.Tools.IsValidEmail(user.email)) throw new Exception();
                var checkedUser = await GetUserByName(connString, user.username);
                var secondCheck = await GetUserByEmail(connString, user.email);

                if (checkedUser.username == null && secondCheck.username == null)
                {
                    string sqlQuery = "INSERT INTO Users(username, password, email, join_date, role_id, isBanned) VALUES(@username, @password, @email, @join_date, @role_id, @isBanned);";
                    DateTime now = DateTime.Now;
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        await conn.OpenAsync();
                        using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                        {
                            cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = user.username;
                            cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = BCrypt.Net.BCrypt.HashPassword(user.password);
                            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = user.email;
                            cmd.Parameters.Add("@join_date", MySqlDbType.DateTime).Value = now;
                            cmd.Parameters.Add("@role_id", MySqlDbType.Int32).Value = 3;
                            cmd.Parameters.Add("@isBanned", MySqlDbType.Bit).Value = false;

                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                    return new Users(user.username, user.email, now, 3, false);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                // Handle other exceptions
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
        public async Task<Users> UpdateUser(string connString, int id, UserDtoUpdate user)
        {
            var tempUser = await GetUserById(connectionString, id);
            if (tempUser != null && BCrypt.Net.BCrypt.Verify(user.oldPassword, tempUser.password))
            {
                try
                {
                    string sqlQuery = "UPDATE users SET username = @username," +
                                                      " password =  @password," +
                                                      " email = @email" +
                                                      " WHERE user_id = @user_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = user.username;
                        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = BCrypt.Net.BCrypt.HashPassword(user.password);
                        cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = user.email;
                        cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Users(id, user.username, user.email);

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Update user avatar
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Users> UpdateUserAvatar(string connString, int id, UserDtoUpdateAvatar user)
        {
            
            try
            {
                string sqlQuery = "UPDATE users SET avatar_url = @avatar_url" +
                                                    " WHERE user_id = @user_id;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.Add("@avatar_url", MySqlDbType.VarChar).Value = user.avatar_url;
                    cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = id;
                    await cmd.ExecuteNonQueryAsync();
                    return new Users(id, user.avatar_url);

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Method to delete a user by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users> DeleteUser(string connString, int id)
        {

            var tempUser = await GetUserById(connectionString, id);
            if (tempUser != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM users WHERE user_id = @user_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@user_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempUser;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to login User using JWT tokens
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_username"></param>
        /// <param name="_password"></param>
        /// <returns></returns>
        public async Task<SecurityToken> LoginUserJWT(string connString, string _username, string _password)
        {
            Users user = new Users();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users where username=@username", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@username", _username);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            DateTime join_date = reader.GetDateTime(5);
                            string avatar_url = reader.IsDBNull(4)? "www.exemple.fr/imagedefouf" : reader.GetString(5);
                            bool isBanned = reader.GetBoolean(6);
                            int role_id = reader.GetInt32(7);

                            if (BCrypt.Net.BCrypt.Verify(_password, password))
                            {
                                user = new Users(id, username, password, email, join_date, avatar_url, role_id, isBanned);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }

                if (user.user_id != 0)
                {
                    //Getting the key from app setting
                    IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

                    var configuration = builder.Build();

                    JwtSettings jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

                    var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

                    //Token shaping
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim("User_id", user.user_id.ToString()),
                                new Claim("Role_id", user.role_id.ToString())
                            }),
                        Expires = DateTime.UtcNow.AddDays(90),
                        SigningCredentials = 
                        new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature ),
                        Issuer = jwtSettings.Issuer,
                        Audience = jwtSettings.Audience
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

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
        public async Task<List<Threads>> GetThreadsByUserId(string connString, int user_id)
        {
            try
            {

                List<Threads> threads = new List<Threads>();


                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Threads WHERE user_id = @user_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int thread_id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            DateTime last_post_date = reader.GetDateTime(3);
                            int userId = reader.GetInt32(4);
                            int subforum_id =  reader.GetInt32(5);
                            threads.Add(new Threads(thread_id, title, create_date, last_post_date, userId, subforum_id));
                        }
                    }
                }
                return threads;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get all posts by User Id
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public async Task<List<Posts>> GetPostsByUserId(string connString, int user_id)
        {
            try
            {

                List<Posts> posts = new List<Posts>();

                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Posts WHERE user_id = @user_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int post_id = reader.GetInt32(0);
                            string content = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            int thread_id = reader.GetInt32(3);
                            int userId = reader.GetInt32(4);
                            posts.Add(new Posts(post_id, content, create_date, thread_id, userId));
                        }
                    }
                }
                return posts;
            }
            catch (Exception ex)
            { 
                return null; 
            }
        }

        public async Task<Users> GetLast3PostsAndLast5TeamsByUserId(string connString, int user_id)
        {
            try
            {
                Users user = await GetUserById(connString, user_id);

                List<Teams> teams = new List<Teams>();
                List<Posts> posts = new List<Posts>();

                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand(
                                                "SELECT * " +
                                                "FROM Posts " +
                                                "WHERE user_id = @user_id " +
                                                "ORDER BY create_date desc limit 3", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int post_id = reader.GetInt32(0);
                            string content = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            int thread_id = reader.GetInt32(3);
                            int userId = reader.GetInt32(4);
                            posts.Add(new Posts(post_id, content, create_date, thread_id, userId));
                        }
                    }
                }

                user.posts = posts;

                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand(
                                                "SELECT *" +
                                                " FROM Teams " +
                                                "WHERE user_id = @user_id " +
                                                " ORDER BY date_created desc ", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int team_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string link = reader.GetString(2);
                            DateTime create_date = reader.GetDateTime(3);
                            int teamuser_id = reader.GetInt32(4);
                            teams.Add(new Teams(team_id, name, link, create_date, teamuser_id));
                        }
                    }
                }

                user.teams = teams;

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}