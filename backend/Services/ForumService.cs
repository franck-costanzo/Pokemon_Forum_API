﻿using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.ForumDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Services
{
    public class ForumService
    {
        string connectionString = Tools.Tools.connectionString;

        UserService userService = new UserService();
        public ForumService() { }

        /// <summary>
        /// Method to get all forums from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Forums>> GetAllForums(string connString)
        {

            List<Forums> forums = new List<Forums>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM forums", conn))
            {
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        int forum_id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        var forum = new Forums(forum_id, name, description);
                        /*var tempForum = await GetAllSubForumsByForumId(connString, forum_id);
                        forum.subforums = tempForum.subforums;
                        tempForum = await GetAllThreadsByForumId(connString, forum_id);
                        forum.threads = tempForum.threads;*/
                        forums.Add(forum);
                    }
                }

            }
            return forums;
        }

        /// <summary>
        /// Method to get one forum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Forums> GetForumById(string connString, int _id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM forums where forum_id=@forum_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@forum_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int forum_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            var forum = new Forums(forum_id, name, description);
                            /*var tempForum = await GetAllSubForumsByForumId(connString, forum_id);
                            forum.subforums = tempForum.subforums;
                            tempForum = await GetAllThreadsByForumId(connString, forum_id);
                            forum.threads = tempForum.threads;*/
                            return forum;
                        }
                    }

                }
            }
            catch   (Exception ex) 
            {
                return null;
            }

            return null;

        }

        /// <summary>
        /// Method to create a forum
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="forum"></param>
        /// <returns></returns>
        public async Task<Forums> CreateForum(string connString, ForumDto forum)
        {
            try
            {

                string sqlQuery = "INSERT INTO Forums (name, description) " +
                                                   "VALUES (@name, @description);";

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = forum.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = forum.description;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Forums(forum.name, forum.description);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return null;
            }
        }

        /// <summary>
        /// Method to update a forum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="forum"></param>
        /// <returns></returns>
        public async Task<Forums> UpdateForum(string connString, int id, ForumDto forum)
        {
            var tempForum = await GetForumById(connectionString, id);
            if (tempForum != null)
            {
                try
                {
                    string sqlQuery = "UPDATE forums SET name = @name," +
                                                      " description =  @description" +
                                                      " WHERE forum_id = @forum_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = forum.name;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = forum.description;
                        cmd.Parameters.Add("@forum_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Forums(forum.name, forum.description);

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
        /// Method to delete a forum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Forums> DeleteForum(string connString, int id)
        {

            var tempForum = await GetForumById(connectionString, id);
            if (tempForum != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM forums WHERE forum_id = @forum_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@forum_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempForum;

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
        /// Method to get oneall subforums by forum ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Forums> GetAllSubForumsByForumId(string connString, int _id)
        {
            try
            {
                List<SubForums> list = new List<SubForums>();
                Forums forum = await GetForumById(connString, _id);
                using (MySqlConnection conn = new MySqlConnection(connString))
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM subforums where forum_id=@forum_id", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@forum_id", _id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            int subforum_id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            var forum_id = reader.GetInt32(0);
                            list.Add(new SubForums(subforum_id, name, description, forum_id));
                        }
                    }

                }
                forum.subforums = list;

                return forum;
            }
            catch(Exception ex) 
            {
                return null;
            }

            return null;
        }

        
    }
}