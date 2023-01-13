using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.PostDTO;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using utils;

namespace Pokemon_Forum_API.Services
{
    public class PostService
    {
        string connectionString = Utils.ConnectionString;

        UserService userService = new UserService();
        ThreadService threadService = new ThreadService();
        public PostService() { }

        /// <summary>
        /// Method to get all posts from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Posts>> GetAllPosts(string connString)
        {
            
            List<Posts> posts = new List<Posts>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM posts", conn))
            {
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        int post_id = reader.GetInt32(0);
                        string content = reader.GetString(1);
                        DateTime create_date = reader.GetDateTime(2);
                        int thread_id = reader.GetInt32(3);
                        int user_id = reader.GetInt32(4);
                        var thread = new Posts(post_id, content, create_date, thread_id, user_id);
                        /*var tempPost = await GetAllLikesByPostId(connString, post_id);
                        thread.likes = tempPost.likes;*/
                        /*thread.user = await userService.GetUserById(connString, user_id);
                        thread.thread = await threadService.GetThreadById(connString, thread_id);*/
                        
                        posts.Add(thread);
                    }
                }

            }
            return posts;
        }

        /// <summary>
        /// Method to get one post by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Posts> GetPostById(string connString, int _id)
        {

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM posts where post_id=@post_id", conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@post_id", _id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        int post_id = reader.GetInt32(0);
                        string content = reader.GetString(1);
                        DateTime create_date = reader.GetDateTime(2);
                        int thread_id = reader.GetInt32(3);
                        int user_id = reader.GetInt32(4);
                        var thread = new Posts(post_id, content, create_date, thread_id, user_id);
                       /* var tempPost = await GetAllLikesByPostId(connString, post_id);
                        thread.likes = tempPost.likes;
                        thread.user = await userService.GetUserById(connString, user_id);
                        thread.thread = await threadService.GetThreadById(connString, thread_id);*/

                        return thread;
                    }
                }

            }

            return new Posts();
        }

        /// <summary>
        /// Method to create a post
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Posts> CreatePost(string connString, PostDto post)
        {
            DateTime now = DateTime.Now;
            try
            {

                string sqlQuery = "INSERT INTO posts (content, create_date, thread_id, user_id) " +
                                                   "VALUES (@content, @create_date, @thread_id, @user_id);";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@content", MySqlDbType.VarChar).Value = post.content;
                        cmd.Parameters.Add("@create_date", MySqlDbType.DateTime).Value = now;
                        cmd.Parameters.Add("@thread_id", MySqlDbType.Int32).Value = post.thread_id;
                        cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = post.user_id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Posts(post.content, post.create_date, post.thread_id, post.user_id);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return new Posts();
            }
        }

        /// <summary>
        /// Method to update a post by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Posts> UpdatePost(string connString, int id, PostDto post)
        {
            var tempPost = await GetPostById(connectionString, id);
            if (tempPost != null)
            {
                try
                {
                    string sqlQuery = "UPDATE posts SET content = @content," +
                                                      " create_date =  @create_date," +
                                                      " thread_id = @thread_id, " +
                                                      " user_id = @user_id " +
                                                      " WHERE post_id = @post_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@content", MySqlDbType.VarChar).Value = post.content;
                        cmd.Parameters.Add("@create_date", MySqlDbType.DateTime).Value = post.create_date;
                        cmd.Parameters.Add("@thread_id", MySqlDbType.Int32).Value = post.thread_id;
                        cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = post.user_id;
                        cmd.Parameters.Add("@post_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Posts(id, post.content, post.create_date, post.thread_id, post.user_id);

                    }
                }
                catch (Exception ex)
                {
                    return new Posts();
                }
            }
            else
            {
                return new Posts();
            }

        }

        /// <summary>
        /// Method to delete a post by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Posts> DeletePost(string connString, int id)
        {

            var tempPost = await GetPostById(connectionString, id);
            if (tempPost != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM posts WHERE post_id = @post_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@post_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempPost;

                    }
                }
                catch (Exception ex)
                {
                    return new Posts();
                }
            }
            else
            {
                return new Posts();
            }
        }



        /// <summary>
        /// Method to get all posts by post ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Posts> GetAllLikesByPostId(string connString, int _id)
        {
            List<Likes> list = new List<Likes>();
            Posts post = await GetPostById(connString, _id);
            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM likes where post_id=@post_id", conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@post_id", _id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        int like_id = reader.GetInt32(0);
                        int post_id = reader.GetInt32(1);
                        int user_id = reader.GetInt32(2);
                        list.Add(new Likes(like_id, post_id, user_id));
                    }
                }

            }
            post.likes = list;

            return post;
        }

    }

}