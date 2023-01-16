using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.ThreadDTO;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using utils;

namespace Pokemon_Forum_API.Services
{
    public class ThreadService
    {
        string connectionString = Utils.ConnectionString;

        UserService userService = new UserService();
        ForumService forumService = new ForumService();
        SubForumService subForumService = new SubForumService();
        public ThreadService() { }

        /// <summary>
        /// Method to get all threads from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Threads>> GetAllThreads(string connString)
        {
            
            List<Threads> threads = new List<Threads>();

            try
            {

                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM threads", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int thread_id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            DateTime? last_post_date = reader.IsDBNull(3) ? null : reader.GetDateTime(3);
                            int user_id = reader.GetInt32(4);
                            int? forum_id = reader.IsDBNull(5) ? null : reader.GetInt32(5);
                            int? subforum_id = reader.IsDBNull(6) ? null : reader.GetInt32(6);
                            var thread = new Threads(thread_id, title, create_date, last_post_date, user_id, forum_id, subforum_id);
                            /*var tempThread = await GetAllPostsByThreadId(connString, thread_id);
                            thread.posts = tempThread.posts;
                            thread.user = await userService.GetUserById(connString, user_id);*/
                            /*if(forum_id!= null)
                            {
                                thread.forum = await forumService.GetForumById(connString, (int)forum_id);
                            }
                            if(subforum_id != null)
                            {
                                thread.subforum = await subForumService.GetSubForumById(connString, (int)subforum_id);
                            }*/
                            threads.Add(thread);
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
        /// Method to get one thread by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Threads> GetThreadById(string connString, int _id)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM threads where thread_id=@thread_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@thread_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int thread_id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            DateTime? last_post_date = reader.IsDBNull(3) ? null : reader.GetDateTime(3);
                            int user_id = reader.GetInt32(4);
                            int? forum_id = reader.IsDBNull(5) ? null : reader.GetInt32(5);
                            int? subforum_id = reader.IsDBNull(6) ? null : reader.GetInt32(6);
                            var thread = new Threads(thread_id, title, create_date, last_post_date, user_id, forum_id, subforum_id);
                            /*var tempThread = await GetAllPostsByThreadId(connString, thread_id);
                            thread.posts = tempThread.posts;
                            thread.user = await userService.GetUserById(connString, user_id);
                            if (forum_id != null)
                            {
                                thread.forum = await forumService.GetForumById(connString, (int)forum_id);
                            }
                            if (subforum_id != null)
                            {
                                thread.subforum = await subForumService.GetSubForumById(connString, (int)subforum_id);
                            }*/
                            return thread;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// Method to create a thread
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public async Task<Threads> CreateThread(string connString, ThreadDto thread)
        {
            DateTime now = DateTime.Now;
            try
            {

                string sqlQuery = "INSERT INTO threads (title, create_date, last_post_date, " +
                                                            "user_id, forum_id, subforum_id) " +
                                                   "VALUES (@title, @create_date, @last_post_date," +
                                                             "@user_id, @forum_id, @subforum_id);";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@title", MySqlDbType.VarChar).Value = thread.title;
                        cmd.Parameters.Add("@create_date", MySqlDbType.DateTime).Value = now;
                        cmd.Parameters.Add("@last_post_date", MySqlDbType.DateTime).Value = DBNull.Value;
                        cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = thread.user_id;
                        cmd.Parameters.Add("@forum_id", MySqlDbType.Int32).Value = thread.forum_id;
                        cmd.Parameters.Add("@subforum_id", MySqlDbType.Int32).Value = thread.subforum_id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Threads(thread.title, thread.create_date, thread.last_post_date,
                                    thread.user_id, thread.forum_id, thread.subforum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to update a thread by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public async Task<Threads> UpdateThread(string connString, int id, ThreadDto thread)
        {
            var tempThread = await GetThreadById(connectionString, id);
            if (tempThread != null)
            {
                try
                {
                    string sqlQuery = "UPDATE threads SET title = @title," +
                                                      " create_date =  @create_date," +
                                                      " last_post_date =  @last_post_date," +
                                                      " user_id = @user_id, " +
                                                      " forum_id = @forum_id, " +
                                                      " subforum_id = @subforum_id " +
                                                      " WHERE thread_id = @thread_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@title", MySqlDbType.VarChar).Value = thread.title;
                        cmd.Parameters.Add("@create_date", MySqlDbType.DateTime).Value = thread.create_date;
                        cmd.Parameters.Add("@last_post_date", MySqlDbType.DateTime).Value = thread.last_post_date;
                        cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = thread.user_id;
                        cmd.Parameters.Add("@forum_id", MySqlDbType.Int32).Value = thread.forum_id;
                        cmd.Parameters.Add("@subforum_id", MySqlDbType.Int32).Value = thread.subforum_id;
                        cmd.Parameters.Add("@thread_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Threads(id, thread.title, thread.create_date, thread.last_post_date,
                                                thread.user_id, thread.forum_id, thread.subforum_id);

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
        /// Method to delete a thread by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Threads> DeleteThread(string connString, int id)
        {

            var tempThread = await GetThreadById(connectionString, id);
            if (tempThread != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM threads WHERE thread_id = @thread_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@thread_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempThread;

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
        /// Method to get all posts by thread ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Threads> GetAllPostsByThreadId(string connString, int _id)
        {
            try
            {
                List<Posts> list = new List<Posts>();
                Threads thread = await GetThreadById(connString, _id);
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM posts where thread_id=@thread_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@thread_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int post_id = reader.GetInt32(0);
                            string content = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            int thread_id = reader.GetInt32(3);
                            int user_id = reader.GetInt32(4);
                            list.Add(new Posts(post_id, content, create_date, thread_id, user_id));
                        }
                    }

                }
                thread.posts = list;

                return thread;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}