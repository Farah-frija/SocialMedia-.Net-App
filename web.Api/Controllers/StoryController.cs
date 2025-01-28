using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;
        
        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
           
        }

        [HttpPost("add")]
        public IActionResult AddStory(Guid userId,string content, int hoursToExpire = 24)
        {
           

            try
            {
                _storyService.AddStory(userId,content, hoursToExpire);
                return Ok("Story added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{storyId}")]
        public IActionResult DeleteStory(Guid storyId)
        {
           

            try
            {
                _storyService.DeleteStory(storyId);
                return Ok("Story deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("active")]
        public IActionResult GetActiveStories(Guid userId)
        {
            

            var stories = _storyService.GetActiveStories(userId);
            return Ok(stories);
        }
    }

}
