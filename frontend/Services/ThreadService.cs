using Smogon_MAUIapp.DTO.ThreadDTO;
using Smogon_MAUIapp.Entities;

namespace Smogon_MAUIapp.Services
{
    public class ThreadService : InfosAPI
    {

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
            return new Threads();
        }

        /// <summary>
        /// Method to create a thread
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="thread"></param>
        /// <returns></returns>
        public async Task<Threads> CreateThread(ThreadDto thread)
        {
            return new Threads();
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
        public async Task<Threads> GetAllPostsByThreadId(int _id)
        {
            return new Threads();
        }

    }
}