using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces.Follows
{
    public interface IFollowRepo
    {
        Task<IdentityResult> FollowUserAsync(Follow follow);
        Task<IdentityResult> AcceptFollowRequestAsync(string followId);
        Task<IdentityResult> RefuseFollowRequestAsync(string followId);
        Task<IdentityResult> UnfollowUserAsync(Follow follow);
        Task<List<User>> GetFollowingAsync(string userId);
        Task<List<User>> GetFollowersAsync(string userId);

        Task<List<Follow>> GetWaitingFollowRequestsAsync(string userId);
    }


}
