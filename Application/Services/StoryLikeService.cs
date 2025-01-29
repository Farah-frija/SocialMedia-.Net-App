using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class StoryLikeService:IStoryLikeService
    {
        private readonly IStoryLikeRepository _storyLikeRepository;

        public StoryLikeService(IStoryLikeRepository storyLikeRepository)
        {
            _storyLikeRepository = storyLikeRepository;
        }

        public async Task<bool> LikeOrUnlikeStoryAsync(Guid storyId, Guid userId)
        {
            var isLiked = await _storyLikeRepository.IsStoryLikedByUserAsync(storyId, userId);

            if (isLiked)
            {
                var like = new StoryLike { StoryId = storyId, UserId = userId };
                await _storyLikeRepository.RemoveLikeAsync(like);
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
