using Core.Application.DTOs.DtoRequests.StoryRequests;
using Core.Application.Interfaces.Stories;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication

    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;
        private readonly IStoryLikeService _storyLikeService;
        private readonly IStoryCommentService _commentService;

        public StoriesController(IStoryService storyService, IStoryLikeService storyLikeService, IStoryCommentService storyCommentService)
        {
            _storyService = storyService;
            _storyLikeService = storyLikeService;
            _commentService = storyCommentService;
        }

        // POST: api/Stories
        [HttpPost]
        public async Task<ActionResult<Story>> CreateStory([FromForm] CreateStoryDto storyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Ensures validation errors are returned.
            }

            try
            {
                var createdStory = await _storyService.AddStoryAsync(storyDto);
                if (createdStory == null)
                {
                    return StatusCode(500, "Unable to create the story. Please try again.");
                }

                return StatusCode(201, createdStory); // Returns the created story with HTTP 201 status.
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework like Serilog).
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";
                return StatusCode(500, $"An error occurred: {ex.Message}. Inner Exception: {innerExceptionMessage}");
            }
        }

        // PUT: api/Stories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Story>> UpdateStory(Guid id, [FromForm] UpdateStoryDto storyDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedStory = await _storyService.UpdateStoryAsync(id, storyDto); // Pass Id explicitly.
                if (updatedStory == null)
                {
                    return NotFound($"Story with ID {id} not found.");
                }

                return Ok(updatedStory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Stories/active/{userId}
        [HttpGet("active/{userId}")]
        public async Task<ActionResult<IEnumerable<Story>>> GetActiveStories(string userId)
        {
            var stories = await _storyService.GetActiveStoriesAsync(userId);
            return Ok(stories);
        }

        // DELETE: api/Stories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStory(Guid id)
        {
            await _storyService.DeleteStoryAsync(id);
            return NoContent();
        }
        [HttpPost("{storyId}/like")]
        public async Task<IActionResult> LikeOrUnlikeStory(Guid storyId, string userId)
        {
            var liked = await _storyLikeService.LikeOrUnlikeStoryAsync(storyId, userId);
            return Ok(new { Liked = liked });
        }

        [HttpGet("{storyId}/likes")]
        public async Task<IActionResult> GetStoryLikes(Guid storyId)
        {
            var likeCount = await _storyLikeService.GetStoryLikeCountAsync(storyId);
            return Ok(new { Likes = likeCount });
        }
        [HttpPost("{storyId}/comments")]
        public async Task<IActionResult> AddComment(Guid storyId, string userId, [FromBody] string content)
        {
            var comment = await _commentService.AddCommentAsync(storyId, userId, content);
            return Ok(comment);
        }

        [HttpGet("{storyId}/comments")]
        public async Task<IActionResult> GetComments(Guid storyId)
        {
            var comments = await _commentService.GetCommentsByStoryAsync(storyId);
            return Ok(comments);
        }

        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> UpdateStoryComment(Guid commentId, [FromBody] string newContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedComment = await _commentService.UpdateStoryCommentAsync(commentId, newContent);
                return Ok(updatedComment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("comments/{commentId}")]
        public async Task<IActionResult> DeleteStoryComment(Guid commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _commentService.DeleteStoryCommentAsync(commentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }


}