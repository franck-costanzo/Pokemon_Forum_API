using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.PostDTO;
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
    public class PostsController : ControllerBase
    {
        //string connectionString = Utils.ConnectionString;
        string connectionString = Tools.Tools.connectionString;
        PostService postService = new PostService();

        public PostsController(){}

        [HttpGet]
        public async Task<ActionResult<List<Posts>>> GetAllPosts()
        {
            var posts = await postService.GetAllPosts(connectionString);
            if (posts.Count == 0)
                return BadRequest("An error occurred while getting all the posts. Please check your request and try again.");
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPostById(int id)
        {
            var post = await postService.GetPostById(connectionString, id);
            if (post != null)
            {
                return Ok(post);
            }
            else
            {
                return BadRequest("An error occurred while getting post. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Posts>> PostPost(TeamDto post)
        {
            try
            {
                var createdPost = await postService.CreatePost(connectionString, post);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, TeamDto post)
        {
            try 
            {
                var updatedPost = await postService.UpdatePost(connectionString, id, post);
                if (updatedPost != null)
                {
                    return Ok(updatedPost);
                }
                else
                {
                    return BadRequest("An error occurred while updating the post. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the post. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Posts>> DeletePost(int id)
        {
            var deletedPost = await postService.DeletePost(connectionString, id);
            if (deletedPost != null)
            {                
                return Ok(deletedPost);
            }
            else
            {
                return BadRequest("An error occurred while deleting the post. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/likes")]
        public async Task<ActionResult<List<Threads>>> GetAllThreadsByPostId(int id)
        {
            var threads = await postService.GetAllLikesByPostId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
