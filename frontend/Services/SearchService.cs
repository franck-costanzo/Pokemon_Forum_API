using Newtonsoft.Json;
using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Services;

namespace Pokemon_Forum_API.Services
{
    public class SearchService : InfosAPI
    {
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

        public SearchService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }
        /// <summary>
        /// Method to search posts
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<List<Posts>> SearchPosts(string searchString)
        {
            try
            {
                var json = await client.GetStringAsync("search/" + searchString);

                var postsList = JsonConvert.DeserializeObject<List<Posts>>(json);

                return postsList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
