

namespace Core.Application.DTOs.DTOsResponses.StoryResponse
{
    public class StoryCommentDto
    {
        public Guid Id { get; set; }
        public Guid StoryId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}