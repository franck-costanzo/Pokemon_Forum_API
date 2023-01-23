using Microsoft.Data.SqlClient;
using Pokemon_Forum_API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Pokemon_Forum_API.Services
{
    public class SearchService
    {

        /// <summary>
        /// Method to search posts
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<List<Posts>> SearchPosts(string connString, string searchString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM posts WHERE content LIKE @searchString", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var posts = new List<Posts>();
                        while (await reader.ReadAsync())
                        {
                            int post_id = reader.GetInt32(0);
                            string content = reader.GetString(1);
                            DateTime create_date = reader.GetDateTime(2);
                            int thread_id = reader.GetInt32(3);
                            int user_id = reader.GetInt32(4);
                            var post = new Posts(post_id, content, create_date, thread_id, user_id);

                            posts.Add(post);
                        }
                        return posts;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
