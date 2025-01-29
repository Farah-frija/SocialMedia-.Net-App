using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StoryRepository:IStoryRepository
    {
        private readonly ApplicationDbContext _context;
        public StoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<StoryComment> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.StoryComments
                .FirstOrDefaultAsync(c => c.Id == commentId);
        }
        public async Task<Story> GetByIdAsync(Guid id)
        {
            return await _context.Stories.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Story>> GetActiveStoriesByUserIdAsync(Guid userId)
        {
            return await _context.Stories
                .Where(s => s.UserId == userId && s.ExpiryTime > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task AddAsync(Story story)
        {
            await _context.Stories.AddAsync(story);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Story story)
        {
            _context.Stories.Update(story);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var story = await GetByIdAsync(id);
            if (story != null)
            {
                _context.Stories.Remove(story);
                await _context.SaveChangesAsync();
            }
        }
      

        public async Task UpdateCommentAsync(StoryComment comment)
        {
            _context.StoryComments.Update(comment); // Mark as updated
            await _context.SaveChangesAsync(); // Save changes to the database
        }
    }
}
