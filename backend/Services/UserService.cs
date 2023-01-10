using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Security.Cryptography;
using utils;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Utilities.Collections;
using System.Web.Helpers;

namespace Pokemon_Forum_API.Services
{
    public class UserService
    {
        string connectionString = Utils.ConnectionString;
        public UserService() {}

        public async Task<List<Users>> GetAllUsers(string connString)
        {
            
            List<Users> users = new List<Users>();
            
            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", conn))
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
                        DateTime join_date = reader.GetDateTime(4);
                        int role_id = reader.GetInt32(5);
                        bool isBanned = reader.GetBoolean(6);
                        users.Add(new Users(id, username, password, email, join_date, role_id, isBanned));
                    }
                }
                
            }
            return users;
        }

        public async Task<Users> GetUserById(string connString, int _id)
        {

            Users user = new Users();

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
                        DateTime join_date = reader.GetDateTime(4);
                        int role_id = reader.GetInt32(5);
                        bool isBanned = reader.GetBoolean(6);
                        return user = new Users(id, username, password, email, join_date, role_id, isBanned));
                    }
                }

            }

            return user;
        }

        public async Task<bool> CreateUser(string connString, Users user)
        {
            try
            {
                string sqlQuery = "INSERT INTO Users(username, password, email, join_date, role_id, isBanned)" +
                              "VALUES(@username, @password', @email, @join_date, @role_id, @isBanned);";
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                {

                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@password", user.password);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@join_date", user.join_date);
                    cmd.Parameters.AddWithValue("@role_id", user.role_id);
                    cmd.Parameters.AddWithValue("@isBanned", user.isBanned);

                    await cmd.ExecuteNonQueryAsync();
                    return true;

                }
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(string connString, int id, Users user)
        {
            var tempUser = await GetUserById(connectionString, id);
            if (tempUser != null) 
            {
                try
                {
                    string sqlQuery = "UPDATE users SET username = @username," +
                                                      " password =  @password," +
                                                      " email = @email," +
                                                      " join_date = @join_date," +
                                                      " role_id = @role_id," +
                                                      " isBanned = @isBanned" +
                                                      " WHERE user_id = @user_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@username", user.username);
                        cmd.Parameters.AddWithValue("@password", user.password);
                        cmd.Parameters.AddWithValue("@email", user.email);
                        cmd.Parameters.AddWithValue("@join_date", user.join_date);
                        cmd.Parameters.AddWithValue("@role_id", user.role_id);
                        cmd.Parameters.AddWithValue("@isBanned", user.isBanned);
                        cmd.Parameters.AddWithValue("@user_id", user.user_id);

                        await cmd.ExecuteNonQueryAsync();
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteUser(string connString, int id)
        {

            var tempUser = await GetUserById(connectionString, id);
            if (tempUser != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM user WHERE  user_id = @user_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@user_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
