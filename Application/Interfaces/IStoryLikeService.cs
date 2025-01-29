using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IStoryLikeService
    {
        Task<bool> LikeOrUnlikeStoryAsync(Guid storyId, Guid userId);
        Task<int> GetStoryLikeCountAsync(Guid storyId);
    }
}
