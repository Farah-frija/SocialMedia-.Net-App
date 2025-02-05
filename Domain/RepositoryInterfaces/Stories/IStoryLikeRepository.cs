using Core.Domain.Entities;

namespace Core.Domain.RepositoryInterfaces.Stories
{
    public interface IStoryLikeRepository
    {
        Task<bool> IsStoryLikedByUserAsync(Guid storyId, string userId);
        Task AddLikeAsync(StoryLike like);
        Task RemoveLikeAsync(StoryLike like);
        Task<int> GetLikeCountAsync(Guid storyId);
        Task<StoryLike?> GetStoryLikeAsync(Guid storyId, string userId);

    }
}