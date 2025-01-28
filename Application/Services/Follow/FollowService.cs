using Core.Application.Interfaces.Follows;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Follows;
using Microsoft.AspNetCore.Identity;

public class FollowService : IFollowService
{
    private readonly IFollowRepo _followRepo;

    public FollowService(IFollowRepo followRepo)
    {
        _followRepo = followRepo;
    }

    // Follow a user
    public async Task<IdentityResult> FollowUserAsync(Follow follow)
    {
        return await _followRepo.FollowUserAsync(follow);
    }

    // Accept follow request
    public async Task<IdentityResult> AcceptFollowRequestAsync(string followId)
    {
        return await _followRepo.AcceptFollowRequestAsync(followId);
    }

    // Refuse follow request
    public async Task<IdentityResult> RefuseFollowRequestAsync(string followId)
    {
        return await _followRepo.RefuseFollowRequestAsync(followId);
    }

    // Cancel follow request
  

    // Get following list for a user
    public async Task<List<User>> GetFollowingAsync(string userId)
    {
        return await _followRepo.GetFollowingAsync(userId);
    }

    // Get followers list for a user
    public async Task<List<User>> GetFollowersAsync(string userId)
    {
        return await _followRepo.GetFollowersAsync(userId);
    }

    // Get waiting follow requests
    public async Task<List<Follow>> GetWaitingFollowRequestsAsync(string userId)
    {
        return await _followRepo.GetWaitingFollowRequestsAsync(userId);
    }

    // Unfollow a user (use the follow object)
    public async Task<IdentityResult> UnfollowUserAsync(Follow follow)
    {
        return await _followRepo.UnfollowUserAsync(follow);
    }

    
}

