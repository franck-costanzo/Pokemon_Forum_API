using Smogon_MAUIapp.DTO.LikeDTO;
using Smogon_MAUIapp.Entities;

namespace Smogon_MAUIapp.Services
{
    public class LikeService : InfosAPI
    {

        public LikeService() {}

        /// <summary>
        /// Method to get one like by his ID from DB
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Likes> GetLikeById(int _id)
        {
            return new Likes();
        }

        /// <summary>
        /// Method to create a like
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        public async Task<Likes> CreateLike(LikeDto like)
        {
            return new Likes();
        }


        /// <summary>
        /// Method to delete a like by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Likes> DeleteLike(int id)
        {
            return new Likes();
        }


    }

}