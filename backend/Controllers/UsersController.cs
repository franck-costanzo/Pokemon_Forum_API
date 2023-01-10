using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using utils;

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
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await userService.GetUserById(connectionString, id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostUser(Users user)
        {
            var result = await userService.CreateUser(connectionString, user);
            if (result == false)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            var result = await userService.UpdateUser(connectionString, id, user);
            if (result == false)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var result = await userService.DeleteUser(connectionString, id);
            if (result == false)
                return NotFound();
            return Ok(result);
        }


    }
}
