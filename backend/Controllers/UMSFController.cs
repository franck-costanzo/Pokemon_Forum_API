using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.PostDTO;
using Pokemon_Forum_API.DTO.UMSFDTO;
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
    [Route("/posts")]
    public class UMSFController : ControllerBase
    {
        //string connectionString = Utils.ConnectionString;
        string connectionString = Tools.Tools.connectionString;
        UMSFService UMSFService = new UMSFService();

        public UMSFController(){}

        [HttpGet]
        public async Task<ActionResult<List<User_Moderates_SubForum>>> GetAllPosts()
        {
            var posts = await UMSFService.GetAllModerators(connectionString);
            if (posts.Count == 0)
                return BadRequest("An error occurred while getting all the posts. Please check your request and try again.");
            return Ok(posts);
        }

        [HttpGet("{id}/moderators")]
        public async Task<ActionResult<List<Users>>> GetModeratorsBySubforumId(int id)
        {
            var users = await UMSFService.GetModeratorsBySubforumId(connectionString, id);
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest("An error occurred while getting post. Please check your request and try again.");
            }
        }

        [HttpGet("{id}/subforums")]
        public async Task<ActionResult<List<SubForums>>> GetsubforumsByModeratorId(int id)
        {
            var subforums = await UMSFService.GetsubforumsByModeratorId(connectionString, id);
            if (subforums != null)
            {
                return Ok(subforums);
            }
            else
            {
                return BadRequest("An error occurred while getting post. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User_Moderates_SubForum>> PostUMSF(UMSFDto uMSF)
        {
            try
            {
                var createdPost = await UMSFService.CreateModeratorForSubforum(connectionString, uMSF);
                if (createdPost != null)
                {
                    return Ok(createdPost);
                }
                else
                {
                    return BadRequest("An error occurred while creating the post. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the post. Please check your request and try again.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User_Moderates_SubForum>> DeleteModeratorForSubforum(int id)
        {
            var deletedPost = await UMSFService.DeleteModeratorForSubforum(connectionString, id);
            if (deletedPost != null)
            {                
                return Ok(deletedPost);
            }
            else
            {
                return BadRequest("An error occurred while deleting the post. Please check your request and try again.");
            }
            
        }


    }
}
