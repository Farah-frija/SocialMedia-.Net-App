using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Context; // Adjust this import based on your DbContext's namespace
using Core.Domain.RepositoryInterfaces.Follows;
using Core.Domain.Entities;
using Core.Domain.Enums; // Adjust for the FriendRequestStatus enum

public class FollowRepo : IFollowRepo
{
    private readonly ApplicationDbContext _context;

    public FollowRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    // Follow a user
    public async Task<IdentityResult> FollowUserAsync(Follow follow)
    {
        var follower = await _context.Users.FindAsync(follow.SenderId);
        var followed = await _context.Users.FindAsync(follow.ReceiverId);

        if (follower == null || followed == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
        var existingFollow = await _context.Follows
       .FirstOrDefaultAsync(f => f.SenderId == follow.SenderId && f.ReceiverId == follow.ReceiverId);

        if (existingFollow != null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Follow relationship already exists." });
        }

        follow.Id = Guid.NewGuid().ToString("N");

        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    // Accept follow request
    public async Task<IdentityResult> AcceptFollowRequestAsync(string followId)
    {
        var follow = await _context.Follows.FindAsync(followId);
        if (follow == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Follow request not found." });
        }

        follow.Status = FollowRequestStatus.Accepted;
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    // Refuse follow request
    public async Task<IdentityResult> RefuseFollowRequestAsync(string followId)
    {
        var follow = await _context.Follows.FindAsync(followId);
        if (follow == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Follow request not found." });
        }

        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    // Cancel follow request
    public async Task<IdentityResult> UnfollowUserAsync(Follow follows)
    {
        var follow = await _context.Follows
        .FirstOrDefaultAsync(f => f.SenderId == follows.SenderId && f.ReceiverId == follows.ReceiverId);

        if (follow == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Follow relationship not found." });
        }

        // Remove the follow record from the database
        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    // Get list of users a given user is following
    public async Task<List<User>> GetFollowingAsync(string userId)
    {
        var followingIds = await _context.Follows
            .Where(f => f.SenderId == userId && f.Status == FollowRequestStatus.Accepted)
            .Select(f => f.ReceiverId)
            .ToListAsync();

        var followingUsers = await _context.Users
            .Where(u => followingIds.Contains(u.Id))
            .ToListAsync();

        return followingUsers;
    }

    // Get list of followers for a given user
    public async Task<List<User>> GetFollowersAsync(string userId)
    {
        var followerIds = await _context.Follows
            .Where(f => f.ReceiverId == userId && f.Status == FollowRequestStatus.Accepted)
            .Select(f => f.SenderId)
            .ToListAsync();

        var followers = await _context.Users
            .Where(u => followerIds.Contains(u.Id))
            .ToListAsync();

        return followers;
    }

    // Unfollow a user
  

    // Get waiting follow requests (those that are still pending)
    public async Task<List<Follow>> GetWaitingFollowRequestsAsync(string userId)
    {
        var waitingRequests = await _context.Follows
            .Where(f => f.ReceiverId == userId && f.Status == FollowRequestStatus.Pending)
            .ToListAsync();

        return waitingRequests;
    }

   
}

