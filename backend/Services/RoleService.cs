using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.RoleDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class RoleService
    {
        string connectionString = Tools.Tools.connectionString;
        public RoleService() {}

        /// <summary>
        /// Method to get all roles from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Roles>> GetAllRoles(string connString)
        {
            try
            {
                List<Roles> roles = new List<Roles>();
            
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM roles", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                    
                        while (await reader.ReadAsync())
                        {
                            int role_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            roles.Add(new Roles(role_id, name, description));
                        }
                    }
                
                }
                return roles;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get one role by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Roles> GetRoleById(string connString, int _id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM roles where role_id=@role_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@role_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int role_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            return new Roles(role_id, name, description);
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
        /// Method to create a role
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Roles> CreateRole(string connString, RoleDto role)
        {
            try
            {                  

                string sqlQuery = "INSERT INTO Roles(name, description) VALUES(@name, @description);";
                DateTime now = DateTime.Now;
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = role.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = role.description;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Roles(role.name, role.description);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return null;
            }
        }

        /// <summary>
        /// Method to update a role by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Roles> UpdateRole(string connString, int id, RoleDto role)
        {
            var tempRole = await GetRoleById(connectionString, id);
            if (tempRole != null)
            {
                try
                {
                    string sqlQuery = "UPDATE roles SET name = @name," +
                                                      " description =  @description" +
                                                      " WHERE role_id = @role_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = role.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = role.description;
                        cmd.Parameters.Add("@role_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Roles(id, role.name, role.description);

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
        /// Method to delete a role by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Roles> DeleteRole(string connString, int id)
        {

            var tempRole = await GetRoleById(connectionString, id);
            if (tempRole != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM roles WHERE role_id = @role_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@role_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempRole;

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
        /// Method to get all users by role ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetUsersByRoleId(string connString, int role_id)
        {
            List<Users> users = new List<Users>();
            try 
            { 
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE role_id = @role_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@role_id", role_id);

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
                            int _role_id = reader.GetInt32(6);
                            bool isBanned = reader.GetBoolean(7);
                            users.Add(new Users(id, username, password, email, join_date, avatar_url, _role_id, isBanned));
                        }
                    }
                }

                return users;
            
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}