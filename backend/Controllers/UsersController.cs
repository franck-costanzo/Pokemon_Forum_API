using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Services;
using Pokemon_Forum_API.Tools.UserTools;
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
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        string connectionString = Utils.ConnectionString;
        //string connectionString = @"Server=127.0.0.1;User ID=root;Password=;Database=smogon_forum;";
        UserService userService = new UserService();

        public UsersController(){}

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            var users = await userService.GetAllUsers(connectionString);
            if (users.Count == 0)
                return BadRequest("An error occurred while getting all the users. Please check your request and try again.");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await userService.GetUserById(connectionString, id);
            if (user.username != null)
            {
                user.password = "Password is encrypted";
                return Ok(user);
            }
            else
            {
                return BadRequest("An error occurred while getting user. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(UserDtoCreate user)
        {
            try
            {
                var createdUser = await userService.CreateUser(connectionString, user);
                if (createdUser != null)
                {
                    dynamic response = new ExpandoObject();
                    response.username = createdUser.username;
                    response.email = createdUser.email;
                    response.join_date = createdUser.join_date;
                    response.role_id = createdUser.role_id;
                    response.isBanned = createdUser.isBanned;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while creating the user. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the user. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDtoUpdate user)
        {
            try 
            {
                var updatedUser = await userService.UpdateUser(connectionString, id, user);
                if (updatedUser.username != null)
                {
                    dynamic response = new ExpandoObject();
                    response.user_id = updatedUser.user_id;
                    response.username = updatedUser.username;
                    response.email = updatedUser.email;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while updating the user. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the user. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var deletedUser = await userService.DeleteUser(connectionString, id);
            if (deletedUser.username != null)
            {
                deletedUser.password = "Password was encrypted";
                return Ok(deletedUser);
            }
            else
            {
                return BadRequest("An error occurred while deleting the user. Please check your request and try again.");
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDtoLogin userDto)
        {
            var token = await userService.LoginUserJWT(connectionString, userDto.username, userDto.password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpGet("{id}/threads")]
        public async Task<ActionResult<List<Threads>>> GetAllThreadsByUserId(int id)
        {
            var threads = await userService.GetThreadsByUserId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


        [HttpGet("{id}/posts")]
        public async Task<ActionResult<List<Posts>>> GetAllPostsByUserId(int id)
        {
            var posts = await userService.GetPostsByUserId(connectionString, id);
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }
    }
}
