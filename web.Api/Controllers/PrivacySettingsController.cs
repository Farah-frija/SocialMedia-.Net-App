using Core.Application.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivacySettingsController : ControllerBase
    {
        private readonly IPrivacySettingsService _privacySettingsService;

        public PrivacySettingsController(IPrivacySettingsService privacySettingsService)
        {
            _privacySettingsService = privacySettingsService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetPrivacySettings(Guid userId)
        {
            try
            {
                var settings = _privacySettingsService.GetPrivacySettings(userId);
                return Ok(settings);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{userId}")]
        public IActionResult UpdatePrivacySettings(Guid userId, [FromBody] PrivacySettings updatedSettings)
        {
            try
            {
                _privacySettingsService.UpdatePrivacySettings(userId, updatedSettings);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
