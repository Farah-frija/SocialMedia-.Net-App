using Core.Application.Interfaces.Stories;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Posts;
using Core.Domain.RepositoryInterfaces.Stories;


namespace Core.Application.Services.Stories
{
    public class StoryLikeService : IStoryLikeService
    {
        private readonly IStoryLikeRepository _storyLikeRepository;

        public StoryLikeService(IStoryLikeRepository storyLikeRepository)
        {
            _storyLikeRepository = storyLikeRepository;
        }

        public async Task<bool> LikeOrUnlikeStoryAsync(Guid storyId, string userId)
        {
            var existingLike = await _storyLikeRepository.GetStoryLikeAsync(storyId, userId);
           // var existingLike = await _likeRepository.GetLikeAsync(postId, userId);

            if (existingLike != null)
            {
                //var like = new StoryLike { StoryId = storyId, UserId = userId };
                await _storyLikeRepository.RemoveLikeAsync(existingLike);
                return false; // Unliked
            }
            else
            {
                var like = new StoryLike { StoryId = storyId, UserId = userId };
                await _storyLikeRepository.AddLikeAsync(like);
                return true; // Liked
            }
        }

        public async Task<int> GetStoryLikeCountAsync(Guid storyId)
        {
            return await _storyLikeRepository.GetLikeCountAsync(storyId);
        }
    }
}