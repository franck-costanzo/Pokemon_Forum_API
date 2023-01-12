using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.BannedUserDTO;
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
    [Route("/bannedusers")]
    public class BannedUsersController : ControllerBase
    {
        string connectionString = Utils.ConnectionString;
        BannedUserService bannedUserService = new BannedUserService();

        public BannedUsersController(){}

        [HttpGet]
        public async Task<ActionResult<List<BannedUsers>>> GetAllBannedUsers()
        {
            var bannedUsers = await bannedUserService.GetAllBannedUsers(connectionString);
            if (bannedUsers.Count == 0)
                return BadRequest("An error occurred while getting all the bannedUsers. Please check your request and try again.");
            return Ok(bannedUsers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BannedUsers>> GetBannedUserById(int id)
        {
            var bannedUser = await bannedUserService.GetBannedUserById(connectionString, id);
            if (bannedUser != null)
            {
                return Ok(bannedUser);
            }
            else
            {
                return BadRequest("An error occurred while getting bannedUser. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BannedUsers>> PostBannedUser(BannedUserDto bannedUser)
        {
            try
            {
                var createdBannedUser = await bannedUserService.CreateBannedUser(connectionString, bannedUser);
                if (createdBannedUser != null)
                {
                    return Ok(createdBannedUser);
                }
                else
                {
                    return BadRequest("An error occurred while creating the bannedUser. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the bannedUser. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBannedUser(int id, BannedUserDto bannedUser)
        {
            try 
            {
                var updatedBannedUser = await bannedUserService.UpdateBannedUser(connectionString, id, bannedUser);
                if (updatedBannedUser != null)
                {
                    return Ok(updatedBannedUser);
                }
                else
                {
                    return BadRequest("An error occurred while updating the bannedUser. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the bannedUser. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BannedUsers>> DeleteBannedUser(int id)
        {
            var deletedBannedUser = await bannedUserService.DeleteBannedUser(connectionString, id);
            if (deletedBannedUser != null)
            {                
                return Ok(deletedBannedUser);
            }
            else
            {
                return BadRequest("An error occurred while deleting the bannedUser. Please check your request and try again.");
            }
            
        }


    }
}
