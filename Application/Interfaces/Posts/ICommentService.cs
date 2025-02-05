using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Posts
{
    public interface ICommentService
    {
        Task<Comment> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId);
        Task CreateAsync(Comment comment);
        Task UpdateAsync(Guid id, string content);
        Task DeleteAsync(Guid id);
    }
}