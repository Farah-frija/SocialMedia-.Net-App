
namespace Core.Application.DTOs.DTOsRequests.PostRequests
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
