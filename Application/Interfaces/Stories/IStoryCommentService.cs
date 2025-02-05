using Core.Application.DTOs.DTOsResponses.StoryResponse;


namespace Core.Application.Interfaces.Stories
{
    public interface IStoryCommentService
    {
        Task<StoryCommentDto> AddCommentAsync(Guid storyId, string userId, string content);
        Task<IEnumerable<StoryCommentDto>> GetCommentsByStoryAsync(Guid storyId);
        Task<StoryCommentDto> UpdateStoryCommentAsync(Guid commentId, string newContent);
        Task DeleteStoryCommentAsync(Guid commentId);
    }
}