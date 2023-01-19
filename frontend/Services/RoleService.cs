using Smogon_MAUIapp.DTO.RoleDTO;
using Smogon_MAUIapp.Entities;

namespace Smogon_MAUIapp.Services
{
    public class RoleService : InfosAPI
    {
        /// <summary>
        /// Method to get all roles from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public async Task<List<Roles>> GetAllRoles()
        {
            return new List<Roles>();
        }

        /// <summary>
        /// Method to get one role by his ID from DB
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<Roles> GetRoleById(int _id)
        {
            return new Roles();
        }

        /// <summary>
        /// Method to create a role
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Roles> CreateRole(RoleDto role)
        {
            return new Roles();
        }

        /// <summary>
        /// Method to update a role by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Roles> UpdateRole(int id, RoleDto role)
        {
            return new Roles();
        }

        /// <summary>
        /// Method to delete a role by his ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Roles> DeleteRole(int id)
        {
            return new Roles();
        }


        /// <summary>
        /// Method to get all users by role ID
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetUsersByRoleId(string connString, int role_id)
        {
            return new List<Users>();            
        }

    }
}