using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Posts
{
    public interface IPostService
    {
        Task<Post> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetPostsByUserAsync(string userId);
        Task CreateAsync(Post post, List<IFormFile> photoFiles);
        Task DeleteAsync(Guid id);
    }
}