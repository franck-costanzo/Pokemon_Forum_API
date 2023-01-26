using Microsoft.Data.SqlClient;
using Pokemon_Forum_API.DTO.BannedUserDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class BannedUserService
    {
        string connectionString = Tools.Tools.connectionString;
        UserService userService = new UserService();
        public BannedUserService() { }

        /// <summary>
        /// Method to get all bannedUsers from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<BannedUsers>> GetAllBannedUsers(string connString)
        {
            try
            {
                List<BannedUsers> bannedUsers = new List<BannedUsers>();

                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM bannedusers", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int banned_user_id = reader.GetInt32(0);
                            int user_id = reader.GetInt32(1);
                            int banned_by_user_id = reader.GetInt32(2);
                            DateTime ban_start_date = reader.GetDateTime(3);
                            DateTime ban_end_date = reader.GetDateTime(4);
                            string reason = reader.GetString(5);
                            var tempUser = new BannedUsers(banned_user_id, user_id, banned_by_user_id, ban_start_date, ban_end_date, reason);
                            bannedUsers.Add(tempUser);
                        }
                    }

                }
                return bannedUsers;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Method to get one bannedUser by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<BannedUsers> GetBannedUserById(string connString, int _id)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM bannedusers where banned_user_id=@banned_user_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@banned_user_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int banned_user_id = reader.GetInt32(0);
                            int user_id = reader.GetInt32(1);
                            int banned_by_user_id = reader.GetInt32(2);
                            DateTime ban_start_date = reader.GetDateTime(3);
                            DateTime ban_end_date = reader.GetDateTime(4);
                            string reason = reader.GetString(5);
                            var tempUser = new BannedUsers(banned_user_id, user_id, banned_by_user_id, ban_start_date, ban_end_date, reason);
                            tempUser.user = await userService.GetUserById(connectionString, user_id);
                            tempUser.user.password = "Password way encrypted";
                            tempUser.bannedbyuser = await userService.GetUserById(connectionString, banned_by_user_id);
                            tempUser.bannedbyuser.password = "Password way encrypted";
                            return tempUser;
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
        /// Method to create a bannedUser
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="bannedUser"></param>
        /// <returns></returns>
        public async Task<BannedUsers> CreateBannedUser(string connString, BannedUserDto bannedUser)
        {
            try
            {

                string sqlQuery = "INSERT INTO BannedUsers (user_id, banned_by_user_id, ban_start_date, ban_end_date, reason) " +
                                                   "VALUES (@user_id, @banned_by_user_id, @ban_start_date, @ban_end_date, @reason);";
                DateTime now = DateTime.Now;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = bannedUser.user_id;
                        cmd.Parameters.Add("@banned_by_user_id", SqlDbType.Int).Value = bannedUser.banned_by_user_id;
                        cmd.Parameters.Add("@ban_start_date", SqlDbType.DateTime).Value = now;
                        cmd.Parameters.Add("@ban_end_date", SqlDbType.DateTime).Value = bannedUser.ban_end_date;
                        cmd.Parameters.Add("@reason", SqlDbType.VarChar).Value = bannedUser.reason;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new BannedUsers(bannedUser.user_id, bannedUser.banned_by_user_id,
                                        bannedUser.ban_start_date, bannedUser.ban_end_date, bannedUser.reason);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return null; 
            }
        }

        /// <summary>
        /// Method to update a bannedUser by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="bannedUser"></param>
        /// <returns></returns>
        public async Task<BannedUsers> UpdateBannedUser(string connString, int id, BannedUserDto bannedUser)
        {
            var tempBannedUser = await GetBannedUserById(connectionString, id);
            if (tempBannedUser != null)
            {
                try
                {
                    string sqlQuery = "UPDATE bannedUsers SET user_id = @user_id," +
                                                      " banned_by_user_id =  @banned_by_user_id," +
                                                      " ban_start_date =  @ban_start_date," +
                                                      " ban_end_date =  @ban_end_date," +
                                                      " reason =  @reason," +
                                                      " WHERE banned_user_id = @banned_user_id;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = bannedUser.user_id;
                        cmd.Parameters.Add("@banned_by_user_id", SqlDbType.Int).Value = bannedUser.banned_by_user_id;
                        cmd.Parameters.Add("@ban_start_date", SqlDbType.DateTime).Value = bannedUser.ban_start_date;
                        cmd.Parameters.Add("@ban_end_date", SqlDbType.DateTime).Value = bannedUser.ban_end_date;
                        cmd.Parameters.Add("@reason", SqlDbType.VarChar).Value = bannedUser.reason;
                        cmd.Parameters.Add("@banned_user_id", SqlDbType.Int).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new BannedUsers(id, bannedUser.user_id, bannedUser.banned_by_user_id,
                            bannedUser.ban_start_date, bannedUser.ban_end_date, bannedUser.reason);

                    }
                }
                catch (Exception ex)
                {
                    return null; ;
                }
            }
            else
            {
                return null; 
            }

        }

        /// <summary>
        /// Method to delete a bannedUser by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BannedUsers> DeleteBannedUser(string connString, int id)
        {

            var tempBannedUser = await GetBannedUserById(connectionString, id);
            if (tempBannedUser != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM bannedUsers WHERE bannedUser_id = @bannedUser_id;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@bannedUser_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempBannedUser;

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