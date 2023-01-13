using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.ThreadDTO;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using utils;
using static Org.BouncyCastle.Math.EC.ECCurve;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pokemon_Forum_API.Controllers
{
    [ApiController]
    [Route("/threads")]
    public class ThreadsController : ControllerBase
    {
        string connectionString = Utils.ConnectionString;
        ThreadService threadService = new ThreadService();

        public ThreadsController(){}

        [HttpGet]
        public async Task<ActionResult<List<Threads>>> GetAllThreads()
        {
            var threads = await threadService.GetAllThreads(connectionString);
            if (threads.Count == 0)
                return BadRequest("An error occurred while getting all the threads. Please check your request and try again.");
            return Ok(threads);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Threads>> GetThreadById(int id)
        {
            var thread = await threadService.GetThreadById(connectionString, id);
            if (thread != null)
            {
                return Ok(thread);
            }
            else
            {
                return BadRequest("An error occurred while getting thread. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Threads>> PostThread(ThreadDto thread)
        {
            try
            {
                var createdThread = await threadService.CreateThread(connectionString, thread);
                if (createdThread != null)
                {

                    return Ok(createdThread);
                }
                else
                {
                    return BadRequest("An error occurred while creating the thread. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the thread. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutThread(int id, ThreadDto thread)
        {
            try 
            {
                var updatedThread = await threadService.UpdateThread(connectionString, id, thread);
                if (updatedThread != null)
                {

                    return Ok(updatedThread);
                }
                else
                {
                    return BadRequest("An error occurred while updating the thread. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the thread. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Threads>> DeleteThread(int id)
        {
            var deletedThread = await threadService.DeleteThread(connectionString, id);
            if (deletedThread != null)
            {                
                return Ok(deletedThread);
            }
            else
            {
                return BadRequest("An error occurred while deleting the thread. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/posts")]
        public async Task<ActionResult<List<Threads>>> GetAllThreadsByThreadId(int id)
        {
            var threads = await threadService.GetAllPostsByThreadId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
