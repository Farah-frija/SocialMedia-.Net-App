using Core.Application.DTOs.DTOsRequests;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var comment = await _commentService.GetByIdAsync(id);
            return Ok(comment);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(Guid postId)
        {
            var comments = await _commentService.GetCommentsByPostAsync(postId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto createCommentDto)
        {
            await _commentService.CreateAsync(createCommentDto);
            return CreatedAtAction(nameof(GetCommentsByPost), new { postId = createCommentDto.PostId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] string content)
        {
            await _commentService.UpdateAsync(id, content);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
