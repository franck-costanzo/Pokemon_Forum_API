using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.DTO.TopicDTO;
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
    [Route("/topics")]
    public class TopicsController : ControllerBase
    {
        string connectionString = Utils.ConnectionString;
        TopicService topicService = new TopicService();

        public TopicsController(){}

        [HttpGet]
        public async Task<ActionResult<List<Topics>>> GetAllTopics()
        {
            var topics = await topicService.GetAllTopics(connectionString);
            if (topics.Count == 0)
                return BadRequest("An error occurred while getting all the topics. Please check your request and try again.");
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topics>> GetTopicById(int id)
        {
            var topic = await topicService.GetTopicById(connectionString, id);
            if (topic.name != null)
            {
                return Ok(topic);
            }
            else
            {
                return BadRequest("An error occurred while getting topic. Please check your request and try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Topics>> PostTopic(TopicDto topic)
        {
            try
            {
                var createdTopic = await topicService.CreateTopic(connectionString, topic);
                if (createdTopic != null)
                {
                    dynamic response = new ExpandoObject();
                    response.name = createdTopic.name;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while creating the topic. Please check your request and try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the topic. Please check your request and try again.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopic(int id, TopicDto topic)
        {
            try 
            {
                var updatedTopic = await topicService.UpdateTopic(connectionString, id, topic);
                if (updatedTopic.name != null)
                {
                    dynamic response = new ExpandoObject();
                    response.topic_id = id;
                    response.name = updatedTopic.name;
                    return Ok(response);
                }
                else
                {
                    return BadRequest("An error occurred while updating the topic. Please check your request and try again.");
                }
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occurred while updating the topic. Please check your request and try again.");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Topics>> DeleteTopic(int id)
        {
            var deletedTopic = await topicService.DeleteTopic(connectionString, id);
            if (deletedTopic.name != null)
            {                
                return Ok(deletedTopic);
            }
            else
            {
                return BadRequest("An error occurred while deleting the topic. Please check your request and try again.");
            }
            
        }


        [HttpGet("{id}/forums")]
        public async Task<ActionResult<List<Threads>>> GetAllForumsByTopicId(int id)
        {
            var threads = await topicService.GetForumsByTopicId(connectionString, id);
            if (threads == null)
            {
                return NotFound();
            }
            return Ok(threads);
        }


    }
}
