using Newtonsoft.Json;
using Smogon_MAUIapp.DTO.PostDTO;
using Smogon_MAUIapp.Entities;

namespace Smogon_MAUIapp.Services
{
    public class PostService : InfosAPI
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

        public PostService()
        {
            client = new HttpClient { BaseAddress = new Uri(url) };
        }

        #endregion

        /// <summary>
        /// Method to get all posts from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Posts>> GetAllPosts()
        {
            return new List<Posts>();
        }

        /// <summary>
        /// Method to get one post by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Posts> GetPostById(int _id)
        {
            var json = await client.GetStringAsync($"posts/{_id}");
            var post = JsonConvert.DeserializeObject<Posts>(json);

            return post;
        }

        /// <summary>
        /// Method to create a post
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Posts> CreatePost(PostDto post)
        {
            return new Posts();
        }

        /// <summary>
        /// Method to update a post by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Posts> UpdatePost(int id, PostDto post)
        {
            return new Posts();
        }

        /// <summary>
        /// Method to delete a post by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Posts> DeletePost(int id)
        {
            return new Posts();
        }



        /// <summary>
        /// Method to get all posts by post ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Posts> GetAllLikesByPostId(int _id)
        {
            return new Posts();
        }

    }

}