using Microsoft.Data.SqlClient;
using Pokemon_Forum_API.DTO.PostDTO;
using Pokemon_Forum_API.DTO.UMSFDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class UMSFService
    {
        string connectionString = Tools.Tools.connectionString;

        UserService userService = new UserService();
        SubForumService SubForumService = new SubForumService();
        public UMSFService() { }

        /// <summary>
        /// Method to get all posts from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<User_Moderates_SubForum>> GetAllModerators(string connString)
        {
            
            List<User_Moderates_SubForum> moderators = new List<User_Moderates_SubForum>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM User_Moderates_SubForum", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int UMSF_id = reader.GetInt32(0);
                            int user_id = reader.GetInt32(1);
                            int subforum_id = reader.GetInt32(2);
                            var UMSF = new User_Moderates_SubForum(UMSF_id, user_id, subforum_id);
                            UMSF.user = await userService.GetUserById(connString, user_id);
                            UMSF.subforum = await SubForumService.GetSubForumById(connString, subforum_id);

                            moderators.Add(UMSF);
                        }
                    }

                }
                
                return moderators;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<User_Moderates_SubForum> GetUMSFbyID(string connString, int _id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM User_Moderates_SubForum where UMSF_id=@UMSF_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@UMSF_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int UMSF_id = reader.GetInt32(0);
                            int user_id = reader.GetInt32(1);
                            int subforum_id = reader.GetInt32(2);
                            var UMSF = new User_Moderates_SubForum(UMSF_id, user_id, subforum_id);

                            return UMSF;
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
        /// Method to get all the moderators of a subforum
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetModeratorsBySubforumId(string connString, int _id)
        {
            List<Users> users = new List<Users> {};
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM User_Moderates_SubForum where subforum_id=@subforum_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@subforum_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {                            
                            int user_id = reader.GetInt32(1);
                            Users tempUser = await userService.GetUserById(connString, user_id);
                            users.Add(tempUser);
                        }

                        return users;
                    }

                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get all subforum moderated by one user
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<List<SubForums>> GetsubforumsByModeratorId(string connString, int _id)
        {
            List<SubForums> subForums = new List<SubForums> { };
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM User_Moderates_SubForum where user_id=@user_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@user_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int subforum_id = reader.GetInt32(1);
                            SubForums tempSubForum = await SubForumService.GetSubForumById(connString, subforum_id);
                            subForums.Add(tempSubForum);
                        }

                        return subForums;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Method to create a post
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<User_Moderates_SubForum> CreateModeratorForSubforum(string connString, UMSFDto post)
        {
            DateTime now = DateTime.Now;
            try
            {

                string sqlQuery = "INSERT INTO User_Moderates_SubForum (user_id, subforum_id) " +
                                                   "VALUES (@user_id, @subforum_id);";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = post.user_id;
                        cmd.Parameters.Add("@subforum_id", SqlDbType.Int).Value = post.subforum_id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new User_Moderates_SubForum(post.user_id, post.subforum_id);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return null;
            }
        }

        /// <summary>
        /// Method to delete a moderator by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User_Moderates_SubForum> DeleteModeratorForSubforum(string connString, int id)
        {

            var tempUMSF = await GetUMSFbyID(connectionString, id);
            if (tempUMSF != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM User_Moderates_SubForum WHERE UMSF_id = @UMSF_id;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@UMSF_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempUMSF;

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

    }

}