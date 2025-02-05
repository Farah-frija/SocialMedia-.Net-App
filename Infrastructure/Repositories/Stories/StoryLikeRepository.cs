using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Stories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Stories
{
    public class StoryLikeRepository : IStoryLikeRepository
    {
        private readonly ApplicationDbContext _context;

        public StoryLikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsStoryLikedByUserAsync(Guid storyId, string userId)
        {
            return await _context.StoryLikes
                .AnyAsync(sl => sl.StoryId == storyId && sl.UserId == userId);
        }

        public async Task AddLikeAsync(StoryLike like)
        {
            await _context.StoryLikes.AddAsync(like);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLikeAsync(StoryLike like)
        {
            _context.StoryLikes.Remove(like);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLikeCountAsync(Guid storyId)
        {
            return await _context.StoryLikes.CountAsync(sl => sl.StoryId == storyId);
        }

        public async Task<StoryLike?> GetStoryLikeAsync(Guid storyId, string userId)
        {
            return await _context.StoryLikes
                .FirstOrDefaultAsync(sl => sl.StoryId == storyId && sl.UserId == userId);
        }

    }
}