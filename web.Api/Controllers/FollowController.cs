using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Core.Application.Interfaces.Follows;
using Core.Domain.Entities;
using Core.Application.DTOs.DTOsRequests.FollowRequests;
using Core.Application.DTOs.DTOsResponses.FollowResponse;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication

    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IMapper _mapper;

        public FollowController(IFollowService followService, IMapper mapper)
        {
            _followService = followService;
            _mapper = mapper;
        }

        // Follow a user
        [HttpPost("Follow")]
        public async Task<IActionResult> FollowUser([FromBody] GetFollowDto followdetails)
        {     
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid follow data", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var follow = _mapper.Map<Follow>(followdetails);

            var result = await _followService.FollowUserAsync(follow);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "Follow action failed", Errors = errors });
            }

            return Ok(new { Message = "Follow action successful" });
        }

        // Accept follow request
        [HttpPost("AcceptFollowRequest/{followId}")]
        public async Task<IActionResult> AcceptFollowRequest([FromRoute] string followId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid follow request data", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var result = await _followService.AcceptFollowRequestAsync(followId);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "Follow request acceptance failed", Errors = errors });
            }

            return Ok(new { Message = "Follow request accepted successfully" });
        }

        // Refuse follow request
        [HttpPost("RefuseFollowRequest/{followId}")]
        public async Task<IActionResult> RefuseFollowRequest([FromRoute] string followId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid follow request data", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var result = await _followService.RefuseFollowRequestAsync(followId);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "Follow request refusal failed", Errors = errors });
            }

            return Ok(new { Message = "Follow request refused successfully" });
        }

        // Unfollow a user
        [HttpPost("Unfollow")]
        public async Task<IActionResult> UnfollowUser([FromBody] GetFollowDto followdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid follow request data", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }
            var follow = _mapper.Map<Follow>(followdetails);

            var result = await _followService.UnfollowUserAsync(follow);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "Follow request refusal failed", Errors = errors });
            }

            return Ok(new { Message = "User Unfollowed successfully" });
        }

        // Get list of users a given user is following
        [HttpGet("Following/{userId}")]
        public async Task<IActionResult> GetFollowing([FromRoute] string userId)
        {
            var followings = await _followService.GetFollowingAsync(userId);
            var followingsDto = _mapper.Map<List<listingFollowersDto>>(followings);
            return Ok(new { Message = "Following list retrieved", Following = followingsDto });
        }

        // Get list of followers for a given user
        [HttpGet("Followers/{userId}")]
        public async Task<IActionResult> GetFollowers([FromRoute] string userId)
        {
            var followers = await _followService.GetFollowersAsync(userId);
            var followersDto = _mapper.Map<List<listingFollowersDto>>(followers);

            return Ok(new { Message = "Followers list retrieved", Followers = followersDto });
        }

        // Get waiting follow requests
        [HttpGet("WaitingFollowRequests/{userId}")]
        public async Task<IActionResult> GetWaitingFollowRequests([FromRoute] string userId)
        {
            var waitingRequests = await _followService.GetWaitingFollowRequestsAsync(userId);
            

            return Ok(new { Message = "Waiting follow requests retrieved", WaitingRequests = waitingRequests });
        }
    }
}
