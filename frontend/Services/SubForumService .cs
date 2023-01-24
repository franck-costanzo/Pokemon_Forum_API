﻿using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.SubForumDTO;
using Smogon_MAUIapp.Entities;
using System.Text;

namespace Smogon_MAUIapp.Services
{
    public class SubForumService : InfosAPI
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

        public SubForumService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        #region Methods

        public async Task<SubForums> GetSubForumById(int _id)
        {
            var json = await client.GetStringAsync($"subforums/{_id}");
            var subForums = JsonConvert.DeserializeObject<SubForums>(json);

            return subForums;
        }

        /// <summary>
        /// Method to get one subForum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<SubForums> GetAllThreadsBySubForumId(int _id)
        {
            var json = await client.GetStringAsync($"subforums/{_id}/threads");
            var subForums = JsonConvert.DeserializeObject<SubForums>(json);

            return subForums;
        }

        #endregion

    }
}