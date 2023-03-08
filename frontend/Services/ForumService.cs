using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.ForumDTO;
using Smogon_MAUIapp.Entities;

namespace Smogon_MAUIapp.Services
{
    public class ForumService : InfosAPI
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

        public ForumService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get all forums from DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Forums>> GetAllForums()
        {
            return new List<Forums>();
        }

        /// <summary>
        /// Method to get one forum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Forums> GetForumById(int _id)
        {
            var json = await client.GetStringAsync($"forums/{_id}");
            var topic = JsonConvert.DeserializeObject<Forums>(json);

            return topic;

        }

        /// <summary>
        /// Method to create a forum
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="forum"></param>
        /// <returns></returns>
        public async Task<Forums> CreateForum(ForumDto forum)
        {
            return new Forums();
        }

        /// <summary>
        /// Method to update a forum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="forum"></param>
        /// <returns></returns>
        public async Task<Forums> UpdateForum(int id, ForumDto forum)
        {
            return new Forums();

        }

        /// <summary>
        /// Method to delete a forum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Forums> DeleteForum(int id)
        {

            return new Forums();
        }

        /// <summary>
        /// Method to get oneall subforums by forum ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Forums> GetAllSubForumsByForumId(int _id)
        {
            try
            {
                var json = await client.GetStringAsync($"forums/{_id}/subforums");
                var topic = JsonConvert.DeserializeObject<Forums>(json);

                return topic;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get one forum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<List<Threads>> GetAllThreadsByForumId(int _id)
        {
            try
            {
                var json = await client.GetStringAsync($"forums/{_id}/threads");
                var forum = JsonConvert.DeserializeObject<Forums>(json);
                var threads = forum.threads;

                return threads;
            }
            catch
            {
                return null;
            }
            
        }

        #endregion

    }
}