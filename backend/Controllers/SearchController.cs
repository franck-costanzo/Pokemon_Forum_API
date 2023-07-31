using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon_Forum_API.Entities;
using Pokemon_Forum_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon_Forum_API.Controllers
{
    [ApiController]
    [Route("/search")]
    public class SearchController : Controller
    {
        #region Properties

        string connectionString = Tools.Tools.connectionString;

        SearchService searchService = new SearchService();
        #endregion

        [HttpGet("{searchString}")]
        public async Task<ActionResult<List<Posts>>> SearchPosts(string searchString)
        {
            var posts = await searchService.SearchPosts(connectionString, searchString);
            if (posts.Count == 0)
                return BadRequest("An error occurred while searching the posts. Please check your request and try again.");
            return Ok(posts);
        }
    }
}
