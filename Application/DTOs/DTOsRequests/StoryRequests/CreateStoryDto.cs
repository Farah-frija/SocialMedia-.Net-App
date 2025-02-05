using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.DtoRequests.StoryRequests
{
    public class CreateStoryDto
    {
        public string Content { get; set; }
        public IFormFile? Image { get; set; } // Image file upload

        public string UserId { get; set; }
    }
}