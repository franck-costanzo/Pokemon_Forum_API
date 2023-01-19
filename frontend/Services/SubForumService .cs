using Smogon_MAUIapp.DTO.SubForumDTO;
using Smogon_MAUIapp.Entities;
using System.Text;

namespace Smogon_MAUIapp.Services
{
    public class SubForumService : InfosAPI
    {
        /// <summary>
        /// Method to get all subForums from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<SubForums>> GetAllSubForums()
        {
            return new List<SubForums>();
        }

        /// <summary>
        /// Method to get one subForum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<SubForums> GetSubForumById(int _id)
        {
            return new SubForums();
        }

        /// <summary>
        /// Method to create a subForum
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="subForum"></param>
        /// <returns></returns>
        public async Task<SubForums> CreateSubForum(SubForumDto subForum)
        {
            return new SubForums();
        }

        /// <summary>
        /// Method to update a subForum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="subForum"></param>
        /// <returns></returns>
        public async Task<SubForums> UpdateSubForum(int id, SubForumDto subForum)
        {
            return new SubForums();
        }

        /// <summary>
        /// Method to delete a subForum by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SubForums> DeleteSubForum(int id)
        {
            return new SubForums();
        }

 

        /// <summary>
        /// Method to get one subForum by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<SubForums> GetAllThreadsBySubForumId(int _id)
        {
            return new SubForums();
        }
    }
}