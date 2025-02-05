

namespace Core.Application.DTOs.DTOsResponses.PostResponse
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> PhotoUrls { get; set; } // New property
    }
}
