using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.TopicDTO;
using Smogon_MAUIapp.Entities;

namespace Smogon_MAUIapp.Services
{
    public class TopicService : InfosAPI
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

        public TopicService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        /// <summary>
        /// Method to get all topics from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Topics>> GetAllTopics()
        {
            var json = await client.GetStringAsync("topics");
            var topics = JsonConvert.DeserializeObject<List<Topics>>(json);

            return topics;

        }

        /// <summary>
        /// Method to get one topic by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Topics> GetTopicById(int _id)
        {
            var json = await client.GetStringAsync($"topics/{_id}");
            var topic = JsonConvert.DeserializeObject<Topics>(json);

            return topic;
        }

        /// <summary>
        /// Method to create a topic
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<Topics> CreateTopic(TopicDto topic)
        {
            return new Topics();
        }

        /// <summary>
        /// Method to update a topic by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task<Topics> UpdateTopic(int id, TopicDto topic)
        {
            return new Topics();
        }

        /// <summary>
        /// Method to delete a topic by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Topics> DeleteTopic(int id)
        {
            return new Topics();
        }


        /// <summary>
        /// Method to get all users by topic ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="topic_id"></param>
        /// <returns></returns>
        public async Task<List<Forums>> GetForumsByTopicId(int topic_id)
        {
            var json = await client.GetStringAsync($"topics/{topic_id}/forums");
            var forumsList = JsonConvert.DeserializeObject<List<Forums>>(json);

            return forumsList;
        }

    }
}