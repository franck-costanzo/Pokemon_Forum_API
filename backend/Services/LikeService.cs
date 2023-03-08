using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.LikeDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class LikeService
    {
        string connectionString = Tools.Tools.connectionString;

        UserService userService = new UserService();
        PostService postService = new PostService();
        public LikeService() {}

        /// <summary>
        /// Method to get one like by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Likes> GetLikeByPostIdAndUserId(string connString, string postAndUserID)
        {
            string[] Ids = postAndUserID.Split("-");
            int post_id = Int32.Parse(Ids[0]);
            int user_id = Int32.Parse(Ids[1]);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM likes " +
                                                           "where post_id=@post_id and user_id=@user_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@post_id", post_id);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int like_id = reader.GetInt32(0);
                            int _post_id = reader.GetInt32(1);
                            int _user_id = reader.GetInt32(2);
                            var like = new Likes(like_id, post_id, user_id);

                            return like;
                        }
                    }

                }

                return new Likes();

            }
            catch(Exception ex)
            {
                return null;
            }

            return null;
        }



        /// <summary>
        /// Method to create a like
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        public async Task<Likes> CreateLike(string connString, LikeDto like)
        {
            try
            {

                string sqlQuery = "INSERT INTO likes (post_id, user_id) " +
                                                   "VALUES (@post_id, @user_id);";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@post_id", MySqlDbType.Int32).Value = like.post_id;
                        cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = like.user_id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Likes(like.post_id, like.user_id);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return null;
            }
        }


        /// <summary>
        /// Method to delete a like by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Likes> DeleteLike(string connString, LikeDto likeDto)
        {
            try
            {
                string sqlQuery = "DELETE FROM likes WHERE user_id = @user_id AND post_id=@post_id;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", likeDto.user_id); 
                    cmd.Parameters.AddWithValue("@post_id", likeDto.post_id);
                    var like = await cmd.ExecuteNonQueryAsync();
                    return new Likes();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }

}