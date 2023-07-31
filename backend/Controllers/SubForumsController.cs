using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.SubForumDTO;
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
    [Route("/subforums")]
    public class SubForumsController : ControllerBase
    {
        //string connectionString = Utils.ConnectionString;
        string connectionString = Tools.Tools.connectionString;
        SubForumService subForumService = new SubForumService();

        public SubForumsController(){}

        [HttpGet]
        public async Task<ActionResult<List<SubForums>>> GetAllSubForums()
        {
            var subForums = await subForumService.GetAllSubForums(connectionString);
            if (subForums.Count == 0)
                return BadRequest("An error occurred while getting all the subForums. Please check your request and try again.");
            return Ok(subForums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubForums>> GetSubForumById(int id)
        {
            var subForum = await subForumService.GetSubForumById(connectionString, id);
            if (subForum.name != null)
            {
                return Ok(subForum);
            }
            else
            {
                return BadRequest("An error occurred while getting subForum. Please check your request and try again.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SubForums>> PostSubForum(SubForumDto subForum)
        {
            try
            {
                var createdSubForum = await subForumService.CreateSubForum(connectionString, subForum);
                if (createdSubForum != null)
                {
                    dynamic response = new ExpandoObject();
                    response.name = createdSubForum.name;
                    response.description = createdSubForum.description;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while creating the subForum. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the subForum. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutSubForum(int id, SubForumDto subForum)
        {
            try 
            {
                var updatedSubForum = await subForumService.UpdateSubForum(connectionString, id, subForum);
                if (updatedSubForum.name != null)
                {
                    dynamic response = new ExpandoObject();
                    response.subForum_id = id;
                    response.name = updatedSubForum.name;
                    response.description = updatedSubForum.description;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while updating the subForum. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the subForum. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<SubForums>> DeleteSubForum(int id)
        {
            var deletedSubForum = await subForumService.DeleteSubForum(connectionString, id);
            if (deletedSubForum.name != null)
            {                
                return Ok(deletedSubForum);
            }
            else
            {
                return BadRequest("An error occurred while deleting the subForum. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/threads")]
        public async Task<ActionResult<List<Threads>>> GetAllThreadsBySubForumId(int id)
        {
            var threads = await subForumService.GetAllThreadsBySubForumId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
