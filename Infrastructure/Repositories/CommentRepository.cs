using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _context.Comments
                .Include(c => c.User) // Include the user navigation property
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId)
        {
            return await _context.Comments
                .Include(c => c.User) // Include the user navigation property
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await GetByIdAsync(id);
            if (comment == null) throw new Exception("Comment not found");

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
