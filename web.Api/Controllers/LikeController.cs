using Core.Application.Interfaces.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication

    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}/toggle")]
        public async Task<IActionResult> ToggleLike(Guid postId, [FromQuery] string userId)
        {
            await _likeService.ToggleLikeAsync(postId, userId);
            return Ok(new { message = "Like updated successfully!" });
        }

        [HttpGet("{postId}/likes")]
        public async Task<IActionResult> GetLikeCount(Guid postId)
        {
            var count = await _likeService.GetLikeCountAsync(postId);
            return Ok(new { likes = count });
        }
    }


}
