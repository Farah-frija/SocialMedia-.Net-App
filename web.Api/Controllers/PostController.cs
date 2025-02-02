using Core.Application.DTOs;
using Core.Application.DTOs.DTOsRequests;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService; // Injected ICommentService

        public PostController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService; // Initialize the comment service
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
            return Ok(post);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePostDto createPostDto, [FromForm] List<IFormFile> photoFiles)
        {
            // Call the service to create the post with photos
            await _postService.CreateAsync(createPostDto, photoFiles);

            // Return a response
            return CreatedAtAction(nameof(GetById), new { id = createPostDto.UserId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _postService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPostsByUser(Guid userId)
        {
            var posts = await _postService.GetPostsByUserAsync(userId);
            return Ok(posts);
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsByPost(Guid id)
        {
            var comments = await _commentService.GetCommentsByPostAsync(id); // Use the comment service
            return Ok(comments);
        }
    }
}
