using Core.Application.Interfaces;
using Core.Application.Interfaces.UserProfile;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Application.DTOs.DTOsRequests.Identity;
using System.IdentityModel.Tokens.Jwt;
using Core.Application.DTOs.DTOsResponses.ProfileResponse;
using Core.Application.Services.Stories;
using Core.Application.Interfaces.Stories;
using Microsoft.AspNetCore.Http;


namespace WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize] // Requires authentication
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFileStorageServiceStories _fileStorageService;

        public UserController(IUserService userService, IFileStorageServiceStories fileStorageService)
        {
            _userService = userService;
            _fileStorageService = fileStorageService;

        }

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                // Log the claims for debugging
                var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                return Unauthorized(new { Message = "Unauthorized", Claims = claims });
            }

            var user = await _userService.GetUserProfileAsync(userId);
            if (user == null) return NotFound();

            // Map the User entity to ProfileDTO
            var profileDto = new ProfileDTO
            {
                Username = user.UserName,
                Biography = user.Biography,
                Birthday = user.Birthday,
                IsPrivateProfile = user.IsPrivateProfile,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return Ok(profileDto);
        }


        [HttpPut("biography")]
        public async Task<IActionResult> UpdateBiography([FromBody] string biography)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            await _userService.UpdateBiographyAsync(userId, biography);
            return NoContent();
        }

        [HttpPut("birthday")]
        public async Task<IActionResult> UpdateBirthday([FromBody] DateOnly birthday)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            await _userService.UpdateBirthdayAsync(userId, birthday);
            return NoContent();
        }

        [HttpPut("toggle-private")]
        public async Task<IActionResult> TogglePrivateProfile()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            await _userService.TogglePrivateProfileAsync(userId);
            return NoContent();
        }
        [HttpPut("profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture( IFormFile file)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            await _userService.UpdateProfilePictureAsync(userId, file);

            return Ok(new { Message = "Profile picture updated successfully." });
        }

        // Update Username
        [HttpPut("username")]
        public async Task<IActionResult> UpdateUsername([FromBody] string newUsername)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var result = await _userService.UpdateUsernameAsync(userId, newUsername);
            if (!result) return BadRequest("Username is already taken or invalid.");

            return NoContent();
        }

        // Update Password
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest model)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var result = await _userService.UpdatePasswordAsync(userId, model.CurrentPassword, model.NewPassword);
            if (!result) return BadRequest("Incorrect current password.");

            return NoContent();
        }
    }
}
