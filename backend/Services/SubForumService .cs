using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.SubForumDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class SubForumService
    {
        string connectionString = Tools.Tools.connectionString;

        UserService userService = new UserService();
        ForumService forumService = new ForumService();
        public SubForumService() { }

        /// <summary>
        /// Method to get all subForums from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<SubForums>> GetAllSubForums(string connString)
        {
            try
            {
                List<SubForums> subForums = new List<SubForums>();

                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM subforums", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int subForum_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            int forum_id = reader.GetInt32(3);
                            var subForum = new SubForums(subForum_id, name, description, forum_id);
                            /*var tempSubForum = await GetAllThreadsBySubForumId(connString, subForum_id);
                            subForum.threads = tempSubForum.threads;
                            subForum.forum = await forumService.GetForumById(connString, forum_id);*/
                            subForums.Add(subForum);
                        }
                    }

                }
                return subForums;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get one subForum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<SubForums> GetSubForumById(string connString, int _id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM subforums where subforum_id=@subforum_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@subForum_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int subForum_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            int forum_id = reader.GetInt32(3);
                            var subForum = new SubForums(subForum_id, name, description, forum_id);
                           /* var tempSubForum = await GetAllThreadsBySubForumId(connString, subForum_id);
                            subForum.threads = tempSubForum.threads;
                            subForum.forum = await forumService.GetForumById(connString, forum_id);*/
                            return subForum;
                        }
                    }

                }

            }
            catch(Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// Method to create a subForum
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="subForum"></param>
        /// <returns></returns>
        public async Task<SubForums> CreateSubForum(string connString, SubForumDto subForum)
        {
            try
            {

                string sqlQuery = "INSERT INTO subforums (name, description, forum_id) " +
                                                   "VALUES (@name, @description, @forum_id);";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = subForum.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = subForum.description;
                        cmd.Parameters.Add("@forum_id", MySqlDbType.Int32).Value = subForum.forum_id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new SubForums(subForum.name, subForum.description, subForum.forum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to update a subForum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="subForum"></param>
        /// <returns></returns>
        public async Task<SubForums> UpdateSubForum(string connString, int id, SubForumDto subForum)
        {
            var tempSubForum = await GetSubForumById(connectionString, id);
            if (tempSubForum != null)
            {
                try
                {
                    string sqlQuery = "UPDATE subforums SET name = @name," +
                                                      " description =  @description," +
                                                      " forum_id = @forum_id " +
                                                      " WHERE subforum_id = @subforum_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = subForum.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = subForum.description;
                        cmd.Parameters.Add("@forum_id", MySqlDbType.Int32).Value = subForum.forum_id;
                        cmd.Parameters.Add("@subforum_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new SubForums(id, subForum.name, subForum.description, subForum.forum_id);

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
        /// Method to delete a subForum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SubForums> DeleteSubForum(string connString, int id)
        {

            var tempSubForum = await GetSubForumById(connectionString, id);
            if (tempSubForum != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM subForums WHERE subForum_id = @subForum_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@subForum_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempSubForum;

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
        /// Method to get one subForum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<SubForums> GetAllThreadsBySubForumId(string connString, int _id)
        {
            try
            {
                List<Threads> list = new List<Threads>();
                SubForums subForum = await GetSubForumById(connString, _id);
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM threads where subforum_id=@subforum_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@subForum_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int thread_id = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            DateTime last_post_date = reader.GetDateTime(3);
                            int user_id = reader.GetInt32(4);
                            int subForum_id = reader.GetInt32(5);
                            list.Add(new Threads(thread_id, title, create_date, last_post_date, user_id, subForum_id));
                        }
                    }

                }
                subForum.threads = list;

                return subForum;
            }
            catch(Exception ex)
            {
                return null;
            }

            return null;
        }
    }
}