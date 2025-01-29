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
    public class StoryCommentRepository:IStoryCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public StoryCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StoryComment comment)
        {
            await _context.StoryComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StoryComment>> GetCommentsByStoryIdAsync(Guid storyId)
        {
            return await _context.StoryComments
                .Where(c => c.StoryId == storyId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
        public async Task<StoryComment> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.StoryComments
                .FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task UpdateCommentAsync(StoryComment comment)
        {
            _context.StoryComments.Update(comment); // Mark as updated
            await _context.SaveChangesAsync(); // Save changes to the database
        }
        public async Task DeleteCommentAsync(Guid commentId)
        {
            var comment = await _context.StoryComments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment != null)
            {
                _context.StoryComments.Remove(comment); // Mark as removed
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }
    }
}
