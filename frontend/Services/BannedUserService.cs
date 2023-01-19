using Smogon_MAUIapp.DTO.BannedUserDTO;
using Smogon_MAUIapp.Entities;
namespace Smogon_MAUIapp.Services
{
    public class BannedUserService : InfosAPI
    {
        public BannedUserService() { }

        /// <summary>
        /// Method to get all bannedUsers from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<BannedUsers>> GetAllBannedUsers()
        {
            return new List<BannedUsers>();
        }

        /// <summary>
        /// Method to get one bannedUser by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<BannedUsers> GetBannedUserById(int _id)
        {
            return new BannedUsers();
        }

        /// <summary>
        /// Method to create a bannedUser
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="bannedUser"></param>
        /// <returns></returns>
        public async Task<BannedUsers> CreateBannedUser(BannedUserDto bannedUser)
        {
            return new BannedUsers();
        }

        /// <summary>
        /// Method to update a bannedUser by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="bannedUser"></param>
        /// <returns></returns>
        public async Task<BannedUsers> UpdateBannedUser(int id, BannedUserDto bannedUser)
        {
            
            return new BannedUsers();
        }

        /// <summary>
        /// Method to delete a bannedUser by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BannedUsers> DeleteBannedUser(int id)
        {
            return new BannedUsers();
        }
    }
}