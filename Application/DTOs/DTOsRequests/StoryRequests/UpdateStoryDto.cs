using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.DtoRequests.StoryRequests
{
    public class UpdateStoryDto
    {
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}