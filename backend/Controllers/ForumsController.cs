using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.ForumDTO;
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
    [Route("/forums")]
    public class ForumsController : ControllerBase
    {
        //string connectionString = Utils.ConnectionString;
        string connectionString = Tools.Tools.connectionString;
        ForumService forumService = new ForumService();

        public ForumsController(){}

        [HttpGet]
        public async Task<ActionResult<List<Forums>>> GetAllForums()
        {
            var forums = await forumService.GetAllForums(connectionString);
            if (forums.Count == 0)
                return BadRequest("An error occurred while getting all the forums. Please check your request and try again.");
            return Ok(forums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Forums>> GetForumById(int id)
        {
            var forum = await forumService.GetForumById(connectionString, id);
            if (forum.name != null)
            {
                return Ok(forum);
            }
            else
            {
                return BadRequest("An error occurred while getting forum. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Forums>> PostForum(ForumDto forum)
        {
            try
            {
                var createdForum = await forumService.CreateForum(connectionString, forum);
                if (createdForum != null)
                {
                    dynamic response = new ExpandoObject();
                    response.name = createdForum.name;
                    response.description = createdForum.description;
                    response.subforums = createdForum.subforums;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while creating the forum. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the forum. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutForum(int id, ForumDto forum)
        {
            try 
            {
                var updatedForum = await forumService.UpdateForum(connectionString, id, forum);
                if (updatedForum.name != null)
                {
                    dynamic response = new ExpandoObject();
                    response.forum_id = id;
                    response.name = updatedForum.name;
                    response.description = updatedForum.description;
                    response.subforums = updatedForum.subforums;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while updating the forum. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the forum. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Forums>> DeleteForum(int id)
        {
            var deletedForum = await forumService.DeleteForum(connectionString, id);
            if (deletedForum.name != null)
            {                
                return Ok(deletedForum);
            }
            else
            {
                return BadRequest("An error occurred while deleting the forum. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/subforums")]
        public async Task<ActionResult<List<Threads>>> GetAllSubForumsByForumId(int id)
        {
            var threads = await forumService.GetAllSubForumsByForumId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
