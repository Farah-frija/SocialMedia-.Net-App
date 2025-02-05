using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Posts
{
    public interface ILikeService
    {
        Task ToggleLikeAsync(Guid postId, string userId);
        Task<int> GetLikeCountAsync(Guid postId);
    }


}
