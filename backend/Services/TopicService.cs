using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Pokemon_Forum_API.DTO.TopicDTO;
using Pokemon_Forum_API.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using utils;

namespace Pokemon_Forum_API.Services
{
    public class TopicService
    {
        string connectionString = Utils.ConnectionString;
        public TopicService() {}

        /// <summary>
        /// Method to get all topics from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Topics>> GetAllTopics(string connString)
        {
            
            List<Topics> topics = new List<Topics>();
            
            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM topics", conn))
            {
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    
                    while (await reader.ReadAsync())
                    {
                        int topic_id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        topics.Add(new Topics(topic_id, name));
                    }
                }
                
            }
            return topics;
        }

        /// <summary>
        /// Method to get one topic by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Topics> GetTopicById(string connString, int _id)
        {

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM topics where topic_id=@topic_id", conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@topic_id", _id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        int topic_id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        return new Topics(topic_id, name);
                    }
                }

            }

            return new Topics();
        }

        /// <summary>
        /// Method to create a topic
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<Topics> CreateTopic(string connString, TopicDto topic)
        {
            try
            {                  

                string sqlQuery = "INSERT INTO Topics(name) VALUES(@name);";
                DateTime now = DateTime.Now;
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = topic.name;

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return new Topics(topic.name);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return new Topics();
            }
        }

        /// <summary>
        /// Method to update a topic by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<Topics> UpdateTopic(string connString, int id, TopicDto topic)
        {
            var tempTopic = await GetTopicById(connectionString, id);
            if (tempTopic != null)
            {
                try
                {
                    string sqlQuery = "UPDATE topics SET name = @name" +
                                                      " WHERE topic_id = @topic_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {
                        await conn.OpenAsync();
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = topic.name;
                        cmd.Parameters.Add("@topic_id", MySqlDbType.Int32).Value = id;
                        await cmd.ExecuteNonQueryAsync();
                        return new Topics(id, topic.name);

                    }
                }
                catch (Exception ex)
                {
                    return new Topics();
                }
            }
            else
            {
                return new Topics();
            }
            
        }

        /// <summary>
        /// Method to delete a topic by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Topics> DeleteTopic(string connString, int id)
        {

            var tempTopic = await GetTopicById(connectionString, id);
            if (tempTopic != null)
            {
                try
                {
                    string sqlQuery = "DELETE FROM topics WHERE topic_id = @topic_id;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                    {

                        await conn.OpenAsync();
                        cmd.Parameters.AddWithValue("@topic_id", id);

                        await cmd.ExecuteNonQueryAsync();
                        return tempTopic;

                    }
                }
                catch (Exception ex)
                {
                    return new Topics();
                }
            }
            else
            {
                return new Topics();
            }
        }


        /// <summary>
        /// Method to get all users by topic ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="topic_id"></param>
        /// <returns></returns>
        public async Task<List<Forums>> GetForumsByTopicId(string connString, int topic_id)
        {
            List<Forums> forums = new List<Forums>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM forums WHERE topic_id = @topic_id", conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@topic_id", topic_id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        forums.Add(new Forums(id, name, description));
                    }
                }
            }
            return forums;
        }

    }
}