using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.TeamDTO;
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
    [Route("/teams")]
    public class TeamsController : ControllerBase
    {
        //string connectionString = Utils.ConnectionString;
        string connectionString = Tools.Tools.connectionString;
        TeamService teamService = new TeamService();

        public TeamsController(){}

        [HttpGet]
        public async Task<ActionResult<List<Teams>>> GetAllTeams()
        {
            var teams = await teamService.GetAllTeams(connectionString);
            if (teams.Count == 0)
                return BadRequest("An error occurred while getting all the teams. Please check your request and try again.");
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teams>> GetTeamById(int id)
        {
            var team = await teamService.GetTeamById(connectionString, id);
            if (team != null)
            {
                return Ok(team);
            }
            else
            {
                return BadRequest("An error occurred while getting team. Please check your request and try again.");
            }
        }

        [HttpTeam]
        public async Task<ActionResult<Teams>> TeamTeam(TeamDto team)
        {
            try
            {
                var createdTeam = await teamService.CreateTeam(connectionString, team);
                if (createdTeam != null)
                {
                    return Ok(createdTeam);
                }
                else
                {
                    return BadRequest("An error occurred while creating the team. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the team. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, TeamDto team)
        {
            try 
            {
                var updatedTeam = await teamService.UpdateTeam(connectionString, id, team);
                if (updatedTeam != null)
                {
                    return Ok(updatedTeam);
                }
                else
                {
                    return BadRequest("An error occurred while updating the team. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the team. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teams>> DeleteTeam(int id)
        {
            var deletedTeam = await teamService.DeleteTeam(connectionString, id);
            if (deletedTeam != null)
            {                
                return Ok(deletedTeam);
            }
            else
            {
                return BadRequest("An error occurred while deleting the team. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/likes")]
        public async Task<ActionResult<List<Threads>>> GetAllThreadsByTeamId(int id)
        {
            var threads = await teamService.GetAllLikesByTeamId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
