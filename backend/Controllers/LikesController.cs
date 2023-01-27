using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.LikeDTO;
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
    [Route("/likes")]
    public class LikesController : ControllerBase
    {
        //string connectionString = Utils.ConnectionString;
        string connectionString = Tools.Tools.connectionString;
        LikeService likeService = new LikeService();

        public LikesController(){}


        [HttpGet("{id}")]
        public async Task<ActionResult<Likes>> GetLikeById(int id)
        {
            var like = await likeService.GetLikeById(connectionString, id);
            if (like != null)
            {
                return Ok(like);
            }
            else
            {
                return BadRequest("An error occurred while getting like. Please check your request and try again.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Likes>> PostLike(LikeDto like)
        {
            try
            {
                var createdLike = await likeService.CreateLike(connectionString, like);
                if (createdLike != null)
                {

                    return Ok(createdLike);
                }
                else
                {
                    return BadRequest("An error occurred while creating the like. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the like. Please check your request and try again.");
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Likes>> DeleteLike(int id)
        {
            var deletedLike = await likeService.DeleteLike(connectionString, id);
            if (deletedLike != null)
            {                
                return Ok(deletedLike);
            }
            else
            {
                return BadRequest("An error occurred while deleting the like. Please check your request and try again.");
            }
            
        }


    }
}
