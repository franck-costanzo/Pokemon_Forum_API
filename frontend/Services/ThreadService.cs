﻿using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.ThreadDTO;
using Smogon_MAUIapp.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace Smogon_MAUIapp.Services
{
    public class ThreadService : InfosAPI
    {
        #region Properties

        public string url
        {
            get
            {
                return base.baseUrl;
            }
            set
            {
                base.baseUrl = value;
            }
        }

        private HttpClient client;

        #endregion


        #region Constructor

        public ThreadService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get all threads from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Threads>> GetAllThreads()
        {
            
            return new List<Threads>();

        }

        /// <summary>
        /// Method to get one thread by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Threads> GetThreadById(int _id)
        {
            var json = await client.GetStringAsync($"threads/{_id}");
            var thread = JsonConvert.DeserializeObject<Threads>(json);

            return thread;
        }

        /// <summary>
        /// Method to create a thread
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public async Task<Threads> CreateThread(ThreadDto thread, JwtSecurityToken jwtToken)
        {
            var json = JsonConvert.SerializeObject(thread);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add JWT token to Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            var response = await client.PostAsync($"threads", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdThread = JsonConvert.DeserializeObject<Threads>(responseJson);
            return createdThread;
        }

        /// <summary>
        /// Method to update a thread by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public async Task<Threads> UpdateThread(int id, ThreadDto thread)
        {
            return new Threads();
        }

        /// <summary>
        /// Method to delete a thread by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Threads> DeleteThread(int id)
        {
            return new Threads();
        }



        /// <summary>
        /// Method to get all posts by thread ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Threads> GetThreadWithPostByThreadId(int _id)
        {
            var json = await client.GetStringAsync($"threads/{_id}/posts");
            var thread = JsonConvert.DeserializeObject<Threads>(json);

            return thread;
        }

        #endregion

    }
}