using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Stories
{
    public interface IStoryLikeService
    {
        Task<bool> LikeOrUnlikeStoryAsync(Guid storyId, string userId);
        Task<int> GetStoryLikeCountAsync(Guid storyId);
    }
}