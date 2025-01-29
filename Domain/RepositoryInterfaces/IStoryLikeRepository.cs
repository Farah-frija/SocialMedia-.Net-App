using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IStoryLikeRepository
    {
        Task<bool> IsStoryLikedByUserAsync(Guid storyId, Guid userId);
        Task AddLikeAsync(StoryLike like);
        Task RemoveLikeAsync(StoryLike like);
        Task<int> GetLikeCountAsync(Guid storyId);
    }
}
