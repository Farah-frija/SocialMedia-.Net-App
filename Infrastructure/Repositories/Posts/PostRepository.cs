using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Posts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Photos) // Ensure Photos are included
                .Include(p => p.User)  // Ensure User is included
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Photos) // Include Photos to fetch the images
                .Include(p => p.User)  // Include User to fetch the user data
                .ToListAsync();
        }

        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await GetByIdAsync(id);
            if (post == null) throw new Exception("Post not found");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAsync(String userId)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Photos)  // Include Photos for each post
                .Include(p => p.User)  // Include the User for each post
                .Where(p => p.UserId == userId)  // Filter by UserId
                .ToListAsync();
        }
    }
}
