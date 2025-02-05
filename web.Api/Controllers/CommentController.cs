using Core.Application.DTOs.DTOsRequests.PostRequests;
using Core.Application.DTOs.DTOsResponses.PostResponse;
using Core.Application.Interfaces.Posts;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var comment = await _commentService.GetByIdAsync(id);
                if (comment == null)
                {
                    return NotFound(new { Message = "Comment not found." });
                }

                var commentDto = MapToCommentDto(comment);
                return Ok(commentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the comment.", Error = ex.Message });
            }
        }


        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(Guid postId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByPostAsync(postId);
                var commentDtos = MapToCommentDtos(comments);
                return Ok(commentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving comments for the post.", Error = ex.Message });
            }
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Invalid input data.", Errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var comment = new Comment
                {
                    Id = Guid.NewGuid(),
                    Content = createCommentDto.Content,
                    UserId = createCommentDto.UserId,
                    PostId = createCommentDto.PostId,
                    CreatedAt = DateTime.UtcNow
                };

                await _commentService.CreateAsync(comment);
                return CreatedAtAction(nameof(GetCommentsByPost), new { postId = comment.PostId }, new { Message = "Comment created successfully.", CommentId = comment.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the comment.", Error = ex.Message });
            }
        }

  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] string content)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    return BadRequest(new { Message = "Content cannot be empty." });
                }

                await _commentService.UpdateAsync(id, content);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the comment.", Error = ex.Message });
            }
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _commentService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the comment.", Error = ex.Message });
            }
        }

        private CommentDto MapToCommentDto(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                UserName = comment.User?.UserName,
                CreatedAt = comment.CreatedAt
            };
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