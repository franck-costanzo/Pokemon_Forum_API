using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.RoleDTO;
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
    [Route("/roles")]
    public class RolesController : ControllerBase
    {
        string connectionString = Utils.ConnectionString;
        RoleService roleService = new RoleService();

        public RolesController(){}

        [HttpGet]
        public async Task<ActionResult<List<Roles>>> GetAllRoles()
        {
            var roles = await roleService.GetAllRoles(connectionString);
            if (roles.Count == 0)
                return BadRequest("An error occurred while getting all the roles. Please check your request and try again.");
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoleById(int id)
        {
            var role = await roleService.GetRoleById(connectionString, id);
            if (role.name != null)
            {
                return Ok(role);
            }
            else
            {
                return BadRequest("An error occurred while getting role. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Roles>> PostRole(RoleDto role)
        {
            try
            {
                var createdRole = await roleService.CreateRole(connectionString, role);
                if (createdRole != null)
                {
                    dynamic response = new ExpandoObject();
                    response.name = createdRole.name;
                    response.description = createdRole.description;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while creating the role. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the role. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDto role)
        {
            try 
            {
                var updatedRole = await roleService.UpdateRole(connectionString, id, role);
                if (updatedRole.name != null)
                {
                    dynamic response = new ExpandoObject();
                    response.role_id = id;
                    response.name = updatedRole.name;
                    response.description = updatedRole.description;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while updating the role. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the role. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Roles>> DeleteRole(int id)
        {
            var deletedRole = await roleService.DeleteRole(connectionString, id);
            if (deletedRole.name != null)
            {                
                return Ok(deletedRole);
            }
            else
            {
                return BadRequest("An error occurred while deleting the role. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/users")]
        public async Task<ActionResult<List<Threads>>> GetAllThreadsByRoleId(int id)
        {
            var threads = await roleService.GetUsersByRoleId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
