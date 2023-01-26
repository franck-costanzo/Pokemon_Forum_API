using Microsoft.Data.SqlClient;
using Pokemon_Forum_API.DTO.TeamDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class TeamService
    {
        string connectionString = Tools.Tools.connectionString;

        UserService userService = new UserService();
        ThreadService threadService = new ThreadService();
        public TeamService() { }

        /// <summary>
        /// Method to get all teams from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Teams>> GetAllTeams(string connString)
        {
            
            List<Teams> teams = new List<Teams>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM teams", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int team_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string link = reader.GetString(2);
                            DateTime create_date = reader.GetDateTime(3);
                            int user_id = reader.GetInt32(4);
                            var team = new Teams(team_id, name, link, create_date, user_id);
                        
                            teams.Add(team);
                        }
                    }

                }
                
                return teams;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get one team by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Teams> GetTeamById(string connString, int _id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM teams where team_id=@team_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@team_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int team_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string link = reader.GetString(2);
                            DateTime create_date = reader.GetDateTime(3);
                            int user_id = reader.GetInt32(4);
                            var thread = new Teams(team_id, name, link, create_date, user_id);

                            return thread;
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
        /// Method to create a team
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<Teams> CreateTeam(string connString, TeamDto team)
        {
            DateTime now = DateTime.Now;
            try
            {

                string sqlQuery = "INSERT INTO teams (name, link, date_created, user_id) " +
                                                   "VALUES (@name, @link, @date_created, @user_id);";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = team.name;
                        cmd.Parameters.Add("@link", SqlDbType.VarChar).Value = team.link;
                        cmd.Parameters.Add("@date_created", SqlDbType.DateTime).Value = now;
                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = team.user_id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Teams(team.name, team.link, now, team.user_id);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return null;
            }
        }

        /// <summary>
        /// Method to update a team by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<Teams> UpdateTeam(string connString, int id, TeamDto team)
        {
            var tempTeam = await GetTeamById(connectionString, id);
            if (tempTeam != null)
            {
                try
                {
                    string sqlQuery = "UPDATE teams SET name = @name," +
                                                      " link = @link" +
                                                      " WHERE team_id = @team_id;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = team.name;
                        cmd.Parameters.Add("@link", SqlDbType.VarChar).Value = team.link;
                        cmd.Parameters.Add("@team_id", SqlDbType.Int).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Teams(id, team.name, team.link, tempTeam.date_created, tempTeam.user_id);

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
        /// Method to delete a team by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Teams> DeleteTeam(string connString, int id)
        {

            var tempTeam = await GetTeamById(connectionString, id);
            if (tempTeam != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM teams WHERE team_id = @team_id;";
                    using (SqlConnection conn = new SqlConnection(connString))
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@team_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempTeam;

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