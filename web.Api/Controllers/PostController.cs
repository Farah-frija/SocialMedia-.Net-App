using Core.Application.DTOs.DTOsRequests.PostRequests;
using Core.Application.DTOs.DTOsResponses.PostResponse;
using Core.Application.Interfaces.Posts;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                if (post == null)
                {
                    return NotFound(new { Message = "Post not found." });
                }

                var postDto = MapToPostDto(post);
                return Ok(postDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the post.", Error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var posts = await _postService.GetAllAsync();
                var postDtos = MapToPostDtos(posts);
                return Ok(postDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving posts.", Error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePostDto createPostDto, [FromForm] List<IFormFile> photoFiles)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Invalid input data.", Errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var post = new Post
                {
                    Id = Guid.NewGuid(),
                    Content = createPostDto.Content,
                    UserId = createPostDto.UserId
                };

                await _postService.CreateAsync(post, photoFiles);
                return CreatedAtAction(nameof(GetById), new { id = post.Id }, new { Message = "Post created successfully.", PostId = post.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the post.", Error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _postService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the post.", Error = ex.Message });
            }
        }


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPostsByUser(string userId)
        {
            try
            {
                var posts = await _postService.GetPostsByUserAsync(userId);
                var postDtos = MapToPostDtos(posts);
                return Ok(postDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving posts by user.", Error = ex.Message });
            }
        }


        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsByPost(Guid id)
        {
            try
            {
                var comments = await _commentService.GetCommentsByPostAsync(id);
                var commentDtos = MapToCommentDtos(comments);
                return Ok(commentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving comments for the post.", Error = ex.Message });
            }
        }

        private PostDto MapToPostDto(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                UserName = post.User?.UserName,
                CreatedAt = post.CreatedAt,
                PhotoUrls = post.Photos?.Select(p => p.Url).ToList()
            };
        }

        private IEnumerable<PostDto> MapToPostDtos(IEnumerable<Post> posts)
        {
            return posts.Select(post => new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                UserName = post.User?.UserName,
                CreatedAt = post.CreatedAt,
                PhotoUrls = post.Photos?.Select(p => p.Url).ToList()
            });
        }

        private IEnumerable<CommentDto> MapToCommentDtos(IEnumerable<Comment> comments)
        {
            return comments.Select(comment => new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                UserName = comment.User?.UserName,
                CreatedAt = comment.CreatedAt
            });
        }
    }
}